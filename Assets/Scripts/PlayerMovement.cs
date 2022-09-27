using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    private float mx, my;

    [SerializeField] private float dashSpeed;
    [SerializeField]private float dashLength;
    [SerializeField]private float dashCooldown;
    [SerializeField]private float dashCounter;
    [SerializeField]private float dashCoolCounter;

    private Rigidbody2D rb;
    [SerializeField]private Camera cam;

    private Vector2 mousePos;
    GrappleHook gh;
     
    // Start is called before the first frame update
    void Start()
    {
        gh = GetComponent<GrappleHook>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if(gh.retracting)
        {
            rb.velocity = Vector2.zero;
        }
    }
    IEnumerator Movement()
    {
        while(true)
        {
            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(mx, my).normalized * playerSpeed;

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
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
}
