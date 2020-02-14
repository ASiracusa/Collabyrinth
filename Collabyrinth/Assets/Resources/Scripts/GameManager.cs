using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int x;
    public int y;
    public GameObject tile;
    public GameObject player;
    public GameObject bridge;
    public int numPlayers;
    private Tile[,] map;
    private Player[] players;
    private List<Bridge> bridges;
    private int curPlayer;
    private bool bridgePlacingPhase;
    private List<Tile> validTiles;
    private int bridgesOwned;

    void Start()
    {
        map = new Tile[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                map[i, j] = new Tile(i, j, Instantiate(tile, new Vector3(i * 2, 0, j * 2), Quaternion.identity));
            }
        }
        players = new Player[numPlayers];
        players[0] = new Player(0, map[0, 0], Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity));
        curPlayer = 0;
        SetNewGoal();
        Debug.Log(players[0].goal.x + "," + players[0].goal.y);
        if (players.Length >= 2)
        {
            players[1] = new Player(1, map[x - 1, y - 1], Instantiate(player, new Vector3(x * 2, 1, y * 2), Quaternion.identity));
            curPlayer = 1;
            SetNewGoal();
        }
        if (players.Length >= 3)
        {
            players[2] = new Player(2, map[0, y - 1], Instantiate(player, new Vector3(0, 1, y * 2), Quaternion.identity));
            curPlayer = 2;
            SetNewGoal();
        }
        if (players.Length >= 4)
        {
            players[3] = new Player(3, map[x - 1, 0], Instantiate(player, new Vector3(x * 2, 1, 0), Quaternion.identity));
            curPlayer = 3;
            SetNewGoal();
        }


        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y - 1; j++)
            {
                GameObject g = Instantiate(bridge, new Vector3(i * 2, .5f, j * 2 + 1), Quaternion.identity);
                g.GetComponent<MeshRenderer>().enabled = false;
                map[i, j].bridges[2] = g;
                map[i, j + 1].bridges[0] = g;
            }
        }
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x - 1; j++)
            {
                GameObject g = Instantiate(bridge, new Vector3(j * 2 + 1, .5f, i * 2), Quaternion.Euler(0, 90, 0));
                g.GetComponent<MeshRenderer>().enabled = false;
                map[j, i].bridges[3] = g;
                map[j + 1, i].bridges[1] = g;
            }
        }

        bridges = new List<Bridge>();
        curPlayer = 0;
        bridgePlacingPhase = true;
    }

    private void FixedUpdate()
    {
        Player p = players[curPlayer];
        bridgesOwned = 0;
        if (bridges.Count > 0)
        {
            foreach (Bridge b in bridges)
            {
                if (b.getPlayer().Equals(p))
                {
                    bridgesOwned++;
                }
            }
        }
        if (p.points > 3)
        {
            Debug.Log(players[curPlayer] + "wins");
        }
        else
        {
            if (bridgePlacingPhase)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.transform.gameObject.tag.Equals("Bridge"))
                        {
                            GameObject g = hit.transform.gameObject;
                            if (!g.GetComponent<MeshRenderer>().enabled)
                            {
                                if (bridgesOwned < 3)
                                {
                                    g.GetComponent<MeshRenderer>().enabled = true;
                                    bridges.Add(new Bridge(p, g));
                                }
                            }
                            else
                            {
                                foreach (Bridge b in bridges)
                                {
                                    if (b.bridge.Equals(g) && (b.getPlayer().Equals(p)))
                                    {
                                        g.GetComponent<MeshRenderer>().enabled = false;
                                        bridges.Remove(b);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.E) && bridgesOwned == 3)
                {
                    bridgePlacingPhase = false;
                    Debug.Log("Switched to movement");
                    validTiles = CheckPath(players[curPlayer].location, new List<Tile>());
                }
            }
            else
            {
                /*
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
                }*/

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.transform.gameObject.tag.Equals("Tile"))
                        {
                            GameObject g = hit.transform.gameObject;
                            foreach (Tile t in validTiles)
                            {
                                if (t.tile.Equals(g))
                                {
                                    p.location = t;
                                    p.player.transform.position = new Vector3(t.x * 2, p.player.transform.position.y, t.y * 2);
                                }
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (p.location.Equals(p.goal))
                    {
                       p.points++;
                        SetNewGoal();
                    }
                    bridgePlacingPhase = true;
                    curPlayer++;
                    if (curPlayer > numPlayers - 1)
                        curPlayer = 0;
                    Debug.Log("Next player, switched to bridges");
                }

            }

        }

    }

    private List<Tile> CheckPath (Tile tile, List<Tile> validTiles)
    {

        // Add this current tile to the list of possible tiles to move to
        // and set it to encountered to prevent infinite recursion
        validTiles.Add(tile);
        map[tile.x, tile.y].encountered = true;

        // Go through each direction and recurse if:
        // a) connected by a bridge and
        // b) not already visited
        // After all is done, all of the connected Tiles will be in validTiles
        if (tile.y!=0 && map[tile.x, tile.y].bridges[0] != null && map[tile.x, tile.y].bridges[0].GetComponent<MeshRenderer>().enabled && !map[tile.x, tile.y - 1].encountered)

        {
            map[tile.x, tile.y - 1].predecessor = 2;
            CheckPath(map[tile.x, tile.y - 1], validTiles);
        }

        if (tile.x!=x-1 && map[tile.x, tile.y].bridges[1] != null && map[tile.x, tile.y].bridges[1].GetComponent<MeshRenderer>().enabled && !map[tile.x + 1, tile.y].encountered)

        {
            map[tile.x + 1, tile.y].predecessor = 3;
            CheckPath(map[tile.x + 1, tile.y], validTiles);
        }

        if (tile.y!=y-1 && map[tile.x, tile.y].bridges[2] != null && map[tile.x, tile.y].bridges[2].GetComponent<MeshRenderer>().enabled && !map[tile.x, tile.y + 1].encountered)

        {
            map[tile.x, tile.y + 1].predecessor = 0;
            CheckPath(map[tile.x, tile.y + 1], validTiles);
        }

        if (tile.x!=0 && map[tile.x, tile.y].bridges[3] != null && map[tile.x, tile.y].bridges[3].GetComponent<MeshRenderer>().enabled && !map[tile.x - 1, tile.y].encountered)

        {
            map[tile.x - 1, tile.y].predecessor = 1;
            CheckPath(map[tile.x - 1, tile.y], validTiles);
        }
        foreach (Tile t in validTiles)
            Debug.Log(t.x + ", " + t.y);
        return validTiles;
    }

    public void SetNewGoal()
    {
        System.Random r = new System.Random();
        int randx = r.Next(0, map.Length/y);
        int randy = r.Next(0, map.Length/x);
        while (randx == players[curPlayer].location.x && randy == players[curPlayer].location.y)
        {
            randx = r.Next(0, map.Length/y);
            randy = r.Next(0, map.Length/x);
        }

        players[curPlayer].SetGoal(map[randx, randy]);
    }
}