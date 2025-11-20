using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using EnginelessPhysics.src.engine.Entities;
using EnginelessPhysics.src.game;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
        public static object _origContent;
        public static GameCanvas canvas = new GameCanvas();
        public static TranslateTransform cameraTransform = new TranslateTransform();

        // Use a stopwatch instead of datetime now for smaller values (faster hopefully)
        public static Stopwatch? gameTimer;

        // Fixed-timestep accumulator
        private double accumulator = 0.0;
        private double fixedDt = 1.0 / 60.0; // physics step at 60hz

        public static Player player;

        public static bool physicsRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            Title = "physics now?????";

            StartPhysics();
        }

        //update screen and physics loop need to be in mainwindow due to them handling u
        private void UpdateScreen(object? sender, EventArgs e)
        {
            double alpha = Math.Clamp(accumulator / fixedDt, 0.0, 1.0);
            foreach (var entity in WorldData.entities)
                entity.Interpolate(alpha);
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

                            await Task.Delay(1);
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

        //functions from XAML
        // ==============================================================
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (player != null)
                player.OnKeyDown(e);

            if (e.Key == Key.Escape)
            {
                GameManager.ClearScene();
                Content = _origContent;
            }
            else if (e.Key == Key.D2)
            {
                GameManager.ClearScene();
                GameManager.LoadScene(2);
            }
            else if (e.Key == Key.D3)
            {
                GameManager.ClearScene();
                GameManager.LoadScene(3);
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (player != null)
                player.OnKeyUp(e);
        }

        //when the window closes, reset the timer potentially stopping a memory leak / large delta times
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop(); gameTimer.Reset();

            physicsRunning = false;
        }

        //main menu buttons
        //===============================================================
        
        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            _origContent = Content;

            canvas = new GameCanvas();
            Content = canvas;
            canvas.Loaded += (s, e) => GameManager.LoadScene(1);

            CompositionTarget.Rendering += UpdateScreen;
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