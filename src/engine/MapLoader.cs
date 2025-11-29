using EnginelessPhysics.src.engine.entities;
using EnginelessPhysics.src.engine.Entities;
using EnginelessPhysics.src.game.boards;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EnginelessPhysics.src.engine
{
    public class MapLoader
    {
        public static void LoadMap(int toLoad)
        {
            int windowHeight =(int) MainWindow.canvas.ActualHeight;
            int windowWidth = (int) MainWindow.canvas.ActualWidth;

            //get the correct board to load
            Tiles._tiles[,]? board = null;
            WorldData.tileScale = MathF.Ceiling(windowHeight / 12f);
            float tileSize = WorldData.tileScale;

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

#pragma warning disable CS8602 // Dereference of a possibly null reference. wont be null due to thrown exception
            int cols = board.GetLength(1);
            int rows = board.GetLength(0);

            WriteableBitmap bitmap = new WriteableBitmap(
                cols * (int)tileSize,   //width
                rows * (int)tileSize,   //height
                96, 96,                 //dpi
                PixelFormats.Bgr32,     //pixel format, also color palette
                null                    //would be color palette
                );

            bitmap.Lock();
            //make the background solid light blue
            bitmap.FillRectangle(0, 0, (int)bitmap.Width, (int)bitmap.Height, Colors.LightBlue);

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
                        Color tileColor = board[y, x].GetBrush();

                        int x1 = (int) posX;
                        int x2 = (int) (posX + tileSize);

                        int y1 = (int) posY;
                        int y2 = (int) (posY + tileSize);

                        bitmap.FillRectangle(x1, y1, x2, y2, tileColor);

                        //add entities based on tile type
                        if (board[y, x] == Tiles._tiles.GRAPPLE)
                            WorldData.grapplePoints.Add(new Vector2(posX, posY));
                        else if (board[y, x] == Tiles._tiles.FIRE)
                            WorldData.entities.Add(new Spike(new Vector2(posX, posY)));
                        else
                            WorldData.staticEntities.Add(new Tile(new Vector2(posX, posY), tileSize));
                    }
                }
            }

            bitmap.Unlock();

            //create an image out of the bitmap and draw it to the canvas
            Image BMImage = new Image();
            BMImage.Source = bitmap;

            MainWindow.canvas.Children.Add(BMImage);
            Panel.SetZIndex(BMImage, -1);

            Canvas.SetLeft(BMImage, 0);
            Canvas.SetTop(BMImage, 0);
        }
    }
}
