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
    }/*
    public ArrayList<GamestateNode> GetAllMoves(Player p)
    {
        ArrayList<GamestateNode> moves=new ArrayList();
        for(int i;i<length;i++)
        {
            GamestateNode temp = //somthng
            for(int j;j<length;j++)
            {
                temp//
                for(int k;k<length;k++)
                {
                    temp//
                    while(!moves.Contains(temp)){
                        moves.
                    }
                }
            }
        }
    }*/

    //bridge = -1 --> move player, bridge != 1 --> move bridge
    /*
    public GamestateNode next(Player p, int[] pos, int moveBridge)
    {
       GamestateNode copy=new Tile[length,length];
       for(int i=0;i<length;i++)
       {
           for(int j=0;j<length;j++)
           {
               copy[i, j] = board[i, j];
           }
       }
        if(moveBridge!=-1){
            copy[pos[0],pos[1]]=copy[pos[0],pos[1]].TakeBridge(moveBridge);
            if(!copy[pos[0],pos[1]].TakeBridge(moveBridge))
                copy[pos[0],pos[1]]=copy[pos[0],pos[1]].PutBridge(moveBridge, p);
        }
    }
    */

    //return to where depth = 1, return parent at this depth
    public GamestateNode top()
    {
        GamestateNode p = parent;
        while (p != null && p.depth != 1)
            p = p.parent;

        return p;
    }

}
