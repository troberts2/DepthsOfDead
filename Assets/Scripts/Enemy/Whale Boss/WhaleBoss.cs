using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBoss : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField]private int damage;
    [SerializeField]private int shootSpeed;
    [SerializeField]private GameObject bigBossBullet;
    [SerializeField]private GameObject normalBullet;
    [SerializeField]private Transform shootPoint;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement pm;
    float moveSpeed = 2f;
    bool moveUp = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(basicSprayAttack());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
        if(collision.CompareTag("attack"))
        {
            health -= pm.baseDamage;
            if(health < 0){
                Destroy(gameObject);
            }
        }
    }
    void Movement(){
        if(transform.position.y > 4f){
            moveUp = false;
        }
        else if(transform.position.y < -4f){
            moveUp = true;
        }

        if(moveUp){
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else{
            transform.position = new Vector2(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime));
        }
    }
    IEnumerator basicSprayAttack(){
        yield return new WaitForSeconds(4f);
        float startAngle = 180f;
        float endAngle = 0f;
        int bulletsAmount = 10;
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;
        int sprayLoop = 4;

        for(int j = 0; j < sprayLoop; j++){
            for(int i = 0; i < bulletsAmount + 1; i++){
                float bulDirX = transform.position.x +Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y +Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = Instantiate(normalBullet, shootPoint.position, Quaternion.identity);
                bul.GetComponent<BossBullet>().SetMoveDirection(bulDir);
                angle += angleStep;
            }
            yield return new WaitForSeconds(.5f);
        }
        StartCoroutine(basicSprayAttack());
    }
}