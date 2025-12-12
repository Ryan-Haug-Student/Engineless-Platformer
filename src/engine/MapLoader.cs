using EnginelessPhysics.src.engine.Entities;
using EnginelessPhysics.src.game;
using EnginelessPhysics.src.game.boards;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EnginelessPhysics.src.engine
{
    public class MapLoader
    {
        private static BitmapImage cloud = new BitmapImage(new Uri("src/game/sprites/ground/cloud.png", UriKind.Relative));
        public static void LoadMap(int toLoad)
        {
            int windowHeight = (int)MainWindow.canvas.ActualHeight;
            int windowWidth = (int)MainWindow.canvas.ActualWidth;

            //get the correct board to load
            Tiles._tiles[,]? board = null;
            WorldData.tileScale = MathF.Ceiling(windowHeight / 12f);
            float tileSize = WorldData.tileScale;

            switch (toLoad)
            {
                case 0:
                    board = LevelZero.board;
                    break;
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
                PixelFormats.Pbgra32,   //pixel format, also color palette
                null                    //would be color palette
                );

            bitmap.Lock();
            //make the background solid light blue
            bitmap.FillRectangle(0, 0, (int)bitmap.Width, (int)bitmap.Height, (Color)ColorConverter.ConvertFromString("#9AE1F4"));

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    //only draw tiles that have content
                    if (board[y, x] != Tiles._tiles.AIR)
                    {
                        // bitmask: top=1, right=2, bottom=4, left=8
                        int tileIndex = 0;
                        if (y > 0 && board[y - 1, x] == Tiles._tiles.GROUND) tileIndex |= 1;         // top
                        if (x < cols - 1 && board[y, x + 1] == Tiles._tiles.GROUND) tileIndex |= 2;  // right
                        if (y < rows - 1 && board[y + 1, x] == Tiles._tiles.GROUND) tileIndex |= 4;  // bottom
                        if (x > 0 && board[y, x - 1] == Tiles._tiles.GROUND) tileIndex |= 8;         // left


                        //round positions to ensure that there is no gaps between tiles
                        float posX = MathF.Round(x * tileSize);
                        float posY = MathF.Round(y * tileSize);
                        WriteableBitmap tileSprite = board[y, x].GetImage(tileIndex);

                        int x1 = (int)posX;
                        int x2 = (int)(posX + tileSize);

                        int y1 = (int)posY;
                        int y2 = (int)(posY + tileSize);

                        bitmap.Blit(new Rect(x1, y1, x2 - x1, y2 - y1),
                            tileSprite,
                            new Rect(0, 0, tileSprite.PixelWidth, tileSprite.PixelHeight),
                            WriteableBitmapExtensions.BlendMode.Alpha);

                        switch (board[y, x])
                        {
                            case Tiles._tiles.GRAPPLE:
                                WorldData.grapplePoints.Add(new Vector2(posX, posY));
                                break;

                            case Tiles._tiles.SPIKE:
                                WorldData.entities.Add(new Spike(new Vector2(posX, posY)));
                                break;

                            case Tiles._tiles.ROOMBA:
                                WorldData.entities.Add(new Roomba(new Vector2(posX, posY)));
                                break;

                            case Tiles._tiles.FLAG:
                                WorldData.entities.Add(new Flag(new Vector2(posX, posY)));
                                break;

                            case Tiles._tiles.COIN:
                                WorldData.entities.Add(new Coin(new Vector2(posX, posY)));
                                break;


                            default: //assume ground
                                WorldData.staticEntities.Add(new Tile(new Vector2(posX, posY), tileSize));
                                break;
                        }
                    }
                    else // add some clouds
                    {
                        float x1 = x * tileSize;
                        float y1 = y * tileSize;

                        float x2 = x1 + tileSize;
                        float y2 = y1 + tileSize;

                        // if its air above y level 6, give a 1/75 chance to draw a cloud
                        if (new Random().Next(0, 75) == 1 && y < 6)
                            bitmap.Blit(new Rect(x1, y1, x2 - x1, y2 - y2),
                                new WriteableBitmap(cloud),
                                new Rect(0, 0, 8, 16),
                                WriteableBitmapExtensions.BlendMode.Alpha);
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
