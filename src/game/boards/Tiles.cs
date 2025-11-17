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
            AIR, GROUND, FIRE, GRAPPLE
        }

        public static Color GetBrush(this _tiles tile)
        {
            switch (tile)
            {
                case _tiles.GROUND:
                    return Colors.Green;
                case _tiles.FIRE:
                    return Colors.Red;
                case _tiles.GRAPPLE:
                    return Colors.Gray;

                default:
                    return Colors.Transparent;
            }
        }
    }
}
