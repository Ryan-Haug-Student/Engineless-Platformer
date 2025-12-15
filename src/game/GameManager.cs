using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using System.Numerics;
using System.Windows.Controls;

namespace EnginelessPhysics.src.game
{
    public static class GameManager
    {
        private static int currentLevel = 0;

        //this is to be set manually based on number of levels for a win screen
        private static int levelCount = 3;

        public static int currencyCount = 0;

        // UI components
        public static TextBlock currencyDisp = new TextBlock { FontSize = 32 };
        public static TextBlock livesDisp = new TextBlock { FontSize = 32 };
        public static TextBlock levelDisp = new TextBlock { FontSize = 32 };

        public static void LevelCompleted()
        {
            if (currentLevel < levelCount)
                MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                { //force push to main thread becaues usually called by phyiscal entity
                    LoadScene(currentLevel + 1);
                    UpdateUI();
                }));
            else
                MainWindow.LoadWinMenu();
        }

        public static void LoadScene(int toLoad)
        {
            ClearScene();
            MainWindow.canvas.RenderTransform = MainWindow.cameraTransform;

            MapLoader.LoadMap(toLoad);
            currentLevel = toLoad;

            //physical entities go here
            WorldData.entities.Add(new Player(
                new Vector2(50, 300),  //starting pos
                new Vector2(35, 70))); //scale

            MainWindow.player = WorldData.entities.OfType<Player>().First();

            foreach (PhysicalEntity entity in WorldData.entities)
                MainWindow.canvas.Children.Add(entity.sprite);

            LoadUI();

            MainWindow.physicsRunning = true;
        }

        public static void ClearScene()
        {
            MainWindow.canvas.Children.Clear();
            MainWindow.uiCanvas.Children.Clear();
            MainWindow.player = null;

            WorldData.entities.Clear();
            WorldData.staticEntities.Clear();
            WorldData.grapplePoints.Clear();

#pragma warning disable CS8602
            MainWindow.gameTimer.Stop(); MainWindow.gameTimer.Reset();

            MainWindow.physicsRunning = false;
        }

        public static void LoadUI()
        {
            UpdateUI();

            Canvas.SetLeft(currencyDisp, 10);
            Canvas.SetTop(currencyDisp, 10);
            MainWindow.uiCanvas.Children.Add(currencyDisp);

            Canvas.SetLeft(levelDisp, 10);
            Canvas.SetTop(levelDisp, 50);
            MainWindow.uiCanvas.Children.Add(levelDisp);

            Canvas.SetLeft(livesDisp, 10);
            Canvas.SetTop(livesDisp, 90);
            MainWindow.uiCanvas.Children.Add(livesDisp);
        }

        public static void UpdateUI()
        {
            currencyDisp.Text = $"Score - {currencyCount}";
            levelDisp.Text = $"Level - {currentLevel}";
            livesDisp.Text = $"Lives - {MainWindow.player.lives}";
        }
    }
}
