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

    //던전 생성
    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset, int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        CreatedRooms = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);  // 생성된 방 데이터 저장

        CorridorsGenerator corridorGenerator = new CorridorsGenerator();
        var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

        // 중첩 체크 스크립트
        bool foundOverlap = false;
        foreach (var room in CreatedRooms)
        {
            foreach (var corridor in corridorList)
            {
                if (CheckOverlap(room, corridor))
                {
                    Debug.Log("중첩되는 영역을 확인: Room - " + room + ", Corridor - " + corridor);
                    foundOverlap = true;
                }
                if (foundOverlap) { break; } // 중첩 발생시 체크 break
            }
        }

        // 중첩 영역 확인되었으면 CalculateDungeon메서드를 다시 실행
        if (foundOverlap || CreatedRooms.Count <= 7)
        {
            Debug.Log("중첩된 영역이 발견되었습니다. CalculateDungeon을 다시 실행합니다.");
            return CalculateDungeon(maxIterations, roomWidthMin, roomLengthMin, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset, corridorWidth);
        }

        else
        {
            return new List<Node>(CreatedRooms).Concat(corridorList).ToList();
        }
    }
    // 복도와 방 중첩 체크 메서드
    private bool CheckOverlap(Node a, Node b)
    {
        return a.BottomLeftAreaCorner.x < b.TopRightAreaCorner.x && a.TopRightAreaCorner.x > b.BottomLeftAreaCorner.x &&
               a.BottomLeftAreaCorner.y < b.TopRightAreaCorner.y && a.TopRightAreaCorner.y > b.BottomLeftAreaCorner.y;
    }

  

}