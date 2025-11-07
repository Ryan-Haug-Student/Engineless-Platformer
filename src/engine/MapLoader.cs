using EnginelessPhysics.src.engine.Entities;
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
            //get the correct board to load
            Tiles._tiles[,]? board = null;
            switch (toLoad)
            {
                case 1:
                    board = LevelOne.board;
                    break;
                case 2:
                    board = LevelTwo.board;
                    break;
                case 3:
                    board = LevelThree.board;
                    break;

                default:
                    throw new Exception("Level not found");
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int cols = board.GetLength(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            int rows = board.GetLength(0);

            //Get a tile scale based on number of rows in maps
            float tileSize = MathF.Ceiling((float)MainWindow.canvas.ActualHeight / rows);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    //only draw tiles that have content
                    if (board[y, x] != Tiles._tiles.AIR)
                    {
                        //round positions to ensure that there is no gaps between tiles
                        float posX = MathF.Round(x * tileSize);
                        float posY = MathF.Round(y * tileSize);
                        Brush tileBrush = LevelOne.board[y, x].GetBrush();

                        //create a new tile to draw to the screen
                        Tile tile = new Tile(new Vector2(posX, posY))
                        {
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
