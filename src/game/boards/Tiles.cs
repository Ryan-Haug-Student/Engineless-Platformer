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
                1 => 1,   // top center
                2 => 6,   // right wall
                3 => 2,   // top right
                4 => 8,   // bottom center
                5 => 5,   // surrounded
                6 => 9,   // bottom right
                7 => 5,   // surrounded
                8 => 4,   // left wall
                9 => 0,   // top left
                10 => 5,   // left + right → surrounded
                11 => 5,   // top + left + right → surrounded
                12 => 7,   // bottom left
                13 => 5,   // top + left + bottom → surrounded
                14 => 5,   // bottom + left + right → surrounded
                15 => 5,   // all sides → surrounded
                _ => 5,
            };

            return new WriteableBitmap(groundSprites[spriteIndex]);
        }
    }
}
