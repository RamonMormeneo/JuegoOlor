using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPos;
    public Transform targetPos;
    public void Awake()
    {
        grid = GetComponent<Grid>();
    }


    // Update is called once per frame
    void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }
    void FindPath(Vector3 _startPos, Vector3 _targetPos)
    {
        Node Startnode = grid.NodeFroemWorldPosition(_startPos);
        Node Targetnode = grid.NodeFroemWorldPosition(_targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> CloseList = new HashSet<Node>();

        openList.Add(Startnode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            CloseList.Add(currentNode);
            if (currentNode == Targetnode)
            {
                GetFinalPath(Startnode, Targetnode);
            }
            foreach (Node neighbourNode in grid.GetNieghbour(currentNode))
            {
                if (!neighbourNode.isWall || CloseList.Contains(neighbourNode))
                {
                    continue;
                }
                int MoveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighbourNode);
                if(MoveCost < neighbourNode.gCost || !openList.Contains(neighbourNode))
                {
                    neighbourNode.gCost = MoveCost;
                    neighbourNode.hCost = GetManhattenDistance(neighbourNode, Targetnode );
                    neighbourNode.Parent = currentNode;

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
    }
    void GetFinalPath(Node a_Startnode, Node a_Targetnode)
    {
        List<Node> finalPath = new List<Node>();
        Node CurrentNode = a_Targetnode;

        while (CurrentNode != a_Startnode)
        {
            finalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        finalPath.Reverse();
        grid.FinalPath = finalPath;
    }
    int GetManhattenDistance(Node a_currentNode, Node a_neighbourNode)
    {
        int ix = Mathf.Abs(a_currentNode.gridX - a_neighbourNode.gridX);
        int iy = Mathf.Abs(a_currentNode.gridY - a_neighbourNode.gridY);
        return (ix + iy);
    }
}
