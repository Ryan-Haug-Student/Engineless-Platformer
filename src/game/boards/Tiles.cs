using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Navigation;

namespace EnginelessPhysics.src.game.boards
{
    public static class Tiles
    {
        public enum _tiles
        {
            //no color
            AIR,

            //green
            GROUND,

            //red
            FIRE
        }

        public static Color GetBrush(this _tiles tile)
        {
            switch (tile)
            {
                case _tiles.GROUND:
                    return Colors.Green;
                case _tiles.FIRE:
                    return Colors.Red;

                default:
                    return Colors.Transparent;
            }
        }
    }
}
