/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriorityQueue;
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data;

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int ci = data.Count - 1; // child index; start at end
        while (ci > 0)
        {
            int pi = (ci - 1) / 2; // parent index
            if (data[ci].CompareTo(data[pi]) >= 0) break; // child item is larger than (or equal) parent so we're done
            T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        // assumes pq is not empty; up to calling code
        int li = data.Count - 1; // last index (before removal)
        T frontItem = data[0];   // fetch the front
        data[0] = data[li];
        data.RemoveAt(li);

        --li; // last index (after removal)
        int pi = 0; // parent index. start at front of pq
        while (true)
        {
            int ci = pi * 2 + 1; // left child index of parent
            if (ci > li) break;  // no children so done
            int rc = ci + 1;     // right child
            if (rc <= li && data[rc].CompareTo(data[ci]) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                ci = rc;
            if (data[pi].CompareTo(data[ci]) <= 0) break; // parent is smaller than (or equal to) smallest child so done
            T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
            pi = ci;
        }
        return frontItem;
    }

    public T Peek()
    {
        T frontItem = data[0];
        return frontItem;
    }

    public int Count()
    {
        return data.Count;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < data.Count; ++i)
            s += data[i].ToString() + " ";
        s += "count = " + data.Count;
        return s;
    }

    public bool IsConsistent()
    {
        // is the heap property true for all data?
        if (data.Count == 0) return true;
        int li = data.Count - 1; // last index
        for (int pi = 0; pi < data.Count; ++pi) // each parent index
        {
            int lci = 2 * pi + 1; // left child index
            int rci = 2 * pi + 2; // right child index

            if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false; // if lc exists and it's greater than parent then bad.
            if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false; // check the right child too.
        }
        return true; // passed all checks
    } // IsConsistent
} // PriorityQueue
// ns

public class AI {
    private Dictionary<GameState, int> filter = new Dictionary<GameState, bool>();

    public GameState BFS_ish(GameState gameState,Player player,int cutoff)    {
        PriorityQueue<gameState> queue = new PriorityQueue();
        gameState curnode = gameState;
        int[] originalpoints = player.goal;
        
        //copy the player array over to a new thing 
        queue.Enqueue(gameState);
        int i = 0;
        while ((queue.Count > 0) && (cutoff > i)){
            curnode = queue.Dequeue();
            if(player.pos[0]==originalpoints[0]&& player.pos[1] == originalpoints[1])
            {
                return curnode.Top();
            }
            if (filter.ContainsKey(curnode)){
                continue;
            }
            foreach(GameState node in curnode.getAllMoves(player)){
                filter.Add(node, true);
                node.cost = i + heuristic(node);
                queue.Enqueue(node);
            }
        }
        return queue.Dequeue().Top();
    
    }
    public int heuristic(GameState game)
    {
        return 0;
    }
    public Tuple<GameState,int[]> Minimax(GameState gameState, Player[] player, int cutoff,int deapth,int curplayer){
        GameState best = gameState;
        int[] bestval;
        foreach(GameState node in gameState.getAllMoves()){
            if (node.player.won)
            {
                int[] vals = new int[4];
                for(int i =0; i< vals.Length; i++)
                {
                    if (player[curplayer])
                    {
                        vals[i] = 1000;
                    }
                    else
                    {
                        vals[i] = -1000;
                    }
                }
                return new Tuple<node, vals>();
            }
            else if (deapth >= cutoff) {
                int[] tempval = heuristic(node);
            }
            else
            {
                int[] tempval = Minimax(node, player, cutoff, deapth++,(curplayer+1)%4)[1];
            }

            if (bestval != null && (tempval[curplayer]-tempval[(curplayer+1)%4] - tempval[(curplayer + 1) % 4] - tempval[(curplayer + 2) % 4] - tempval[(curplayer + 3) % 4]) > bestval){
                bestval = tempval;
                best = node;
            }
        }
        return new Tuple<best, bestval>();

    }
    public GameState MC()
    {
        return null;
    }
    
}
*/