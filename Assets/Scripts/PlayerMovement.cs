using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

  
    



    public Rigidbody2D rb;
    public Camera cam;


 
    Vector2 movement;
    Vector2 mousePos;
     
    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = movement * activeMoveSpeed;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                Debug.Log("spacebar");
                    
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = playerSpeed;
                dashCoolCounter = dashCooldown;

            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime; 
        }
     

        
      
    }
    void FixedUpdate()
    {
        
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
