using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : EnemyCollides
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        if (PlayerDies == true)
        {
            EnableGameOverMenu();
        }
    }

    private void OnDisable()
    {
        if (PlayerDies == false)
        {
            DisableGameOverMenu();
        }
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void DisableGameOverMenu()
    {
        gameOverMenu.SetActive(false);
    }
}
