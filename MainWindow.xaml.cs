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
        public static List<Entity> entities = new List<Entity>();

        // Use a stopwatch instead of datetime now for smaller values (faster hopefully)
        public static Stopwatch gameTimer = Stopwatch.StartNew();
        private static long lastFrameTimeMs = 0;

        // Fixed-timestep accumulator
        private double accumulator = 0.0;
        private double fixedDt = 1.0 / 120.0; // physics step at 120hz

        public Player player;

        public MainWindow()
        {
            InitializeComponent();
            Content = canvas;
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

            lastFrameTimeMs = gameTimer.ElapsedMilliseconds;

            // Update screens every screen refresh
            CompositionTarget.Rendering += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            // delta time in seconds
            double deltaTime = (gameTimer.ElapsedMilliseconds - lastFrameTimeMs) / 1000.0;
            lastFrameTimeMs = gameTimer.ElapsedMilliseconds;

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

            // interpolate render between previous and current physics state
            double alpha = accumulator / fixedDt;

            foreach (var entity in entities)
                entity.Interpolate(alpha);
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

        //when the window closes, reset the timer potentially stopping a memory leak
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Reset();
            lastFrameTimeMs = 0;
        }
    }



    public class GameCanvas : Canvas
    {
        public GameCanvas() : base()
        {
            Background = Brushes.LightBlue;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
    }

}