using System;
using UnityEngine;

public class GamestateNode : IComparable<GamestateNode>
{
    Tile[,] board;
    Player[] players;
    int length;
    int depth;
    int cost;
    GamestateNode parent;

    public GamestateNode(GamestateNode parent, Player[] players, int length, int depth)
    {
        this.parent = parent;
        this.players = players;
        this.depth = depth;
        this.cost = 0;
        this.length = length;
        board = new Tile[length, length];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                board[i, j] = new Tile(i, j, true, null);
            }
        }
    }

    public int CompareTo(GamestateNode other)
    {
        if (cost > other.cost)
            return 1;

        else if (cost == other.cost)
            return 0;

        return -1;
    }

    //bridge = -1 --> move player, bridge != 1 --> move bridge
    public GamestateNode next(Player p, int[,] pos, int moveBridge)
    {
       

    }

    //return to where depth = 1, return parent at this depth
    public GamestateNode top()
    {
        GamestateNode p = parent;
        if (p != null && p.depth != 1)
            p = p.parent;

        return p;
    }

}
