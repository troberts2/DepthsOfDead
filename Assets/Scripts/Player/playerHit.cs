using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Enemy"))
        {
            
        }
    }
}
