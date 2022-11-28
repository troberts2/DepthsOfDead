using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene(18);
            }
        }
    }
    void Movement(){
        if(transform.position.y > 3.85f){
            moveUp = false;
        }
        else if(transform.position.y < -2.7f){
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
        yield return new WaitForSeconds(4f);
        StartCoroutine(SpawnProjectiles(15));

    }

    /*IEnumerator CannonExplodeAttack(){
        yield return new WaitForSeconds(4f);
        GameObject bul1 = Instantiate(bigBossBullet, shootPoint.position, Quaternion.identity);
        bul1.transform.LookAt(new Vector3(player.position.x, player.position.y + 1, 0));
        GameObject bul2 = Instantiate(bigBossBullet, shootPoint.position, Quaternion.identity);
        bul2.transform.LookAt(new Vector3(player.position.x, player.position.y - 1, 0));
        yield return new WaitForSeconds(4f);
        StartCoroutine(CannonExplodeAttack());


    }*/
    IEnumerator SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;
        for(int j = 0; j < 3; j++){
            for (int i = 0; i <= numberOfProjectiles - 1; i++)
            {

                float projectileDirXposition = shootPoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5;
                float projectileDirYposition = shootPoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5;

                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - (Vector2)shootPoint.position).normalized * 5;

                var proj = Instantiate(normalBullet, shootPoint.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                angle += angleStep;
            }   
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(SpawnProjectilesRotation(15));
    }

    IEnumerator SpawnProjectilesRotation(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {

            float projectileDirXposition = shootPoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5;
            float projectileDirYposition = shootPoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)shootPoint.position).normalized * 5;

            var proj = Instantiate(normalBullet, shootPoint.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(basicSprayAttack());
    }
}