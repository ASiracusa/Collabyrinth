using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge
{
    public GameObject bridge;
    private Player owner;

    public Bridge(Player p, GameObject g)
    {
        bridge = g;
        owner = p;
    }

    public Player getPlayer()
    {
        return owner;
    }
}
