using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int playerNum;
    int points;
    Tile location;
    Tile goal;

    public Player(int playerNum, Tile location)
    {
        this.playerNum = playerNum;
        points = 0;
        this.location = location;
    }
}
