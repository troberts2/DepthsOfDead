using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishProjectileScript : MonoBehaviour
{
    private Transform target;
    private Vector2 direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        direction = target.position - transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * 1.5f;
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("wall") || collider.CompareTag("Grab") || collider.CompareTag("Player")){
            Destroy(gameObject);
        }

    }
}
