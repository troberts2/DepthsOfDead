using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int enemyHealth;
    [SerializeField] private GameObject enemyAttack;
    [SerializeField] private float kb;
    private bool canMove = true;
    private Rigidbody2D rb;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TargetPlayer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
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

    IEnumerator TargetPlayer()
    {
        GameObject instAttack;
        while(canMove && Vector2.Distance(transform.position, player.position) > 1.3f)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        instAttack = Instantiate(enemyAttack, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(instAttack);
        StartCoroutine(TargetPlayer());
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
        enemyHealth--;
        while(!done)
        {
            sr.color = Color.red;
            canMove = false;
            //rb.AddForce(kb * player.transform.position, ForceMode2D.Impulse);
            //to do fix the knockback and make it so enemy stops when this happens
            yield return new WaitForSeconds(.5f);
            done = true;
            canMove = true;
            sr.color = ogColor;
        }

        if(enemyHealth < 1)
        {
            Destroy(gameObject);
        }
    }

}
