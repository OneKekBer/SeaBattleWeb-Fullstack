using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeaBattle.Values;

public readonly record struct Coordinates(int X, int Y)
{
    public static Coordinates operator +(Coordinates coord1, Coordinates coord2)
    {
        return new Coordinates(coord1.X + coord2.X, coord1.Y + coord2.Y);
    }
}
