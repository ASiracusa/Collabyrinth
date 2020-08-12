using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    public GameObject wall;
    private Player owner;

    public Wall(Player p, GameObject g)
    {
        wall = g;
        owner = p;
    }

    public Player getPlayer()
    {
        return owner;
    }
}
