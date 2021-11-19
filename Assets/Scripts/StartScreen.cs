using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private CGameManager gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameManager.ResumeGame();
            gameObject.SetActive(false);
        }
    }
}
