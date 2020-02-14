using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public bool exists;
    public Player[] bridge;
    public int[] pos;
    public readonly int UP = 0;
    public readonly int RIGHT= 1;
    public readonly int DOWN= 2;
    public readonly int LEFT= 3;

    public Tile(int row, int col, bool ex)
    {
        pos= new int[] {row, col};
        exists=ex;
        bridge= new Player[4];
    }    
    public bool TakeBridge(int pos)
    {
        if(bridge[pos]==null)
            return false;
        bridge[pos]=null;
        return true;
    }
    public bool PutBridge(int pos, Player briPl)
    {
        if(bridge[pos]==null){
            bridge[pos]=briPl;
            return true;
        }
        return false;

    }
    
}
