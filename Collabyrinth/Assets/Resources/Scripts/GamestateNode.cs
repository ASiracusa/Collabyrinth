using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateNode : MonoBehaviour
{
    Tile[][] board;
    int length;

    public GamestateNode(int length)
    {
        this.length = length;
        board = new Tile[length][length];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                board[i][j] = new Tile(i, j, true);
            }
        }
    }

}
