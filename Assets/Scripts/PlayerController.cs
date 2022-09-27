using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    
    }
    IEnumerator movement()
    {
        while(true)
        {
            float yMove = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
            float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
            transform.Translate(xMove, yMove, 0f);
            yield return null;
        }
    }
}
