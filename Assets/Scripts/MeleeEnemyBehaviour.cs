using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 3;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            RestartGame();

        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
