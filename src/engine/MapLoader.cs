using EnginelessPhysics.src.game.boards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine
{
    public class MapLoader
    {
        public static void LoadMap(int toLoad)
        {
            int cols = LevelOne.board.GetLength(1);
            int rows = LevelOne.board.GetLength(0);

            //Get a tile scale based on number of rows in maps
            float tileSize = MathF.Ceiling((float)MainWindow.canvas.ActualHeight / rows);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    //only draw tiles that have content
                    if (LevelOne.board[y, x] != Tiles._tiles.AIR)
                    {
                        float posX = MathF.Round(x * tileSize);
                        float posY = MathF.Round(y * tileSize);
                        Brush tileBrush = LevelOne.board[y, x].GetBrush();

                        //create a new tile to draw to the screen
                        Entity tile = new Entity
                        {
                            position = new Vector2(posX, posY),
                            sprite = new Rectangle
                            {
                                Fill = tileBrush,
                                Width = MathF.Round(tileSize),
                                Height =MathF.Round(tileSize)
                            }
                        };

                        MainWindow.canvas.Children.Add(tile.sprite);
                        tile.Draw();
                    }
                }
            }
        }
    }
}
