using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PufferfishEnemyBehaviour : Enemy
{
    private NavMeshAgent agent;
    [SerializeField] private GameObject explosion;
    private bool isAttacking = false;
    
    [SerializeField] private AudioSource pufferHit;

    // Start is called before the first frame update
    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").transform;
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
    }

    void Update(){
        if(Vector3.Distance(transform.position, target.position) <= attackRadius && currentState != EnemyState.attack){
            StartCoroutine(ExplodeFish());
        }
        if(isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance(){
        Vector3 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        ChangeAnim(temp - transform.position);
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger && currentState != EnemyState.attack){
                temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                transform.position = temp;
                Debug.Log("pufferfish move");
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }else{
            anim.SetBool("wakeUp", false);

        }
    }
    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
    private void ChangeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if(direction.x > 0){
                SetAnimFloat(Vector2.right);
            }else if(direction.x < 0){
                SetAnimFloat(Vector2.left);
            }

        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if(direction.y > 0){
                SetAnimFloat(Vector2.up);
            }else if(direction.y < 0){
                SetAnimFloat(Vector2.down);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("attack"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    public IEnumerator TakeDamage()
    {
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool done = false;
        health--;
        currentState = EnemyState.stagger;
        while(!done)
        {
            sr.color = Color.red;
            pufferHit.Play();
            yield return new WaitForSeconds(.5f);
            done = true;
            sr.color = ogColor;
            currentState = EnemyState.idle;
        }

        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
    
    private void ChangeState(EnemyState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }


    IEnumerator ExplodeFish(){
        ChangeState(EnemyState.attack);
        Vector2 direction = -(transform.position - target.position).normalized;
        Debug.DrawRay(transform.position, direction, Color.blue, 5f);
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(.5f);
        //rb.constraints = RigidbodyConstraints2D.None;
        isAttacking = true;
        yield return new WaitForSeconds(.15f);
        isAttacking = false;
        anim.SetBool("attack", true);

    }
    public void PufferfishDeath()
    {
        Destroy(gameObject);
    }
}
