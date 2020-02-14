using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject player;
    public int playerNum;
    public int points;
    public Tile location;
    public Tile goal;

    public Player(int playerNum, Tile location, GameObject g)
    {
        this.playerNum = playerNum;
        points = 0;
        this.location = location;
        player = g;
    }

    public void SetGoal(Tile goal)
    {
        this.goal = goal;
    }

}
