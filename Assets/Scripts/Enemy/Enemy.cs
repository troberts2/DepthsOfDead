using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TakeDamage()
    {
        Color ogColor = GetComponent<SpriteRenderer>().color;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool done = false;
        health--;
        while(!done)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(.5f);
            done = true;
            sr.color = ogColor;
        }

        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}
