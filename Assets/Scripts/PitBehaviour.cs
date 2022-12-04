using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehaviour : MonoBehaviour
{

    public GameObject player;
    public Transform respawnPoint;
    PlayerMovement pm;
    

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
