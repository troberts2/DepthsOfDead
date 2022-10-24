using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damupbutt : MonoBehaviour
{
    PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void AddDamage(){
        Debug.Log("button pressed");
        pm.baseDamage++;
    }
}
