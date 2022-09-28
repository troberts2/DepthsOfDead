using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] private LayerMask grapplableMask;
    [SerializeField] private float maxDist = 100f;
    [SerializeField] private float grappleSpeed = 10f;
    [SerializeField] private float grappleShootSpeed = 20f;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private bool canHit = true;
    [SerializeField] private GameObject attackPrefab;
    private GameObject attacki;

    private bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    private Vector2 target;
    private GameObject targetObj;
    private GameObject impHarpoon;
    private bool isPulling;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        //StartCoroutine(meleeAttack());
    }
    void Update()
    {
        attack();
        //shoots grapple if not already grappling
        if(Input.GetMouseButtonDown(0) && !isGrappling) 
        {
            StartGrapple();
        }

        if(retracting)
        {
            Vector2 grapplePos;

            if(isPulling)
            {
                grapplePos = Vector2.Lerp(targetObj.transform.position, transform.position, grappleSpeed * Time.deltaTime);
                targetObj.transform.position = grapplePos;
            }else
            {
                grapplePos = Vector2.Lerp(transform.position, targetObj.transform.position, grappleSpeed * Time.deltaTime);
                transform.position = grapplePos;
            }

            line.SetPosition(0, transform.position);
            line.SetPosition(1, targetObj.transform.position);
            impHarpoon.transform.position = targetObj.transform.position;

            if(Vector2.Distance(transform.position, targetObj.transform.position) < 2f)
            {
                Debug.Log("r pressed");
                Destroy(impHarpoon);
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r pressed");
            Destroy(impHarpoon);
            retracting = false;
            isGrappling = false;
            line.enabled = false;
        }
    }

    void StartGrapple()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDist, grapplableMask);
        //if raycast hit enemy layer shoot a line to it
        if(hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            targetObj = hit.collider.gameObject;
            line.enabled = true;
            line.positionCount = 2;
            //will shoot the ray cast at target
            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;
        impHarpoon = Instantiate(harpoon, shootPoint.position, transform.rotation);
        //this is the part that send the line renderer
        for(; t< time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, targetObj.transform.position, t / time);
            impHarpoon.transform.position = newPos;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        line.SetPosition(1, target);
        bool done = false;
        while(!done)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, targetObj.transform.position);
            impHarpoon.transform.position = targetObj.transform.position;
            if(targetObj.layer == LayerMask.NameToLayer("Enemy"))
            {
                if(Input.GetKeyDown(KeyCode.S)){
                isPulling = true;
                done = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.W)){
                isPulling = false;
                done = true;
            }
            yield return null;
        }
        retracting = true;
    }
    IEnumerator attackRate()
    {
        canHit = false;

        yield return new WaitForSeconds(0.25f);
        Destroy(attacki);
        canHit = true;
    }
    void attack()
    {
        
        if(Input.GetKey(KeyCode.F) && canHit)
        {
            attacki = Instantiate(attackPrefab, shootPoint.position, transform.rotation);
            StartCoroutine(attackRate());
        }
    }
}
