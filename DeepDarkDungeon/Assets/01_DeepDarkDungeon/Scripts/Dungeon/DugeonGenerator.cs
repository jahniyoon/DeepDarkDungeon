using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DugeonGenerator
{
    // 던전 만들기

    List<RoomNode> allNodesCollection = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;

    // 생성된 방 데이터에 리스트 추가
    public List<RoomNode> CreatedRooms { get; private set; } = new List<RoomNode>();
    public int CreatedRoomCount => CreatedRooms.Count; // 방 개수

    public DugeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }

    //// 방이 바닥과 겹치는지 확인
    //public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset, int corridorWidth)
    //{
    //    BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
    //    allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
    //    List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

    //    RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);

    //    while (true)
    //    {
    //        CreatedRooms = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);
    //        CorridorsGenerator corridorGenerator = new CorridorsGenerator();
    //        var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

    //        // Check for overlap between rooms and corridors
    //        bool hasOverlap = CheckRoomCorridorOverlap(CreatedRooms, corridorList);

    //        if (!hasOverlap)
    //        {
    //            return new List<Node>(CreatedRooms).Concat(corridorList).ToList();
    //        }
    //    }
    //}

    //private bool CheckRoomCorridorOverlap(List<RoomNode> rooms, List<Node> corridors)
    //{
    //    foreach (var corridor in corridors)
    //    {
    //        foreach (var room in rooms)
    //        {
    //            if (DoRectanglesOverlap(corridor.BottomLeftAreaCorner, corridor.TopRightAreaCorner,
    //                                    room.BottomLeftAreaCorner, room.TopRightAreaCorner))
    //            {
    //                return true; // Overlap detected
    //            }
    //        }
    //    }
    //    return false; // No overlap
    //}

    //private bool DoRectanglesOverlap(Vector2Int rect1Min, Vector2Int rect1Max, Vector2Int rect2Min, Vector2Int rect2Max)
    //{
    //    return rect1Min.x < rect2Max.x && rect1Max.x > rect2Min.x &&
    //           rect1Min.y < rect2Max.y && rect1Max.y > rect2Min.y;
    //}

    //방 체크 코드 추가
    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset, int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        CreatedRooms = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);  // 생성된 방 데이터 저장

        CorridorsGenerator corridorGenerator = new CorridorsGenerator();
        var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

        return new List<Node>(CreatedRooms).Concat(corridorList).ToList();
    }

    // 기존 코드
    //public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset, int corridorWidth)
    //{
    //    BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
    //    allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
    //    List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

    //    RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
    //    List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);

    //    CorridorsGenerator corridorGenerator = new CorridorsGenerator();
    //    var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

    //    return new List<Node>(roomList).Concat(corridorList).ToList();


    //}
}