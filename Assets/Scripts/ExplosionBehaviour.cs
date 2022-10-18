using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplosionBehaviour : MonoBehaviour
{
    public string sceneToLoad;
    [SerializeField] private float destroyTime = .5f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RestartGame();
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
