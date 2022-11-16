using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PufferfishEnemyBehaviour : Enemy
{
    [SerializeField] private GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream:Assets/Scripts/Enemy/PufferfishEnemyBehaviour.cs
       target = GameObject.FindGameObjectWithTag("Player").transform;
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
=======
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
>>>>>>> Stashed changes:Assets/Scripts/Enemy/Basic Enemies/PufferfishEnemyBehaviour.cs
    }

    void Update(){
        if(Vector3.Distance(transform.position, target.position) <= attackRadius && currentState != EnemyState.attack){
            StartCoroutine(ExplodeFish());
        }
    }
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
<<<<<<< Updated upstream:Assets/Scripts/Enemy/PufferfishEnemyBehaviour.cs
            if(currentState == EnemyState.idle || currentState == EnemyState.walk ){
                rb.MovePosition(temp);
                Debug.Log("enemy move");
=======
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger && currentState != EnemyState.attack){
                temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                transform.position = temp;
                Debug.Log("pufferfish move");
>>>>>>> Stashed changes:Assets/Scripts/Enemy/Basic Enemies/PufferfishEnemyBehaviour.cs
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


    IEnumerator ExplodeFish(){
        ChangeState(EnemyState.attack);
        Vector2 direction = target.position - transform.position;
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1f);
        //rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(direction * 2, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("attack", true);
        Debug.Log("pufferfish dive");
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);

    }
}
