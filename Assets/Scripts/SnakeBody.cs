using System;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [NonSerialized] public Transform cachedTransform;
    public Vector2Int currentGridPosition;
    
    private void Awake()
    {
        cachedTransform = GetComponent<Transform>();
    }
}
