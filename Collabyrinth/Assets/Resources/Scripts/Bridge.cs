using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge
{
    private Player owner;

    public Bridge(Player p)
    {
        owner = p;
    }

    public Player getPlayer()
    {
        return owner;
    }
}
