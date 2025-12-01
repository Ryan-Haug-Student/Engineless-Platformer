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
            AIR, BARRIER, GROUND, FIRE, ROOMBA, GRAPPLE, COIN
        }

        public static Color GetBrush(this _tiles tile)
        {
            switch (tile)
            {
                case _tiles.GROUND:
                    return Colors.Green;
                case _tiles.GRAPPLE:
                    return Colors.Gray;

                default://return lightblue as "transparent"
                    return Colors.LightBlue;
            }
        }
    }
}
