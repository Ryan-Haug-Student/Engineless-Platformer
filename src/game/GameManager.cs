using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnginelessPhysics.src.game
{
    public static class GameManager
    {
        private static int currentLevel = 0;
        public static void LevelCompleted()
        {
            MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() => {
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
