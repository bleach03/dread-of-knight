using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCollides : MonoBehaviour
{

    public bool PlayerDies = false;
    public GameObject gameOverMenu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDies = true;
            Debug.Log("Player dies");
            gameOverMenu.SetActive(true);
        }
    }
}
