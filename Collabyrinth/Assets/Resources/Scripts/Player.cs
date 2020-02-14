using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int[] pos;
    public bool isAI;
    public string name;
    public int[] goal;
    public int numBridges;
    public int points;
    public Player(int xPos, int yPos, bool AI, string name, int goalX, int goalY)
    {
        pos=new int[xPos,yPos];
        isAI=AI;
        this.name=name;
        goal=new int[goalX,goalY];
        numBridges=3;
        points=0;
    }
    public AddPoints()
    {
        points++;
    }

}
