using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public RigidbodyConstraints2D gameplayRestraints;
    [SerializeField] private Vector2 doorPos;
    private GameObject[] enemies;
    public JsonSerializer Serializer;
    [SerializeField] GameObject[] upgrades;
    public GameObject canvas;
    [SerializeField] Transform player;
    private bool cardsDropped = false;
    [SerializeField]private GameObject door;
    [SerializeField] private PlayerMovement pm;
    private Camera cam;


    [SerializeField] private AudioSource DoorAppear;
    // Start is called before the first frame update
    void Start()
    {
        Serializer = GetComponent<JsonSerializer>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Serializer.Load();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ///// NEEED TO FIX THE CARDS
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if(GameObject.FindGameObjectWithTag("Enemy") == null && !cardsDropped){
            cardsDropped = true;
            Debug.Log("spawn cards");
            GameObject upgrade1 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(225, 75), Quaternion.identity);
            upgrade1.transform.SetParent(canvas.transform, false);
            GameObject upgrade2 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(925, 75), Quaternion.identity);
            while(upgrade1 == upgrade2){
                upgrade2 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(925, 75), Quaternion.identity);
                Debug.Log("card switched");
            }
            upgrade2.transform.SetParent(canvas.transform, false);
        }

    }

    public void clearUpgrades(){
        GameObject[] upgrades = GameObject.FindGameObjectsWithTag("upgrade");
        foreach(GameObject upgrade in upgrades)
        GameObject.Destroy(upgrade);
        pm.roomNum++;
        Serializer.Save();
        StartCoroutine(InstantiateDoor());

    }

    IEnumerator InstantiateDoor(){
        door.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Vector3 ogPos = cam.transform.position;
        cam.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, -28f);
        yield return new WaitForSeconds(1f);
        cam.transform.position = ogPos;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = gameplayRestraints;
        Debug.Log("door should appear");
        DoorAppear.Play();
    }
}
