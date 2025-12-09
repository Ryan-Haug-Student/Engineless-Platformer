using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EnginelessPhysics.src.game.boards
{
    public static class Tiles
    {
        private static BitmapImage grappleImg = new BitmapImage(new Uri("src/game/sprites/interactables/SpaceFragment.png", UriKind.Relative));
        private static BitmapImage bgImg= new BitmapImage(new Uri("src/game/sprites/ground/bg.png", UriKind.Relative));

        private static BitmapImage groundSheet = new BitmapImage(new Uri("src/game/sprites/ground/tileMap.png", UriKind.Relative));

        static CroppedBitmap[] groundSprites =
        {
            new CroppedBitmap(groundSheet, new Int32Rect(0,  0, 16, 16)),   // row 0, col 0
            new CroppedBitmap(groundSheet, new Int32Rect(16, 0, 16, 16)),   // row 0, col 1
            new CroppedBitmap(groundSheet, new Int32Rect(32, 0, 16, 16)),   // row 0, col 2
            new CroppedBitmap(groundSheet, new Int32Rect(48, 0, 16, 16)),   // row 0, col 3

            new CroppedBitmap(groundSheet, new Int32Rect(0,  16, 16, 16)),  // row 1, col 0
            new CroppedBitmap(groundSheet, new Int32Rect(16, 16, 16, 16)),  // row 1, col 1
            new CroppedBitmap(groundSheet, new Int32Rect(32, 16, 16, 16)),  // row 1, col 2

            new CroppedBitmap(groundSheet, new Int32Rect(0,  32, 16, 16)),  // row 2, col 0
            new CroppedBitmap(groundSheet, new Int32Rect(16, 32, 16, 16)),  // row 2, col 1
            new CroppedBitmap(groundSheet, new Int32Rect(32, 32, 16, 16)),  // row 2, col 2

            new CroppedBitmap(groundSheet, new Int32Rect(0,  48, 16, 16)),  // row 3, col 0
            new CroppedBitmap(groundSheet, new Int32Rect(16, 48, 16, 16)),  // row 3, col 1
            new CroppedBitmap(groundSheet, new Int32Rect(32, 48, 16, 16)),  // row 3, col 2
        };

        public enum _tiles
        {
            AIR, BARRIER, GROUND, SPIKE, ROOMBA, GRAPPLE, COIN, FLAG
        }

        public static WriteableBitmap GetImage(this _tiles tile, int surrondings)
        {
            switch (tile)
            {
                case _tiles.GROUND:
                    return GetGroundSprite(surrondings);
                case _tiles.GRAPPLE:
                    return new WriteableBitmap(grappleImg);

                default:
                    return new WriteableBitmap(bgImg);
            }
        }



        private static WriteableBitmap GetGroundSprite(int tileIndex)
        {
            int spriteIndex = tileIndex switch
            {
                0 => 3,   // floating single
                1 => 8,   // bottom center
                2 => 10,  // floating left endcap
                3 => 7,   // bottom right
                4 => 1,   // top center
                5 => 5,   // middle center NA default center
                6 => 0,   // top left
                7 => 4,   // left wall
                8 => 12,  // floating right
                9 => 9,   // right bottom
                10 => 11, // floating center
                11 => 8,  // bottom center
                12 => 2,  // top right
                13 => 6,  // right wall
                14 => 1,  // top center

                _ => 5,
            };

            return new WriteableBitmap(groundSprites[spriteIndex]);
        }
    }
}
