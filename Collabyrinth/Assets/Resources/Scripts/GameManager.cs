using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject player;
    public GameObject bridge;
    public int numPlayers;
    private Tile[,] map;
    private Player[] players;
    private List<Bridge> bridges;
    private int curPlayer;
    private bool bridgePlacingPhase;

    void Start()
    {
        int x = 5;
        int y = 5;
        map = new Tile[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                map[i, j] = new Tile(i, j);
                Instantiate(tile, new Vector3(i * 2, 0, j * 2), Quaternion.identity);
            }
        }
        players = new Player[numPlayers];
        players[0] = new Player(0, map[0, 0], Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity));
        if (players.Length > 2)
        {
            players[1] = new Player(1, map[x, y], Instantiate(player, new Vector3(x * 2, 1, y * 2), Quaternion.identity));
        }
        if (players.Length > 3)
        {
            players[2] = new Player(2, map[0, y], Instantiate(player, new Vector3(0, 1, y * 2), Quaternion.identity));
        }
        if (players.Length > 4)
        {
            players[3] = new Player(3, map[x, 0], Instantiate(player, new Vector3(x * 2, 1, 0), Quaternion.identity));
        }

        bridges = new List<Bridge>();
        curPlayer = 0;
        bridgePlacingPhase = true;
    }

    private void FixedUpdate()
    {
        Player p = players[curPlayer];
        if (p.points > 3)
        {
            Debug.Log(players[curPlayer] + "wins");

        }
        else
        {
            if (bridgePlacingPhase)
            {
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.W) && p.location.bridges[0] != null)
                {
                    p.location = map[p.location.x, p.location.y - 1];
                    if (p.goal.Equals(p.location))
                    {
                        p.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) && p.location.bridges[1] != null)
                {
                    p.location = map[p.location.x + 1, p.location.y];
                    if (p.goal.Equals(p.location))
                    {
                        p.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.S) && p.location.bridges[2] != null)
                {
                    p.location = map[p.location.x, p.location.y + 1];
                    if (p.goal.Equals(p.location))
                    {
                        p.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) && p.location.bridges[3] != null)
                {
                    p.location = map[p.location.x - 1, p.location.y];
                    if (p.goal.Equals(p.location))
                    {
                        p.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    bridgePlacingPhase = true;
                    curPlayer++;
                    if (curPlayer > numPlayers - 1)
                        curPlayer = 0;
                }
            }

        }

    }
}