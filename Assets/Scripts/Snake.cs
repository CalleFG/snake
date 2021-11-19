using System;
using CustomLinkedList;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;

    private Vector2Int direction = Vector2Int.right;
    private KeyCode lastDirection = KeyCode.A;
    private CLinkedList<SnakeBody> snakeBodies;
    private int moveCount = 0;

    [NonSerialized] public CGameManager gameManager;
    [NonSerialized] public Map map;

    public int MoveCount => moveCount;

    private void Awake()
    {
        snakeBodies = new CLinkedList<SnakeBody>();
    }

    public void SetupInitialBody(Vector2Int initialGridPosition)
    {
        for (int i = 0; i < 2; i++)
        {
            initialGridPosition.x -= i;
            
            GameObject snakeBodyGameObject = Instantiate(snakeBodyPrefab,
                map.GetTilePosition(initialGridPosition), Quaternion.identity, map.transform);
            SnakeBody snakeBody = snakeBodyGameObject.GetComponent<SnakeBody>();
            snakeBody.currentGridPosition = initialGridPosition;
            map.LockTile(initialGridPosition);

            snakeBodies.Add(snakeBody);
        }
    }

    void Update()
    {
        ReceiveInputAndUpdateDirection();
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveCount++;
        Vector2Int newPosition = snakeBodies.FirstElement.currentGridPosition + direction;
        
        if (map.IsValidMove(newPosition))
        {
            if (map.IsTileApple(newPosition))
            {
                map.SnakeAteFood();
                Grow();
            }
            else
            {
                map.FreeTile(snakeBodies.LastElement.currentGridPosition);
            }

            UpdateSnakeBodies(newPosition);
        }
        else
        {
            gameManager.Lose(moveCount);
        }
    }

    private void UpdateSnakeBodies(Vector2Int newPosition)
    {
        Vector3 savedPos = map.GetTilePosition(newPosition);
        Vector2Int savedGridPos = newPosition;
        for (int i = 0; i < snakeBodies.Count; i++)
        {
            SnakeBody obj = snakeBodies[i];
            (obj.cachedTransform.position, savedPos) = (savedPos, obj.cachedTransform.position);
            (obj.currentGridPosition, savedGridPos) = (savedGridPos, obj.currentGridPosition);
        }
    }

    private void Grow()
    {
        GameObject snakeBodyGameObject = Instantiate(snakeBodyPrefab,
            transform.position, Quaternion.identity, map.transform);
        SnakeBody snakeBody = snakeBodyGameObject.GetComponent<SnakeBody>();
        snakeBody.currentGridPosition = snakeBodies.LastElement.currentGridPosition;
        snakeBodies.Add(snakeBody);
    }
    
    private void ReceiveInputAndUpdateDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) && lastDirection != KeyCode.S)
        {
            direction = Vector2Int.down;
            lastDirection = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.S) && lastDirection != KeyCode.W)
        {
            direction = Vector2Int.up;
            lastDirection = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.A) && lastDirection != KeyCode.D)
        {
            direction = Vector2Int.left;
            lastDirection = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.D) && lastDirection != KeyCode.A)
        {
            direction = Vector2Int.right;
            lastDirection = KeyCode.D;
        }
    }
}
