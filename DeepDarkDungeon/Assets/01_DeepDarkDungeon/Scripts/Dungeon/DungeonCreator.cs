using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public TMP_Text roomCountText;


    // 던전 관련 변수
    public int dungeonWidth, dungeonLength;            // 던전의 가로 및 세로 크기 설정
    public int roomWidthMin, roomLengthMin;            // 각 방의 최소 가로 및 세로 크기 설정
    public int maxIterations;                          // 던전 생성 과정 중 반복하는 최대 횟수 설정
    public int corridorWidth;                          // 복도의 너비 설정
    //public Material material;                        // 생성된 방의 바닥 메쉬에 적용할 머티리얼
    
    public GameObject Floor;                           // 생성된 방의 바닥에 적용할 프리펩
    public float tileSize = 1.0f; // 각 타일의 크기
    public GameObject DoorHorizontal;
    public GameObject DoorVertical;

    public GameObject Player;
    public int roomNum = 1;
    private Vector3Int firstRoomCenterPosition; // 첫 번째 방의 중앙 위치
    private bool isFirstRoomCreated = false;    // 첫 번째 방 생성 여부 확인 변수

    public GameObject Exit;
    private Vector3Int lastRoomCenterPosition; // 마지막 방의 중앙 위치
    private bool isLastRoomCreated = false;    // 마지막 방의 생성 여부 확인 변수
    private bool isExitCreate = false;




    [Range(0.0f, 0.4f)]                         
    public float roomBottomCornerModifier;             // 방의 아래쪽 코너를 형성하는데 사용되는 조정값
    [Range(0.5f, 1.0f)]
    public float roomTopCornerMidifier;                // 방의 위쪽 코너를 형성하는데 사용되는 조정값
    [Range(0, 2)]
    public int roomOffset;                             // 방의 간격 설정
    public GameObject wallVertical, wallHorizontal;    // 생성할 벽 오브젝트 프리펩 설정

    // 통과 가능한 문과 벽의 위치 리스트
    List<Vector3Int> possibleDoorVerticalPosition;
    List<Vector3Int> possibleDoorHorizontalPosition;
    List<Vector3Int> possibleWallHorizontalPosition;
    List<Vector3Int> possibleWallVerticalPosition;


    void Start()
    {
        CreateDungeon();
    }

    // 던전 생성 메소드 호출
    public void CreateDungeon()
    {
        DestroyAllChildren();
        DugeonGenerator generator = new DugeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateDungeon(maxIterations,
            roomWidthMin,
            roomLengthMin,
            roomBottomCornerModifier,
            roomTopCornerMidifier,
            roomOffset,
            corridorWidth);
        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
      
        possibleWallHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();
        possibleDoorVerticalPosition = new List<Vector3Int>();
        possibleDoorHorizontalPosition = new List<Vector3Int>();

        // 생성된 방 개수와 정보 출력
        Debug.Log("Created Room Count: " + generator.CreatedRooms.Count);
        roomCountText.text = string.Format("Room Count : {0}", generator.CreatedRooms.Count);
        foreach (var room in generator.CreatedRooms)
        {
            Debug.Log(roomNum +" Room Position: " + room.BottomLeftAreaCorner + " - " + room.TopRightAreaCorner);
            roomNum++;
        }

        // 첫 번째 방 생성 후 중앙 포지션 기록
        if (!isFirstRoomCreated && generator.CreatedRooms.Count > 0)
        {
            RoomNode firstRoom = generator.CreatedRooms[0];
            firstRoomCenterPosition = new Vector3Int(
                (firstRoom.BottomLeftAreaCorner.x + firstRoom.TopRightAreaCorner.x) / 2,
                0,
                (firstRoom.BottomLeftAreaCorner.y + firstRoom.TopRightAreaCorner.y) / 2);

            isFirstRoomCreated = true;
        }
        // 마지막 방 생성 후 중앙 포지션 기록
        if (!isLastRoomCreated && generator.CreatedRooms.Count > 0)
        {
            RoomNode lastRoom = generator.CreatedRooms[generator.CreatedRooms.Count - 1];
            lastRoomCenterPosition = new Vector3Int(
                (lastRoom.BottomLeftAreaCorner.x + lastRoom.TopRightAreaCorner.x) / 2,
                0,
                (lastRoom.BottomLeftAreaCorner.y + lastRoom.TopRightAreaCorner.y) / 2);

            isLastRoomCreated = true;
        }


        // 각 방의 바닥 메쉬 생성
        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }

        // 벽 생성
        CreateWalls(wallParent);
    }
    // } CreateDungeon()


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
    // } CreateWalls

    // { 단일 벽 오브젝트 생성 메소드 
    private void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
    }
    // } CreateWall

    // 각 방의 메쉬 생성
    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
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
                CreateTile(tileBottomLeft, tileTopRight);  
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

        // 문 생성
        GameObject doorParent = new GameObject("DoorParent");
        doorParent.transform.parent = transform;
        CreateDoors(doorParent);


        // 플레이어 위치 변경 및 활성화
        GameObject player = Player;
        if (player != null)
        {
            player.transform.position = firstRoomCenterPosition; // 첫 번째 방의 중앙 위치로 설정
            player.SetActive(true); // 플레이어 오브젝트 활성화
        }
       
        // 플레이어 위치 변경 및 활성화
        GameObject exit = Exit; 
        if (exit != null && !isExitCreate)
        {
            exit.transform.position = lastRoomCenterPosition; // 마지막 방의 중앙 위치로 설정
            Instantiate(exit, exit.transform.position, Quaternion.identity);
            isExitCreate = true;
        }
      
    }
    // } CreateMesh

    // 복도에 문들을 생성
    private void CreateDoors(GameObject doorParent)
    {
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

    // 바닥 생성
    private void CreateTile(Vector3 bottomLeft, Vector3 topRight)
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
        dungeonFloor.transform.parent = transform;

    } 
    private void CreateExit(Vector3 bottomLeft, Vector3 topRight)
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(bottomLeft.x, 0, topRight.z),
            new Vector3(topRight.x, 0, topRight.z),
            new Vector3(bottomLeft.x, 0, bottomLeft.z),
            new Vector3(topRight.x, 0, bottomLeft.z)
        };

        GameObject dungeonExit = Instantiate(Exit);
        dungeonExit.transform.position = new Vector3((bottomLeft.x + topRight.x) / 2, 0, (bottomLeft.z + topRight.z) / 2);
        dungeonExit.transform.localScale = new Vector3(topRight.x - bottomLeft.x, 1, topRight.z - bottomLeft.z);
        dungeonExit.transform.parent = transform;

    }

    // 벽 위치 리스트에 추가
    private void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (wallList.Contains(point)){
            doorList.Add(point);
            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }
    // } AddWallPositionToList

    private void DestroyAllChildren()
    {
        roomNum = 1;
        isFirstRoomCreated = false;
        isLastRoomCreated = false;
        isExitCreate = false;

        while(transform.childCount != 0)
        {
            foreach(Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }


}
