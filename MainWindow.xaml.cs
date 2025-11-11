using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
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
    public partial class MainWindow : Window
    {
        public static GameCanvas canvas = new GameCanvas();
        private static TranslateTransform cameraTransform = new TranslateTransform();

        public static List<Entity> entities = new List<Entity>();

        // Use a stopwatch instead of datetime now for smaller values (faster hopefully)
        public static Stopwatch gameTimer = Stopwatch.StartNew();
        private static double lastFrameTimeMs = 0;

        // Fixed-timestep accumulator
        private double accumulator = 0.0;
        private double fixedDt = 1.0 / 60.0; // physics step at 120hz

        public Player player;

        public MainWindow()
        {
            InitializeComponent();
            Content = canvas;
            canvas.RenderTransform = cameraTransform;
            Title = "Non-Physical Platformer"; 

            //wait to load the map until the window is initialized
            Loaded += (s, e) =>
            {
                MapLoader.LoadMap(1);
                Keyboard.Focus(this);
            };

            //physical entities go here
            entities.Add(new Player());
            player = entities.OfType<Player>().First();

            foreach (Entity entity in entities)
                canvas.Children.Add(entity.sprite);

            lastFrameTimeMs = gameTimer.Elapsed.TotalSeconds;

            // Update screens every screen refresh
            CompositionTarget.Rendering += Update;
        }

        private void Update(object? sender, EventArgs e)
        {
            // delta time in seconds
            double currentTime = gameTimer.Elapsed.TotalSeconds;
            double deltaTime = currentTime - lastFrameTimeMs;
            lastFrameTimeMs = currentTime;

            // avoid huge deltas to stop big jumps
            if (deltaTime <= 0 || double.IsNaN(deltaTime) || deltaTime > 0.25)
                deltaTime = 1.0 / 60.0;

            accumulator += deltaTime;

            // save previous physics state once per frame (used for interpolation)
            foreach (var entity in entities)
                entity.previousPosition = entity.position;

            // fixed step physics updates
            while (accumulator >= fixedDt)
            {
                foreach (var entity in entities)
                    entity.update(fixedDt);

                accumulator -= fixedDt;
            }

            if (deltaTime > 0.05)
                Trace.WriteLine($"High DT: {deltaTime}");

            // interpolate render between previous and current physics state
            double alpha = accumulator / fixedDt;

            foreach (var entity in entities)
                entity.Interpolate(alpha);
        }

        //to be called by the player to be able to use interpolated position
        public static void MoveCamera(Vector2 pos)
        {
            // create values that track the player in the middle of the screen
            float camX = pos.X - ((float)canvas.ActualWidth / 2);
            float camY = pos.Y - ((float)canvas.ActualHeight / 2);

            // Apply movement to the transform
            cameraTransform.X = -camX;
            cameraTransform.Y = -camY;
        }

        //functions from XAML
        // ==============================================================
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            player.OnKeyDown(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            player.OnKeyUp(e);
        }

        //when the window closes, reset the timer potentially stopping a memory leak / large delta times
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop(); gameTimer.Reset();
            lastFrameTimeMs = 0;
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