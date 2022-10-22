using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harpoongunrotation : MonoBehaviour
{
    private Vector2 mousePos;
    private Camera cam;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; 
        rb.rotation = angle;
    }
}
