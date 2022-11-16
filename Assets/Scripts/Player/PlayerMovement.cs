using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum playerState{
    idle,
    walk,
    attack
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private playerState currentState;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int playerHealth = 5;
    public int baseDamage = 1;
    public JsonSerializer Serializer;
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
    private GameObject attacki;
    private BoxCollider2D playerCollider;
    [SerializeField]private GameObject attackPrefab;
    public int roomNum;
     
    // Start is called before the first frame update
    void Start()
    {
        Serializer = GetComponent<JsonSerializer>();
        cam = Camera.main;
        gh = GetComponent<GrappleHook>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(Movement());
    }

    void FixedUpdate()
    {
        if(gh.retracting)
        {
            rb.velocity = Vector2.zero;
        }
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
            if(Input.GetKeyDown(KeyCode.F) && currentState != playerState.attack){
                StartCoroutine(AttackCo());
            }
            else if(mx != 0 || my !=0)
            {
                animator.SetFloat("moveX", mx);
                animator.SetFloat("moveY", my);
                animator.SetBool("moving", true);
                ChangeState(playerState.walk);
            }else
            {
                animator.SetBool("moving", false);
                ChangeState(playerState.idle);
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
        if(collider.CompareTag("EnemyAttack") && iframes == false)
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
        yield return new WaitForSeconds(1f);
        sr.color = Color.white;
        iframes = false;
    }
    private IEnumerator AttackCo(){
        animator.SetBool("attack", true);
        ChangeState(playerState.attack);
        yield return null;
        animator.SetBool("attack", false);
        yield return new WaitForSeconds(0.4f);
        ChangeState(playerState.walk);
    }
    private void ChangeState(playerState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }
    public void SetInit(){
        playerHealth = 500;
        baseDamage = 1;
        playerSpeed = 5;
        roomNum = 0;
    }
}
