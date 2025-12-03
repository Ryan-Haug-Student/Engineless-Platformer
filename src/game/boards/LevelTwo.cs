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
        private static Tiles._tiles b = Tiles._tiles.BARRIER;
        private static Tiles._tiles g = Tiles._tiles.GROUND;

        //enemies
        private static Tiles._tiles s = Tiles._tiles.SPIKE;
        private static Tiles._tiles r = Tiles._tiles.ROOMBA;

        //interactables
        private static Tiles._tiles h = Tiles._tiles.GRAPPLE;
        private static Tiles._tiles c = Tiles._tiles.COIN;
        private static Tiles._tiles w = Tiles._tiles.FLAG;

        //temp board until tile types are determined
        public static Tiles._tiles[,] board = new Tiles._tiles[,]
        {
            {g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g},
            {e, e, e, e, e, g, g, g, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, h, e, e, e, e},
            {e, e, e, e, e, g, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, e},
            {e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, g, g, g, g, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, h, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, g, g, g, e, e, h, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, w, e, e, e},
            {g, e, e, e, e, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, g, g, g, g, g, g, e, e, e, e, s, s, s, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g}
        };
    }
}
