using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isValidTile = true;
    
    public GameObject currentGameObject;
    public bool isFood = false;

    public bool IsValidTile => isValidTile;
    
    public void TileHit()
    {
        isFood = false;
        Destroy(currentGameObject);
    }

    public void SetTileInvalid()
    {
        isValidTile = false;
    }

    public void SetTileValid()
    {
        isValidTile = true;
    }
}
