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

        public MainWindow()
        {
            InitializeComponent();
            Content = canvas;
            Title = "Non-Physical Platformer";

            //wait to load the map until the window is initialized
            Loaded += (s, e) => MapLoader.LoadMap(0);
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