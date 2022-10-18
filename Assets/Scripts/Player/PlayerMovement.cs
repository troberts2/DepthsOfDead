using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private int playerHealth = 6;
    private bool iframes = false;
    private float mx, my;

    [SerializeField] private float dashSpeed;
    [SerializeField]private float dashLength;
    [SerializeField]private float dashCooldown;
    [SerializeField]private float dashCounter;
    [SerializeField]private float dashCoolCounter;
    public TextMeshProUGUI livesText;

    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]private Camera cam;

    private Vector2 mousePos;
    GrappleHook gh;
    private bool canHit = true;
    private GameObject attacki;
    [SerializeField]private GameObject attackPrefab;
    [SerializeField]private Transform shootPoint;
     
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gh = GetComponent<GrappleHook>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(Movement());
    }

    void FixedUpdate()
    {
        if(gh.retracting)
        {
            rb.velocity = Vector2.zero;
        }
        attack();
        livesText.text = "Lives: " + playerHealth;

    }
    IEnumerator Movement()
    {
        while(true)
        {
            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(mx, my).normalized * playerSpeed;

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if(mx != 0 || my !=0)
            {
                animator.SetFloat("moveX", mx);
                animator.SetFloat("moveY", my);
                animator.SetBool("moving", true);
            }else
            {
                animator.SetBool("moving", false);
            }
            
            Dash();
            yield return null;
        }
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                playerSpeed = dashSpeed;
                dashCounter = dashLength;

                    
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                playerSpeed = 5f;
                dashCoolCounter = dashCooldown;

            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime; 
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy") && !iframes)
        {
            StartCoroutine(TakeDamage(1));
        }
        if(collider.CompareTag("EnemyAttack") && !iframes)
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int amt){
        playerHealth -= amt;
        iframes = true;
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        if(playerHealth < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        yield return new WaitForSeconds(.5f);
        sr.color = ogColor;
        iframes = false;
    }
    IEnumerator attackRate()
    {
        canHit = false;

        yield return new WaitForSeconds(0.25f);
        Destroy(attacki);
        canHit = true;
    }
    void attack()
    {
        
        if(Input.GetKey(KeyCode.F) && canHit)
        {
            attacki = Instantiate(attackPrefab, shootPoint.position, transform.rotation);
            StartCoroutine(attackRate());
        }
    }
}
