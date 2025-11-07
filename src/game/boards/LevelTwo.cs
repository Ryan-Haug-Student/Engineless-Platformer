using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginelessPhysics.src.game.boards
{
    public class LevelTwo
    {
        private static Tiles._tiles e = Tiles._tiles.AIR;
        private static Tiles._tiles g = Tiles._tiles.GROUND;
        private static Tiles._tiles f = Tiles._tiles.FIRE;

        //temp board until tile types are determined
        public static Tiles._tiles[,] board = new Tiles._tiles[,]
        {
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, g, g, g, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, g, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g},
            {e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, g, g, g, g, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, g, g, g, g, g, g, e, e, e, e, f, f, f, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {g, g, g, g, g, g, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g}
        };
    }
}
