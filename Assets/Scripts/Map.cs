using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField] private CGameManager gameManager;
    [SerializeField] private GameObject cameraRef;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int mapSize = 6;

    public List<Vector2Int> availablePositions;
    private Tile[,] tiles;
    private Snake snake;

    private void Awake()
    {
        SetupTiles();
        CenterCameraPosition();
        SpawnSnake();
        SpawnFood();
    }

    private void SetupTiles()
    {
        tiles = new Tile[mapSize, mapSize];
        availablePositions = new List<Vector2Int>(mapSize * mapSize);
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                GameObject tileGameObject = Instantiate(tilePrefab, new Vector3(x, -y, 0.0f), quaternion.identity,
                    transform);
                tileGameObject.name = $"Tile({x},{y})";

                Tile tile = tileGameObject.GetComponent<Tile>();
                Vector2Int newPosition = new Vector2Int(x, y);
                availablePositions.Add(newPosition);

                tiles[x, y] = tile;
            }
        }
    }

    private void CenterCameraPosition()
    {
        Vector3 newCameraPos = new Vector3(mapSize / 2, -(mapSize / 2), -10.0f);
        if ((mapSize % 2) == 0)
        {
            newCameraPos.x -= 0.5f;
            newCameraPos.y += 0.5f;
        }
        cameraRef.transform.position = newCameraPos;
    }
    
    private void SpawnSnake()
    {
        int halfMapSize = mapSize / 2;
        Vector3 middlePos = tiles[halfMapSize, halfMapSize].transform.position;
        GameObject snakeGameObject = Instantiate(snakePrefab, middlePos, Quaternion.identity, transform);
        snake = snakeGameObject.GetComponent<Snake>();
        snake.map = this;
        snake.gameManager = gameManager;
        snake.SetupInitialBody(new Vector2Int(halfMapSize, halfMapSize));
    }

    private void SpawnFood()
    {
        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector2Int randomTile = availablePositions[randomIndex];
        Tile tile = tiles[randomTile.x, randomTile.y];
        GameObject appleGameObject = Instantiate(foodPrefab, tile.transform);
        tile.currentGameObject = appleGameObject;
        tile.isFood = true;
    }

    public Vector3 GetTilePosition(Vector2Int tilePos)
    {
        return tiles[tilePos.x, tilePos.y].transform.position;
    }

    public void SnakeAteFood()
    {
        if (CheckIfWin())
        {
            gameManager.Win(snake.MoveCount);
        }
        else
        {
            SpawnFood();
        }
    }

    public bool IsValidMove(Vector2Int tilePos)
    {
        return tilePos.x >= 0 && tilePos.x < mapSize &&
               tilePos.y >= 0 && tilePos.y < mapSize &&
               tiles[tilePos.x, tilePos.y].IsValidTile;
    }
    
    public void FreeTile(Vector2Int tilePos)
    {
        tiles[tilePos.x, tilePos.y].SetTileValid();
        if (!availablePositions.Contains(tilePos))
        {
            availablePositions.Add(tilePos);
        }
    }

    public void LockTile(Vector2Int tilePos)
    {
        tiles[tilePos.x, tilePos.y].SetTileInvalid();
        availablePositions.Remove(tilePos);
    }

    public bool IsTileApple(Vector2Int tilePos)
    {
        Tile tempTile = tiles[tilePos.x, tilePos.y];
        LockTile(tilePos);
        if (tempTile.isFood)
        {
            tempTile.TileHit();
            return true;
        }

        return false;
    }

    private bool CheckIfWin()
    {
        return availablePositions.Count == 0;
    }
}
