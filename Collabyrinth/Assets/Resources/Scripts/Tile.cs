using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Player player;
    bool exists;
    //Player[] bridge
    int[] pos;

    public Tile(int row, int col, bool ex)
    {
        pos={x,y};
        exists=ex;
    }    
    public void TakeBridge()
    {}
    public void PutBridge()
    {}
    public void RemovePlayer()
    {}
    public void PutPlayer(){}
    
}
