using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class NavMeshMeleeEnemy : Enemy
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private Vector2 homePos;
    NavMeshAgent agent;

    [SerializeField] private GameObject enemyAttack;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
    // Update is called once per frame
  //  void FixedUpdate()
   // {
   //     CheckDistance();
   // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
        if (collision.CompareTag("attack"))
        {
            StartCoroutine(TakeDamage());
            Destroy(collision);
        }
    }
    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }


    private void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator TakeDamage()
    {
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool done = false;
        health--;
        while (!done)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(.5f);
            done = true;
            sr.color = ogColor;
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

}
