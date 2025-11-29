using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnginelessPhysics.src.game.boards;

namespace EnginelessPhysics.src.game.boards
{
    public class LevelOne
    {
        private static Tiles._tiles e = Tiles._tiles.AIR;
        private static Tiles._tiles g = Tiles._tiles.GROUND;

        //enemies
        private static Tiles._tiles f = Tiles._tiles.FIRE;
        private static Tiles._tiles r = Tiles._tiles.ROOMBA;

        //interactables
        private static Tiles._tiles h = Tiles._tiles.GRAPPLE;

        //temp board until tile types are determined
        public static Tiles._tiles[,] board = new Tiles._tiles[,]
        {
            {g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g},
            {e, e, e, e, e, g, g, g, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, h, e, e, e, e},
            {e, e, e, e, e, g, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, g, e},
            {e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, g, g, g, g, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, h, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, h, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, g, g, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, g, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, e, e, e, e, e, e, e, e, e, f, r, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {g, r, e, e, e, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, e, e, e, e, e, e, g, g, g, g, g, g, g, g, g, g, g, e},
            {g, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {e, g, g, g, g, g, g, e, e, e, e, f, f, f, e, e, e, g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e},
            {g, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, g}
        };
    }
}
