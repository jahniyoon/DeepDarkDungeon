﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class DungeonCreator : MonoBehaviour
{
    [Header("Debug")]
    public TMP_Text roomCountText;
    public static bool floorOverlap = true; // 던전 적합도 체크
    private int roomNum = 1;
    public NavMeshSurface surface;
        
    // 통과 가능한 문과 벽의 위치 리스트
    List<Vector3Int> possibleDoorVerticalPosition;
    List<Vector3Int> possibleDoorHorizontalPosition;
    List<Vector3Int> possibleWallHorizontalPosition;
    List<Vector3Int> possibleWallVerticalPosition;


    [Header("Dungeon Setting")]
    // 던전 관련 변수
    public float tileSize = 1.0f; // 각 타일의 크기

    public int dungeonWidth, dungeonLength;            // 던전의 가로 및 세로 크기 설정
    public int roomWidthMin, roomLengthMin;            // 각 방의 최소 가로 및 세로 크기 설정
    public int maxIterations;                          // 던전 생성 과정 중 반복하는 최대 횟수 설정
    public int corridorWidth;                          // 복도의 너비 설정
    //public Material material;                        // 생성된 방의 바닥 메쉬에 적용할 머티리얼
    [Range(0.0f, 0.4f)]
    public float roomBottomCornerModifier;             // 방의 아래쪽 코너를 형성하는데 사용되는 조정값
    [Range(0.5f, 1.0f)]
    public float roomTopCornerMidifier;                // 방의 위쪽 코너를 형성하는데 사용되는 조정값
    [Range(0, 2)]
    public int roomOffset;                             // 방의 간격 설정
    

    [Header("Dungeon Prefabs")]

    public GameObject Floor;                           // 생성된 방의 바닥에 적용할 프리펩
    public GameObject DoorHorizontal;
    public GameObject DoorVertical;
    public GameObject wallVertical, wallHorizontal;    // 생성할 벽 오브젝트 프리펩 설정



    [Header("Prefabs")]
    public GameObject Player;
    public GameObject Exit;
    public GameObject enemyPrefabs;
    public GameObject torchPrefabs;


    private Vector3Int firstRoomCenterPosition; // 첫 번째 방의 중앙 위치
    private Vector3Int lastRoomCenterPosition; // 마지막 방의 중앙 위치
    List<Vector3Int> enemyPositions;
    List<Vector3> torchPositions;

    void Start()
    {
        StartCoroutine(CheckFloorOverlapAndGenerateDungeon());
    }

    // 던전 생성 메소드 호출
    public void CreateDungeon()
    {
        Debug.Log("던전을 생성합니다.");
        DestroyAllChildren();   // 던전 초기화

        DugeonGenerator generator = new DugeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateDungeon(maxIterations,
            roomWidthMin,
            roomLengthMin,
            roomBottomCornerModifier,
            roomTopCornerMidifier,
            roomOffset,
            corridorWidth);

        if (6 >= generator.CreatedRooms.Count)
        {
            DungeonCreator.floorOverlap = true;
            Debug.Log("방 개수 미충족. 던전을 다시 생성합니다.");
        }
        // 벽 생성 가능 리스트
        possibleWallHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();
        // 문 생성 가능 리스트
        possibleDoorVerticalPosition = new List<Vector3Int>();
        possibleDoorHorizontalPosition = new List<Vector3Int>();

        // 생성될 프리팹 관리할 게임오브젝트
        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;

        GameObject floorParent = new GameObject("FloorParent");
        floorParent.transform.parent = transform;

        GameObject enemyParent = new GameObject("EnemiesParent");
        enemyParent.transform.parent = transform;

        GameObject dungeonDecorationParent = new GameObject("DungeonDecorationParent");
        dungeonDecorationParent.transform.parent = transform;

        // 적 위치 리스트
        enemyPositions = new List<Vector3Int>();
        torchPositions = new List<Vector3>();


        RoomData(generator);

        // 각 방의 바닥 메쉬 생성
        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, floorParent);         
        }
        
        CreateWalls(wallParent);        // 벽 생성
        CreateDoors();                  // 문 생성
        CreateExit();                   // 출구 생성
        CreateDungeonDecorations(dungeonDecorationParent);  // 던전 장식 생성
        surface.BuildNavMesh(); // NavMesh 업데이트 [움직이지 않는 것 들을 생성한 뒤 호출해야 함]


        CreatePlayer();                 // 플레이어 생성
        CreateEnemies(enemyParent);     // 적 생성


    }
    // } CreateDungeon()
    public void RoomData(DugeonGenerator generator)
    {
        // 생성된 방 개수와 정보 출력
        //Debug.Log("Created Room Count: " + generator.CreatedRooms.Count);
        roomCountText.text = string.Format("Room Count : {0}", generator.CreatedRooms.Count);
        //foreach (var room in generator.CreatedRooms)
        //{
        //    Debug.Log(roomNum +" Room Position: " + room.BottomLeftAreaCorner + " - " + room.TopRightAreaCorner);
        //    roomNum++;
        //}

        // 첫번째로 생성된 방의 위치는?
        RoomNode firstRoom = generator.CreatedRooms[0];
        firstRoomCenterPosition = new Vector3Int(
        (firstRoom.BottomLeftAreaCorner.x + firstRoom.TopRightAreaCorner.x) / 2,
        0,
        (firstRoom.BottomLeftAreaCorner.y + firstRoom.TopRightAreaCorner.y) / 2);

        // 마지막으로 생성된 방의 위치는?  
        RoomNode lastRoom = generator.CreatedRooms[generator.CreatedRooms.Count - 1];
        lastRoomCenterPosition = new Vector3Int(
        (lastRoom.BottomLeftAreaCorner.x + lastRoom.TopRightAreaCorner.x) / 2,
        0,
        (lastRoom.BottomLeftAreaCorner.y + lastRoom.TopRightAreaCorner.y) / 2);

        // 첫번째와 마지막 방을 제외한 방들을 위한 리스트 선언
        List<RoomNode> dungeonRooms = generator.CreatedRooms;
        for (int i = 1; i <= dungeonRooms.Count - 2; i++)
        {
            RoomNode dungeonRoom = dungeonRooms[i];

            // 방의 중앙
            Vector3Int centerPosition = new Vector3Int(
                (dungeonRoom.BottomLeftAreaCorner.x + dungeonRoom.TopRightAreaCorner.x) / 2,
                0,
                (dungeonRoom.BottomLeftAreaCorner.y + dungeonRoom.TopRightAreaCorner.y) / 2);

            // 방의 중앙에 몬스터 생성
            enemyPositions.Add(centerPosition);
        }
        for (int i = 0; i <= dungeonRooms.Count - 1; i++)
        {
            RoomNode dungeonRoom = dungeonRooms[i];

            // 방의 모서리
            Vector3 TopLeftAreaCorner = new Vector3(
                dungeonRoom.BottomLeftAreaCorner.x + 0.5f, 0, dungeonRoom.TopRightAreaCorner.y - 0.5f);
            Vector3 TopRightAreaCorner = new Vector3(
               dungeonRoom.TopRightAreaCorner.x - 0.5f, 0, dungeonRoom.TopRightAreaCorner.y - 0.5f);
            Vector3 BottomLeftAreaCorner = new Vector3(
                dungeonRoom.BottomLeftAreaCorner.x + 0.5f, 0, dungeonRoom.BottomLeftAreaCorner.y + 0.5f);
            Vector3 BottomRightAreaCorner = new Vector3(
               dungeonRoom.TopRightAreaCorner.x - 0.5f, 0, dungeonRoom.BottomLeftAreaCorner.y + 0.5f);

            // 방의 모서리에 토치 생성
            torchPositions.Add(TopLeftAreaCorner);
            torchPositions.Add(TopRightAreaCorner);
            torchPositions.Add(BottomLeftAreaCorner);
            torchPositions.Add(BottomRightAreaCorner);

        }
    }

    // 각 방의 메쉬 생성
    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject floorParent)
    {
        // 메쉬 생성에 필요한 점들 생성
        Vector3 bottomLeftV = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightV = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftV = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightV = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        // 각 메쉬 정점과 UV 좌표 설정
        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV
        };
        // 프리팹으로 바닥 생성
        int numTilesX = Mathf.FloorToInt((topRightV.x - bottomLeftV.x) / tileSize);
        int numTilesZ = Mathf.FloorToInt((topRightV.z - bottomLeftV.z) / tileSize);

        for (int x = 0; x < numTilesX; x++)
        {
            for (int z = 0; z < numTilesZ; z++)
            {
                Vector3 tileBottomLeft = new Vector3(bottomLeftV.x + x * tileSize, 0, bottomLeftV.z + z * tileSize);
                Vector3 tileTopRight = new Vector3(tileBottomLeft.x + tileSize, 0, tileBottomLeft.z + tileSize);
                CreateFloor(tileBottomLeft, tileTopRight, floorParent);
              
            }
        }
        //벽 위치 계산
        for (int row = (int)bottomLeftV.x; row < (int)bottomRightV.x; row++)
        {
            var wallPosition = new Vector3(row, 0, bottomLeftV.z);
            AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
        }
        for (int row = (int)topLeftV.x; row < (int)topRightCorner.x; row++)
        {
            var wallPosition = new Vector3(row, 0, topRightV.z);
            AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
        }
        for (int col = (int)bottomLeftV.z; col < (int)topLeftV.z; col++)
        {
            var wallPosition = new Vector3(bottomLeftV.x, 0, col);
            AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
        }
        for (int col = (int)bottomRightV.z; col < (int)topRightV.z; col++)
        {
            var wallPosition = new Vector3(bottomRightV.x, 0, col);
            AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
        }

      
    }

    // 바닥 생성
    private void CreateFloor(Vector3 bottomLeft, Vector3 topRight, GameObject floorParent)
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(bottomLeft.x, 0, topRight.z),
            new Vector3(topRight.x, 0, topRight.z),
            new Vector3(bottomLeft.x, 0, bottomLeft.z),
            new Vector3(topRight.x, 0, bottomLeft.z)
        };
        GameObject dungeonFloor = Instantiate(Floor);
        dungeonFloor.transform.position = new Vector3((bottomLeft.x + topRight.x) / 2, 0, (bottomLeft.z + topRight.z) / 2);
        dungeonFloor.transform.localScale = new Vector3(topRight.x - bottomLeft.x, 1, topRight.z - bottomLeft.z);
        dungeonFloor.transform.parent = floorParent.transform;
        int floorLayer = LayerMask.NameToLayer("Floor"); // 원하는 레이어 이름
        dungeonFloor.layer = floorLayer;
    }



    // 벽 위치 리스트에 추가
    private void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (wallList.Contains(point))
        {
            doorList.Add(point);
            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }

    // { 벽 생성 메소드 호출
    private void CreateWalls(GameObject wallParent)
    {
        foreach (var wallPosition in possibleWallHorizontalPosition)
        {
            CreateWall(wallParent, wallPosition, wallHorizontal);
        }
        foreach (var wallPosition in possibleWallVerticalPosition)
        {
            CreateWall(wallParent, wallPosition, wallVertical);
        }
    }

    // { 단일 벽 오브젝트 생성 메소드 
    private void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        GameObject newWall = Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
        //int wallLayer = LayerMask.NameToLayer("Wall");
        //newWall.layer = wallLayer;
    }

    // 복도에 문 생성
    private void CreateDoors()
    {
        GameObject doorParent = new GameObject("DoorParent");
        doorParent.transform.parent = transform;

        foreach (var doorPosition in possibleDoorVerticalPosition)
        {
            CreateDoor(doorParent, doorPosition, DoorVertical);
        }
        foreach (var doorPosition in possibleDoorHorizontalPosition)
        {
            CreateDoor(doorParent, doorPosition, DoorHorizontal);
        }
    }
    // 문 생성
    private void CreateDoor(GameObject doorParent, Vector3Int doorPosition, GameObject doorPrefab)
    {
        Instantiate(doorPrefab, doorPosition, Quaternion.identity, doorParent.transform);
    }


    // 플레이어 생성
    public void CreatePlayer()
    {
        // 플레이어 위치 변경 및 활성화
        GameObject player = Player;
        if (player != null)
        {
            player.transform.position = firstRoomCenterPosition; // 첫 번째 방의 중앙 위치로 설정
            player.SetActive(true); // 플레이어 오브젝트 활성화
        }
    }

    // 출구 생성
    public void CreateExit()
    {
        GameObject exit = Exit;
        if (exit != null)
        {
            exit.transform.position = lastRoomCenterPosition; // 마지막 방의 중앙 위치로 설정
            GameObject dungeonExit = Instantiate(exit, exit.transform.position, Quaternion.identity);
            dungeonExit.transform.parent = transform;
        }
    }

    public void CreateEnemies(GameObject enemyParent)
    {
        foreach (var enemyPosition in enemyPositions)
        {
            CreateEnemy(enemyParent, enemyPosition, enemyPrefabs);
        }
        //GameObject enemy = Enemy;
        //if (enemy != null)
        //{
        //    enemy.transform.position = lastRoomCenterPosition; // 마지막 방의 중앙 위치로 설정
        //    GameObject Enemies = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
        //    Enemies.transform.parent = transform;
        //}
    }
    public void CreateEnemy(GameObject enemyParent, Vector3Int enemyPosition, GameObject enemyPrefabs)
    {
        GameObject newEnemy = Instantiate(enemyPrefabs, enemyPosition, Quaternion.identity, enemyParent.transform);
    }

    public void CreateDungeonDecorations(GameObject dungeonDecorationParent)
    {
        foreach (var torchPosition in torchPositions)
        {
            CreateTorch(dungeonDecorationParent, torchPosition, torchPrefabs);
        }
    }
    public void CreateTorch(GameObject torchParent, Vector3 torchPosition, GameObject torchPrefabs)
    {
        GameObject newTorch = Instantiate(torchPrefabs, torchPosition, Quaternion.identity, torchParent.transform);
    }

    // 던전 생성기 초기화
    private void DestroyAllChildren()
    {
        roomNum = 1;    // [디버그] 룸 넘버 초기화

        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }

    // 중복 발생시 재시작
    private IEnumerator CheckFloorOverlapAndGenerateDungeon()
    {
        while (DungeonCreator.floorOverlap)
        {
            DungeonCreator.floorOverlap = false;
            CreateDungeon();
            yield return null; // 한 프레임 대기
        }
    }

}
