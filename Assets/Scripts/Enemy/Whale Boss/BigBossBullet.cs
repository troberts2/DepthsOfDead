using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    void Update(){
        transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }
    public void SetMoveDirection(Vector2 dir){
        moveDirection = dir;
    }

    void OnCollisionEnter2D(Collision2D collider){
        Destroy(gameObject);
    }
}
