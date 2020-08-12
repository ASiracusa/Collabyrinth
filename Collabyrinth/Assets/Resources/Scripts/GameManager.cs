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
    public GameObject wall;
    public int numPlayers;
    private Tile[,] map;
    private Player[] players;
    private List<Bridge> bridges;
    private List<Wall> walls;
    private int curPlayer;
    private bool bridgePlacingPhase;
    private int bridgesOwned;
    private int wallsOwned;
    private List<Tile> validTiles;
    private System.Random r;
    void Start()
    {
        r = new System.Random();
        map = new Tile[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject g = Instantiate(tile, new Vector3(i * 2, -20, j * 2), Quaternion.identity);
                map[i, j] = new Tile(i, j, g);
                Material[] mats = g.GetComponent<MeshRenderer>().materials;
                mats[0] = Resources.Load("Shaders/TowerMat") as Material;
                g.GetComponent<MeshRenderer>().materials = mats;
                StartCoroutine(RaiseTile(g, (i * (float)Math.Sqrt(map.Length) + j) / 10));
            }
        }
        players = new Player[numPlayers];
        if (players.Length >= 1) {
        GameObject g = Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        Material[] mats = g.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
        mats[0] = Resources.Load("Shaders/Player1Mat") as Material;
        g.transform.GetChild(0).GetComponent<MeshRenderer>().materials = mats;
        players[0] = new Player(0, map[0, 0], g);
        curPlayer = 0;
        SetNewGoal();
        Material[] goalmats = players[0].goal.tile.GetComponent<MeshRenderer>().materials;
        goalmats[0] = Resources.Load("Shaders/Player1Mat") as Material;
        players[0].goal.tile.GetComponent<MeshRenderer>().materials = goalmats;
        }
        Debug.Log("Goal" + players[0].goal.x + "," + players[0].goal.y);
        if (players.Length >= 2)
        {
            GameObject go = Instantiate(player, new Vector3((x - 1) * 2, 1, (y - 1) * 2), Quaternion.identity);
            Material[] mats = go.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            mats[0] = Resources.Load("Shaders/Player2Mat") as Material;
            go.transform.GetChild(0).GetComponent<MeshRenderer>().materials = mats;
            players[1] = new Player(1, map[x - 1, y - 1], go);
            curPlayer = 1;
            SetNewGoal();
            Debug.Log("Goal" + players[1].goal.x + "," + players[1].goal.y);
        }
        if (players.Length >= 3)
        {
            GameObject go = Instantiate(player, new Vector3(0, 1, (y - 1) * 2), Quaternion.identity);
            Material[] mats = go.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            mats[0] = Resources.Load("Shaders/Player3Mat") as Material;
            go.transform.GetChild(0).GetComponent<MeshRenderer>().materials = mats;
            players[2] = new Player(2, map[0, y - 1], go);
            curPlayer = 2;
            SetNewGoal();
            Debug.Log("Goal" + players[2].goal.x + "," + players[2].goal.y);
        }
        if (players.Length >= 4)
        {
            GameObject go = Instantiate(player, new Vector3((x - 1) * 2, 1, 0), Quaternion.identity);
            Material[] mats = go.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            mats[0] = Resources.Load("Shaders/Player4Mat") as Material;
            go.transform.GetChild(0).GetComponent<MeshRenderer>().materials = mats;
            players[3] = new Player(3, map[x - 1, 0], go);
            curPlayer = 3;
            SetNewGoal();
            Debug.Log("Goal" + players[3].goal.x + "," + players[3].goal.y);
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

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y - 1; j++)
            {
                GameObject g = Instantiate(wall, new Vector3(i * 2, 1, j * 2 + 1), Quaternion.Euler(0, 90, 0));
                g.GetComponent<MeshRenderer>().enabled = false;
                map[i, j].walls[2] = g;
                map[i, j + 1].walls[0] = g;
            }
        }
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x - 1; j++)
            {
                GameObject g = Instantiate(wall, new Vector3(j * 2 + 1, 1, i * 2), Quaternion.identity);
                g.GetComponent<MeshRenderer>().enabled = false;
                map[j, i].walls[3] = g;
                map[j + 1, i].walls[1] = g;
            }
        }

        bridges = new List<Bridge>();
        walls = new List<Wall>();
        curPlayer = 0;
        bridgePlacingPhase = true;
    }

    private void FixedUpdate()
    {
        Player p = players[curPlayer];
        bridgesOwned = 0;
        wallsOwned = 0;
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
        if (walls.Count > 0)
        {
            foreach (Wall w in walls)
            {
                if (w.getPlayer().Equals(p))
                {
                    wallsOwned++;
                }
            }
        }
        if (p.points > 3)
        {
            Debug.Log(players[curPlayer] + "wins");
            Application.Quit();
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
                                    Material[] mats = g.GetComponent<MeshRenderer>().materials;
                                    mats[0] = Resources.Load("Shaders/Player" + (curPlayer + 1) + "Mat") as Material;
                                    g.GetComponent<MeshRenderer>().materials = mats;
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

                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.transform.gameObject.tag.Equals("Wall"))
                        {
                            GameObject g = hit.transform.gameObject;
                            if (!g.GetComponent<MeshRenderer>().enabled)
                            {
                                if (wallsOwned == 0)
                                {
                                    g.GetComponent<MeshRenderer>().enabled = true;
                                    Material[] mats = g.GetComponent<MeshRenderer>().materials;
                                    mats[0] = Resources.Load("Shaders/Player" + (curPlayer + 1) + "Mat") as Material;
                                    g.GetComponent<MeshRenderer>().materials = mats;
                                    walls.Add(new Wall(p, g));
                                }
                            }
                            else
                            {
                                foreach (Wall w in walls)
                                {
                                    if (w.wall.Equals(g) && (w.getPlayer().Equals(p)))
                                    {
                                        g.GetComponent<MeshRenderer>().enabled = false;
                                        walls.Remove(w);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                if (Input.GetKeyDown(KeyCode.E) && bridgesOwned == 3 && wallsOwned == 1)
                {
                    bridgePlacingPhase = false;
                    Debug.Log("Switched to movement");
                    validTiles = CheckPath(players[curPlayer].location, new List<Tile>());
                }
            }
            else
            {

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
                    Material[] mats = p.goal.tile.GetComponent<MeshRenderer>().materials;
                    mats[0] = Resources.Load("Shaders/TowerMat") as Material;
                    p.goal.tile.GetComponent<MeshRenderer>().materials = mats;

                    if (p.location.Equals(p.goal))
                    {
                       p.points++;
                        SetNewGoal();
                        Debug.Log("POINT");
                        Debug.Log("Goal" + players[0].goal.x + "," + players[0].goal.y);

                    }
                    bridgePlacingPhase = true;
                    foreach(Tile t in validTiles)
                        t.encountered=false;
                    curPlayer++;
                    if (curPlayer > numPlayers - 1)
                        curPlayer = 0;

                    p = players[curPlayer];
                    mats = p.goal.tile.GetComponent<MeshRenderer>().materials;
                    mats[0] = Resources.Load("Shaders/Player" + (curPlayer + 1) + "Mat") as Material;
                    p.goal.tile.GetComponent<MeshRenderer>().materials = mats;

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

        if (tile.x!=x-1 && map[tile.x, tile.y].bridges[3] != null && map[tile.x, tile.y].bridges[3].GetComponent<MeshRenderer>().enabled && !map[tile.x + 1, tile.y].encountered)

        {
            map[tile.x + 1, tile.y].predecessor = 3;
            CheckPath(map[tile.x + 1, tile.y], validTiles);
        }

        if (tile.y!=y-1 && map[tile.x, tile.y].bridges[2] != null && map[tile.x, tile.y].bridges[2].GetComponent<MeshRenderer>().enabled && !map[tile.x, tile.y + 1].encountered)

        {
            map[tile.x, tile.y + 1].predecessor = 0;
            CheckPath(map[tile.x, tile.y + 1], validTiles);
        }

        if (tile.x!=0 && map[tile.x, tile.y].bridges[1] != null && map[tile.x, tile.y].bridges[1].GetComponent<MeshRenderer>().enabled && !map[tile.x - 1, tile.y].encountered)

        {
            map[tile.x - 1, tile.y].predecessor = 1;
            CheckPath(map[tile.x - 1, tile.y], validTiles);
        }
        
        Debug.Log(tile.x + ", " + tile.y);
        return validTiles;
    }

    public void SetNewGoal()
    {
        int randx = r.Next(0, map.Length/y);
        int randy = r.Next(0, map.Length/x);
        while (Math.Abs(randx - players[curPlayer].location.x) + Math.Abs(randy - players[curPlayer].location.y) < 1)
        {
            randx = r.Next(0, map.Length/y);
            randy = r.Next(0, map.Length/x);
        }

        players[curPlayer].SetGoal(map[randx, randy]);
    }

    private IEnumerator RaiseTile (GameObject tileObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        float t = 0;
        while (t < 1f)
        {
            tileObject.transform.position = Vector3.Lerp(tileObject.transform.position, new Vector3(tileObject.transform.position.x, -4f, tileObject.transform.position.z), 0.02f);
            t += 0.005f;
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }
}