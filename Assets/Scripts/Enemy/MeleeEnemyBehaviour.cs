using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeEnemyBehaviour : Enemy
{

    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
        if(collision.CompareTag("attack"))
        {
            StartCoroutine(TakeDamage());
        }
    }
    void CheckDistance(){
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        ChangeAnim(temp - transform.position);
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
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


    private void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
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
}
