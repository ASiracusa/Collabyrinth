﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject player;
    public GameObject bridge;
    public int numPlayers;

    void Start()
    {
        int x = 5;
        int y = 5;
        Tile[, ] map = new Tile[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                map[i, j] = new Tile(i, j);
                Instantiate(tile, new Vector3(i * 2, 0, j * 2), Quaternion.identity);
            }
        }
        Player[] players = new Player[numPlayers];
        players[0] = new Player(1, map[0, 0]);
        Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        if (players.Length > 2)
        {
            players[1] = new Player(2, map[x, y]);
            Instantiate(player, new Vector3(x * 2, 1, y * 2), Quaternion.identity);
        }
        if (players.Length > 3)
        {
            players[2] = new Player(3, map[0, y]);
            Instantiate(player, new Vector3(0, 1, y * 2), Quaternion.identity);
        }
        if (players.Length > 4)
        {
            players[3] = new Player(3, map[x, 0]);
            Instantiate(player, new Vector3(x * 2, 1, 0), Quaternion.identity);
        }


    }

    private void FixedUpdate()
    {
        
    }

}
