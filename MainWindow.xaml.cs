using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using EnginelessPhysics.src.game;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EnginelessPhysics
{
    //worldData class is usesd for accessing data accross threads
    public static class WorldData
    {
        public static List<PhysicalEntity> entities = new List<PhysicalEntity>();
        public static List<Entity> staticEntities = new List<Entity>();

        public static List<Vector2> grapplePoints = new List<Vector2>();

        public static float tileScale;
    }

    public partial class MainWindow : Window
    {
        public static object? _origContent;
        public static GameCanvas canvas = new GameCanvas();
        public static GameCanvas uiCanvas = new GameCanvas();
        private static Grid root = new Grid();

        private static GameCanvas WinMenu = new GameCanvas();

        public static TranslateTransform cameraTransform = new TranslateTransform();

        // Use a stopwatch instead of datetime now for smaller values (faster hopefully)
        public static Stopwatch? gameTimer;

        // Fixed-timestep accumulator
        private double accumulator = 0.0;
        private double fixedDt = 1.0 / 60.0; // physics step at 60hz

        public static Player? player;

        public static bool physicsRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            Title = "physics now?????";

            // Global pixel-art settings
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);

            //create a grid to overlay the gamecanvas and uicanvas to keep UI onscreen
            root.Children.Add(canvas);
            root.Children.Add(uiCanvas);

            WinMenu.Children.Add(new TextBlock
            {
                Text = "You win!!!",
                Padding = new Thickness(100, 100, 0, 0),
                FontSize = 120,
            });

            StartPhysics();
        }

        //update screen and physics loop need to be in mainwindow due to them handling u
        private void UpdateScreen(object? sender, EventArgs e)
        {
            double alpha = Math.Clamp(accumulator / fixedDt, 0.0, 1.0);
            foreach (var entity in WorldData.entities)
                entity.Interpolate(alpha);

            if (player != null)
                player.AnimController();
        }

        private void StartPhysics()
        {
            Task.Run(async () =>
            {
                //keep running between levels
                while (true)
                {
                    gameTimer = Stopwatch.StartNew();
                    double lastTime = gameTimer.Elapsed.TotalSeconds;
                    double fixedDt = 1.0 / 60.0; // 60 Hz

                    while (physicsRunning)
                    {
                        double currentTime = gameTimer.Elapsed.TotalSeconds;
                        double deltaTime = currentTime - lastTime;
                        lastTime = currentTime;

                        accumulator += deltaTime;

                        while (accumulator >= fixedDt)
                        {
                            foreach (var entity in WorldData.entities)
                            {
                                entity.previousPosition = entity.position;
                                entity.update(fixedDt);
                            }

                            accumulator -= fixedDt;
                        }

                        await Task.Yield();
                    }
                }
            });
        }

        //to be called by the player to be able to use interpolated position
        public static void MoveCamera(Vector2 pos)
        {
            // create values that track the player in the middle of the screen
            float camX = pos.X - ((float)canvas.ActualWidth / 2);
            float camY = pos.Y - ((float)canvas.ActualHeight / 2);

            cameraTransform.X = -camX;
            cameraTransform.Y = -camY;
        }

        public static void LoadMainMenu()
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                GameManager.ClearScene();
                if (_origContent != null)
                    ((MainWindow)Application.Current.MainWindow).Content = _origContent;
            });
        }

        public static async void LoadWinMenu()
        {
            await Application.Current.Dispatcher.BeginInvoke(() =>
            {
                GameManager.ClearScene();
                ((MainWindow)Application.Current.MainWindow).Content = WinMenu;
            });

            await Task.Delay(5000);

            LoadMainMenu();
        }

        //functions from XAML
        // ==============================================================
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (player != null)
                player.OnKeyDown(e);

            if (e.Key == Key.Escape)
                LoadMainMenu();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (player != null)
                player.OnKeyUp(e);
        }

        //when the window closes, reset the timer potentially stopping a memory leak / large delta times
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#pragma warning disable CS8602
            gameTimer.Stop(); gameTimer.Reset();
            CompositionTarget.Rendering -= UpdateScreen;
            physicsRunning = false;
        }

        //main menu buttons
        //===============================================================

        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            _origContent = Content;

            Content = root;
            canvas.Loaded += (s, e) => GameManager.LoadScene(0);

            CompositionTarget.Rendering += UpdateScreen;
        }

        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }


    public class GameCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
    }

}