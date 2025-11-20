using EnginelessPhysics.src.engine;
using EnginelessPhysics.src.engine.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnginelessPhysics.src.game
{
    public static class GameManager
    { 
        public static void LoadScene(int toLoad)
        {
            MainWindow.canvas.RenderTransform = MainWindow.cameraTransform;

            MapLoader.LoadMap(toLoad);

            //physical entities go here
            WorldData.entities.Add(new Player(
                new Vector2(50, 100),  //starting pos
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

#pragma warning disable CS8602 // Dereference of a possibly null reference. wont be null because im creating new timers
            MainWindow.gameTimer.Stop(); MainWindow.gameTimer.Reset();

            MainWindow.physicsRunning = false;
        }
    }
}
