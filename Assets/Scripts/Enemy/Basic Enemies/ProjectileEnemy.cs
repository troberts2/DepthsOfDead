using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileEnemy : Enemy
{
    [SerializeField]private GameObject fishProjectile;
    private bool canShoot = true;
    private NavMeshAgent agent;
    [SerializeField] private AudioSource Prohit;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) <= attackRadius && canShoot){
            GameObject fish = Instantiate(fishProjectile, transform.position, Quaternion.identity);
            StartCoroutine(FireRate());
        }
    }
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance(){
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        ChangeAnim(temp - transform.position);
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                agent.SetDestination(target.position);
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


    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
        if(collision.CompareTag("attack"))
        {
            StartCoroutine(TakeDamage());

        }
        if(collision.CompareTag("pit")){
            Destroy(gameObject);
        }
        if(collision.CompareTag("harpoonHit")){
            agent.enabled = false;
            ChangeState(EnemyState.stagger);
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("harpoonHit")){
            agent.enabled = true;
            ChangeState(EnemyState.idle);
        }
    }
    IEnumerator TakeDamage()
    {
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool done = false;
        health--;
        agent.enabled = false;
        while(!done)
        {
            Prohit.Play();
            sr.color = Color.red;
            yield return new WaitForSeconds(.5f);
            done = true;
            sr.color = ogColor;
            ChangeState(EnemyState.stagger);
        }
        ChangeState(EnemyState.idle);
        agent.enabled = true;
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
    IEnumerator FireRate(){
        canShoot = false;
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
}
