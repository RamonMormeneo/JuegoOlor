using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int gridX;
    public int gridY;

    public bool isWall;
    public bool isGreen;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;
    public int FCost {get { return (gCost + hCost); } }

    public Node(bool _isWall, Vector3 _Position, int agridX, int agridY)
    {
        isGreen = false;
        isWall = _isWall;
        Position = _Position;
        gridX = agridX;
        gridY = agridY;
    }
}
