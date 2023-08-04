using System;
using System.Collections.Generic;
using System.Linq;

public class CorridorsGenerator
{
    public List<Node> CreateCorridor(List<RoomNode> allNodesCollection, int corridorWidth)
    {
        List<Node> corridorList = new List<Node>();
        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(
            allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());

        while (structuresToCheck.Count > 0)
        {
            var node = structuresToCheck.Dequeue();

            if (node.ChildrenNodeList.Count == 0)
            {
                continue;
            }

            // Create a corridor node
            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth);

            // Check for overlap with existing corridors and rooms
            bool hasOverlap = corridorList.Any(existingNode => IsOverlap(corridor, existingNode));

            if (!hasOverlap)
            {
                corridorList.Add(corridor);
            }
        }
        return corridorList;
    }

    private bool IsOverlap(Node node1, Node node2)
    {
        // Check for overlap between the bounding boxes of the nodes
        return node1.BottomLeftAreaCorner.x <= node2.TopRightAreaCorner.x &&
               node1.TopRightAreaCorner.x >= node2.BottomLeftAreaCorner.x &&
               node1.BottomLeftAreaCorner.y <= node2.TopRightAreaCorner.y &&
               node1.TopRightAreaCorner.y >= node2.BottomLeftAreaCorner.y;
    }
}

//
//public class CorridorsGenerator
//{
//    public List<Node> CreateCorridor(List<RoomNode> allNodesCollection, int corridorWidth)
//    {
//        List<Node> corridorList = new List<Node>();
//        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(
//            allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());
//        while (structuresToCheck.Count > 0)
//        {
//            var node = structuresToCheck.Dequeue();
//            if (node.ChildrenNodeList.Count == 0)
//            {
//                continue;
//            }
//            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth);
//            corridorList.Add(corridor);
//        }
//        return corridorList;
//    }
//}