using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int playerNum;
    public int points;
    public Tile location;
    public Tile goal;

    public Player(int playerNum, Tile location)
    {
        this.playerNum = playerNum;
        points = 0;
        this.location = location;
    }
}
