using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // The name of the scene to transition to

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Change "Player" to the tag of the object that collides with the scene changer
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
