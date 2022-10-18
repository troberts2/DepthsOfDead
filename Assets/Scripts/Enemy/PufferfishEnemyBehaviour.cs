using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PufferfishEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(explosion, player.position, Quaternion.identity);
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("attack"))
        {
            Destroy(gameObject);
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }


}
