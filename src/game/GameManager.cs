using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using System.Numerics;

namespace EnginelessPhysics.src.game
{
    public static class GameManager
    {
        private static int currentLevel = 0;

        //this is to be set manually based on number of levels for a win screen
        private static int levelCount = 3;
        public static void LevelCompleted()
        {
            if (currentLevel < levelCount)
                MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LoadScene(currentLevel + 1);
                }));
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

            MainWindow.physicsRunning = true;
        }

        public static void ClearScene()
        {
            MainWindow.canvas.Children.Clear();
            MainWindow.player = null;

            WorldData.entities.Clear();
            WorldData.staticEntities.Clear();
            WorldData.grapplePoints.Clear();

#pragma warning disable CS8602
            MainWindow.gameTimer.Stop(); MainWindow.gameTimer.Reset();

            MainWindow.physicsRunning = false;
        }
    }
}
