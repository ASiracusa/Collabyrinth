using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int x;
    public int y;
    public GameObject tile;
    public GameObject[] bridges;
    public bool encountered;
    public int predecessor;

    public Tile(int ix, int iy, GameObject tile)
    {
        x = ix;
        y = iy;
        this.tile = tile;
        bridges = new GameObject[4];
        encountered = false;

    }
    //Creates a tile and sets its coordinates.The Bridges are set in the second go around of the Gameboard.

    public void setBridge(int direction, GameObject bridge)
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
