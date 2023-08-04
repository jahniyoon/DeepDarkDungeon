using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    // 던전 관련 변수
    public int dungeonWidth, dungeonLength;            // 던전의 가로 및 세로 크기 설정
    public int roomWidthMin, roomLengthMin;            // 각 방의 최소 가로 및 세로 크기 설정
    public int maxIterations;                          // 던전 생성 과정 중 반복하는 최대 횟수 설정
    public int corridorWidth;                          // 복도의 너비 설정
    //public Material material;                        // 생성된 방의 바닥 메쉬에 적용할 머티리얼
    
    public GameObject Floor;                           // 생성된 방의 바닥에 적용할 프리펩
    public float tileSize = 1.0f; // 각 타일의 크기


    [Range(0.0f, 0.3f)]                         
    public float roomBottomCornerModifier;             // 방의 바닥을 형성하는데 사용되는 조정값
    [Range(0.7f, 1.0f)]
    public float roomTopCornerMidifier;                // 방의 천장을 형성하는데 사용되는 조정값
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
        possibleDoorVerticalPosition = new List<Vector3Int>();
        possibleDoorHorizontalPosition = new List<Vector3Int>();
        possibleWallHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();

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

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        // 삼각형 설정
        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
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


        //// 메쉬 생성
        //Mesh mesh = new Mesh();
        //mesh.vertices = vertices;
        //mesh.uv = uvs;
        //mesh.triangles = triangles;

        //// 메쉬 오브젝트 생성
        //GameObject dungeonFloor = Instantiate(Floor);
        //dungeonFloor.transform.position = new Vector3(bottomLeftV.x + ((topRightV.x - bottomLeftV.x) / 2), 0, bottomLeftV.z + ((topRightV.z - bottomLeftV.z) / 2));
        //dungeonFloor.transform.localScale = new Vector3(topRightV.x - bottomLeftV.x, 1, topRightV.z - bottomLeftV.z);
        //dungeonFloor.transform.parent = transform;


        //GameObject dungeonFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));

        //dungeonFloor.transform.position = Vector3.zero;
        //dungeonFloor.transform.localScale = Vector3.one;
        //dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        //dungeonFloor.GetComponent<MeshRenderer>().material = material;
        //dungeonFloor.transform.parent = transform;

        //벽 위치 계산
        for (int row = (int)bottomLeftV.x; row <= (int)bottomRightV.x; row++)
        {
            var wallPosition = new Vector3(row, 0, bottomLeftV.z);
            AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
        }
        for (int row = (int)topLeftV.x; row <= (int)topRightCorner.x; row++)
        {
            var wallPosition = new Vector3(row, 0, topRightV.z);
            AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
        }
        for (int col = (int)bottomLeftV.z; col <= (int)topLeftV.z; col++)
        {
            var wallPosition = new Vector3(bottomLeftV.x, 0, col);
            AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
        }
        for (int col = (int)bottomRightV.z; col <= (int)topRightV.z; col++)
        {
            var wallPosition = new Vector3(bottomRightV.x, 0, col);
            AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
        }
    }
    // } CreateMesh

    private void CreateTile(Vector3 bottomLeft, Vector3 topRight)
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(bottomLeft.x, 0, topRight.z),
            new Vector3(topRight.x, 0, topRight.z),
            new Vector3(bottomLeft.x, 0, bottomLeft.z),
            new Vector3(topRight.x, 0, bottomLeft.z)
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
        };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject dungeonFloor = Instantiate(Floor);
        dungeonFloor.transform.position = new Vector3((bottomLeft.x + topRight.x) / 2, 0, (bottomLeft.z + topRight.z) / 2);
        dungeonFloor.transform.localScale = new Vector3(topRight.x - bottomLeft.x, 1, topRight.z - bottomLeft.z);
        dungeonFloor.transform.parent = transform;

        // 기존 코드
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
        while(transform.childCount != 0)
        {
            foreach(Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }
}
