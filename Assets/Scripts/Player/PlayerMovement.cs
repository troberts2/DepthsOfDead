using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum playerState{
    idle,
    walk,
    attack,
    dash
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private playerState currentState;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int playerHealth = 5;
    
    [SerializeField] private AudioSource GettingHit;
    [SerializeField] private AudioSource Dashing;

    public int baseDamage = 1;
    public JsonSerializer Serializer;
    private bool iframes = false;
    private float mx, my;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashDistance = 2f;
    public float dashDuration = 0.2f;

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

    void Update ()
    {
        if (isDashing == false)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                Dashing.Play();
                var mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
                var direction = (mousePos - this.transform.position);
 
                // Z-component won't matter in 2D.  If you want to only dash in
                // the X-direction (HK without Dashmaster), then you'd also set
                // your y-component to zero.
                direction.z = 0;
 
                // Making sure we have a reasonable vector here
                if (direction.magnitude >= 0.1f)
                {
                    // Don't exceed the target, you might not want this
                    this.StartCoroutine (this.DashRoutine (direction.normalized));
                }
            }
         }
     }
    public IEnumerator Movement()
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
            
            
            yield return null;
        }
    }
     IEnumerator DashRoutine (Vector3 direction)
    {
        // Account for some edge cases   
        if (dashDistance <= 0.001f)
            yield break;
 
        if (dashDuration <= 0.001f)
        {
            transform.position += direction * dashDistance;
            yield break;
        }
 
        // Update our state
        iframes =true;
        isDashing = true;
        var elapsed = 0f;
        var start = transform.position;
        var target = transform.position + dashDistance * direction;
 
        // There are a few different ways to do this, but I've always preferred
        // Lerp for things that have a fixed duration as the interpolant is clear
        while (elapsed < dashDuration)
        {
            var iterTarget = Vector3.Lerp (start, target, elapsed / dashDuration);
            transform.position = iterTarget;
 
            yield return null;
            elapsed += Time.deltaTime;
        }
 
        // Snap there when we finish then update our state
        transform.position = target;
        isDashing = false;
        iframes = false;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyAttack") && iframes == false)
        {
            StartCoroutine(TakeDamage(1));
        }
        if(collider.CompareTag("pit") && iframes == false)
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int amt){
        playerHealth -= amt;
        iframes = true;
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        GettingHit.Play();
        sr.color = Color.red;
        if(playerHealth < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(17);
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
        playerHealth = 10;
        baseDamage = 1;
        playerSpeed = 5;
        roomNum = 0;
    }
}
