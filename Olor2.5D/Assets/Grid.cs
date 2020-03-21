using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPos;
    public LayerMask WallMasK;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for(int y =0;y<gridSizeX;y++)
        {
            for(int x =0;x<gridSizeY;x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Wall = true;
                if(Physics.CheckSphere(worldPoint,nodeRadius,WallMasK))
                {
                    Wall = false;
                }
                grid[y, x] = new Node(Wall, worldPoint, x, y);
            }
        }
    }
    public Node NodeFroemWorldPosition(Vector3 a_Worldpos)
    {
        float xpoint = ((a_Worldpos.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_Worldpos.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }
    public List<Node> GetNieghbour(Node curent)
    {
        List<Node> neighNode = new List<Node>();
        int Xchek;
        int Ychek;
        //RIGHT SIDE
        Xchek = curent.gridX + 1;
        Ychek = curent.gridY;
        if (Xchek >= 0 && Xchek < gridSizeX)
        {
            if (Ychek >= 0 && Ychek < gridSizeY)
            {
                neighNode.Add(grid[Xchek, Ychek]);
            }
        }
        //LEFT SIDE
        Xchek = curent.gridX - 1;
        Ychek = curent.gridY;
        if (Xchek >= 0 && Xchek < gridSizeX)
        {
            if (Ychek >= 0 && Ychek < gridSizeY)
            {
                neighNode.Add(grid[Xchek, Ychek]);
            }
        }
        //UP SIDE
        Xchek = curent.gridX;
        Ychek = curent.gridY + 1;
        if (Xchek >= 0 && Xchek < gridSizeX)
        {
            if (Ychek >= 0 && Ychek < gridSizeY)
            {
                neighNode.Add(grid[Xchek, Ychek]);
            }
        }
        //DOWN SIDE
        Xchek = curent.gridX;
        Ychek = curent.gridY - 1;
        if (Xchek >= 0 && Xchek < gridSizeX)
        {
            if (Ychek >= 0 && Ychek < gridSizeY)
            {
                neighNode.Add(grid[Xchek, Ychek]);
            }
        }
        return neighNode;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(grid!=null)
        {
            foreach(Node node in grid)
            {
                if(node.isWall)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                if(FinalPath!=null)
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
  
}
