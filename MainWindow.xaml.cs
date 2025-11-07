using EnginelessPhysics.src.engine;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnginelessPhysics
{
    public partial class MainWindow : Window
    {
        public static GameCanvas canvas = new GameCanvas();
        public static List<Entity> entities = new List<Entity>();

        public static DateTime lastFrameTime = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            Content = canvas;
            Title = "Non-Physical Platformer";

            //wait to load the map until the window is initialized
            Loaded += (s, e) => MapLoader.LoadMap(1);

            //... add other entities here

            // Update screens every screen refresh
            CompositionTarget.Rendering += Update;
        }
        private void Update(object sender, EventArgs e)
        {
            //calculate seconds from last frame (deltatime)
            double deltaTime = (DateTime.Now - lastFrameTime).TotalMilliseconds / 1000;
            lastFrameTime = DateTime.Now;

            foreach (Entity entity in entities)
            {
                entity.update(deltaTime);
                entity.Draw();
            }
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

        //these functions are for passing inputs into player entity
        //
        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    .OnKeyDown(e);
        //}

        //private void Window_KeyUp(object sender, KeyEventArgs e)
        //{
        //    .OnKeyUp(e);
        //}
    }

}