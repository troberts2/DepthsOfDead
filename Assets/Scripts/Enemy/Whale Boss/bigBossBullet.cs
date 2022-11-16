using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossBullet : MonoBehaviour
{
    [SerializeField] private GameObject normalBullet;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.forward * 3 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("wall")){
            SpawnProjectiles(15);
            Destroy(gameObject);
        }
    }
    void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {

            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)transform.position).normalized * 5;

            var proj = Instantiate(normalBullet, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
}
