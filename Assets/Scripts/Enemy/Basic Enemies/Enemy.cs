using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Vector2 homePos;
    public Animator anim;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("pit")){
            Destroy(gameObject);
        }
    }
}
