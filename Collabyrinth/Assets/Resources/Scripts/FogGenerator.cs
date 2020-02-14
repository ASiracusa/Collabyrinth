using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    public float vertDistance = 1f;
    public int gridSize = 20;

    private MeshFilter filter;

    public void Start()
    {

        filter = GetComponent<MeshFilter>();
        filter.mesh = LoadFog();

    }

    private Mesh LoadFog()
    {

        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 0; x < gridSize + 1; x++)
        {
            for (int z = 0; z < gridSize + 1; z++)
            {

                vertices.Add(new Vector3((vertDistance * -0.5f) + (vertDistance * (x / (float)gridSize)), 0, (vertDistance * -0.5f) + (vertDistance * (z / (float)gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridSize, z / (float)gridSize));

            }

        }

        List<int> triangles = new List<int>();
        var vertcount = gridSize + 1;
        
        for (int x = 0; x < vertcount * vertcount - vertcount; x++)
        {
            if ((x + 1) % vertcount == 0)
            {
                continue;
            }

            triangles.AddRange(new List<int>()
            {
                x+1+vertcount, x+vertcount, x,
                x, x+1, x+1+vertcount
            });
        }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }

}
