using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public float Range;
    public Transform Target;
    bool Detection = false;
    Vector2 Direction;
    public GameObject Rotator;
    public GameObject Bullet;
    float nextTimeToFire;
    public float FireRate;
    public Transform FirePoint;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 targetPos = Target.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if(rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if(Detection == false)
                {
                    Detection = true;
                    Debug.Log("Detected");
                }
            }
            else
            {
                if(Detection == true)
                {
                    Detection = false;
                    Debug.Log("Not Detected");
                }
            }
        }
        if(Detection)
        {
            Rotator.transform.up = Direction;
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
    }

    void shoot()
    {
       GameObject BulletIns = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
