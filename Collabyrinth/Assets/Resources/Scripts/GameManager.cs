using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
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


        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y - 1; j++)
            {
                GameObject g = Instantiate(bridge, new Vector3(i * 2, .5f, j * 2 + 1), Quaternion.identity);
                g.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x - 1; j++)
            {
                GameObject g = Instantiate(bridge, new Vector3(j * 2 + 1, .5f, i * 2), Quaternion.Euler(0, 90, 0));
                g.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        bridges = new List<Bridge>();
        curPlayer = 0;
        bridgePlacingPhase = true;
    }

    private void FixedUpdate()
    {
        Player currPlayer = players[curPlayer];
        int bridgesOwned = 0;
        foreach (Bridge currBridge in bridges)
        {
            if (currBridge.getPlayer().Equals(currPlayer))
            {
                bridgesOwned++;
            }
        }
        if (currPlayer.points > 3)
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
                        Debug.Log("Hit Something");
                        if (hit.transform.gameObject.tag.Equals("Bridge"))
                        {
                            Debug.Log("It was a bridge");
                            GameObject g = hit.transform.gameObject;
                            if (!g.GetComponent<MeshRenderer>().enabled)
                            {
                                if (bridgesOwned < 3)
                                {
                                    Debug.Log("Dropping a bridge here");
                                    g.GetComponent<MeshRenderer>().enabled = true;
                                    bridges.Add(new Bridge(currPlayer, g));
                                }
                            }
                            else
                            {
                                foreach (Bridge b in bridges)
                                {
                                    if (b.bridge.Equals(g) && (b.getPlayer().Equals(currPlayer)))
                                    {
                                        Debug.Log("Picking up my bridge");
                                        g.GetComponent<MeshRenderer>().enabled = false;
                                        bridges.Remove(b);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.W) && currPlayer.location.bridges[0] != null)
                {
                    currPlayer.location = map[currPlayer.location.x, currPlayer.location.y - 1];
                    if (currPlayer.goal.Equals(currPlayer.location))
                    {
                        currPlayer.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) && currPlayer.location.bridges[1] != null)
                {
                    currPlayer.location = map[currPlayer.location.x + 1, currPlayer.location.y];
                    if (currPlayer.goal.Equals(currPlayer.location))
                    {
                        currPlayer.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.S) && currPlayer.location.bridges[2] != null)
                {
                    currPlayer.location = map[currPlayer.location.x, currPlayer.location.y + 1];
                    if (currPlayer.goal.Equals(currPlayer.location))
                    {
                        currPlayer.points++;
                        //p.goal = new Tile()
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) && currPlayer.location.bridges[3] != null)
                {
                    currPlayer.location = map[currPlayer.location.x - 1, currPlayer.location.y];
                    if (currPlayer.goal.Equals(currPlayer.location))
                    {
                        currPlayer.points++;
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