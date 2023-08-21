using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomGenerator
{
    private int maxIterations;
    private int roomLengthMin;
    private int roomWidthMin;

    public RoomGenerator(int maxIterations, int roomLengthMin, int roomWidthMin)
    {
        this.maxIterations = maxIterations;
        this.roomLengthMin = roomLengthMin;
        this.roomWidthMin = roomWidthMin;
    }

    public List<RoomNode> GenerateRoomsInGivenSpaces(List<Node> roomSpaces, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();
        foreach (var space in roomSpaces)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(
                space.BottomLeftAreaCorner, space.TopRightAreaCorner, roomBottomCornerModifier, roomOffset);

            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(
                space.BottomLeftAreaCorner, space.TopRightAreaCorner, roomTopCornerMidifier, roomOffset);
            space.BottomLeftAreaCorner = newBottomLeftPoint;
            space.TopRightAreaCorner = newTopRightPoint;
            space.BottomRightAreaCorner = new Vector2Int(newTopRightPoint.x, newBottomLeftPoint.y);
            space.TopLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);
            listToReturn.Add((RoomNode)space);
                
        }

        int randomValue = Random.Range(0, 3);
        // 스폰 방 좌표 
        int spawnRoomPosX = 20 + randomValue;
        int SpawnRoomPosY = -7 ;

        // 보스 방과 출구 방 좌표
        int bossRoomPosX = -4 + randomValue;  
        int bossRoomPosY = 24;   
        int bossRoomSize = 7;

        // 스폰 방 생성
        Vector2Int spawnRoomBottomLeft = new Vector2Int(spawnRoomPosX - 7, SpawnRoomPosY);
        Vector2Int spawnRoomTopRight = new Vector2Int(spawnRoomPosX , SpawnRoomPosY + 5);
        listToReturn.Add(new RoomNode(spawnRoomBottomLeft, spawnRoomTopRight, null, 0));

        // 보스 방 생성
        Vector2Int bossRoomBottomLeft = new Vector2Int(bossRoomPosX, bossRoomPosY);
        Vector2Int bossRoomTopRight = new Vector2Int(bossRoomPosX + 11, bossRoomPosY + bossRoomSize);
        listToReturn.Add(new RoomNode(bossRoomBottomLeft, bossRoomTopRight, null, 0));

        // 출구 방 생성
        Vector2Int exitRoomBottomLeft = new Vector2Int(bossRoomPosX + 3, bossRoomPosY + bossRoomSize + 1);
        Vector2Int exitRoomTopRight = new Vector2Int(bossRoomPosX + 8, bossRoomPosY + bossRoomSize + 5);
        listToReturn.Add(new RoomNode(exitRoomBottomLeft, exitRoomTopRight, null, 0));


        return listToReturn;
    }
}