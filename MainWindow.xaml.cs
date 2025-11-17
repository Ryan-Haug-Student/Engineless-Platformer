using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
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
    }

    public partial class MainWindow : Window
    {
        public static GameCanvas canvas = new GameCanvas();
        private static TranslateTransform cameraTransform = new TranslateTransform();

        // Use a stopwatch instead of datetime now for smaller values (faster hopefully)
        public static Stopwatch? gameTimer;

        // Fixed-timestep accumulator
        private double accumulator = 0.0;
        private double fixedDt = 1.0 / 60.0; // physics step at 60hz

        public Player player;

        private bool physicsRunning = true;

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
            WorldData.entities.Add(new Player(
                new Vector2(50, 100),  //starting pos
                new Vector2(35, 70))); //scale

            //add player reference and move player to top of screen
            player = WorldData.entities.OfType<Player>().First();
            Panel.SetZIndex(player.sprite, 1);

            foreach (PhysicalEntity entity in WorldData.entities)
                canvas.Children.Add(entity.sprite);

            StartPhysicsLoop();

            // Update screens every screen refresh
            CompositionTarget.Rendering += UpdateScreen;
        }

        private void UpdateScreen(object? sender, EventArgs e)
        {
            double alpha = Math.Clamp(accumulator / fixedDt, 0.0, 1.0);
            foreach (var entity in WorldData.entities)
                entity.Interpolate(alpha);
        }

        private void StartPhysicsLoop()
        {
            Task.Run(() =>
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

                    Thread.Sleep(1); // avoid high cpu usage
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

            physicsRunning = false;
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