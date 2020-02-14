using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int x;
    public int y;
    public Bridge[] bridges;

    public Tile(int ix, int iy)
    {
        x = ix;
        y = iy;
        bridges = new Bridge[4];

    }
    //Creates a tile and sets its coordinates.The Bridges are set in the second go around of the Gameboard.

    public void setBridge(int direction, Bridge bridge)
    {
        bridges[direction] = bridge;
    }
    //Sets the bridge spot to the given Bridge.Should be called next to the paired Tile.



    /*
    public Bridge getBridge(int direction)
    {
        return bridges[direction];
    }
    //Gets the bridge spot to the given Bridge.Should be called next to the paired Tile.
    */
}
