using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2 doorPos;
    private GameObject[] enemies;
    public JsonSerializer Serializer;
    [SerializeField] GameObject[] upgrades;
    public GameObject canvas;
    [SerializeField] Transform player;
    private bool cardsDropped = false;
    [SerializeField]private GameObject door;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        Serializer = GetComponent<JsonSerializer>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
<<<<<<< Updated upstream
=======
        Serializer.Load();
        cam = Camera.main;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        ///// NEEED TO FIX THE CARDS
        Debugger();
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

    void Debugger(){
        if(Input.GetKey(KeyCode.R)) 
        { 
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        } 
    }
    public void clearUpgrades(){
        GameObject[] upgrades = GameObject.FindGameObjectsWithTag("upgrade");
        foreach(GameObject upgrade in upgrades)
        GameObject.Destroy(upgrade);
        Serializer.Save();
        InstantiateDoor();

    }

    void InstantiateDoor(){
        door.SetActive(true);
        Debug.Log("door should appear");
        StartCoroutine(DoorPan());
    }

    IEnumerator DoorPan(){
        cam.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, -28f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1f);
        cam.transform.position = new Vector3(player.position.x, player.position.y, -28f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
