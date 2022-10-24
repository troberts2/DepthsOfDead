using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    private GameObject[] enemies;
    [SerializeField] GameObject[] upgrades;
    public GameObject canvas;
    [SerializeField] Transform player;
    private bool cardsDropped = false;
    [SerializeField] private GameObject door;
    [SerializeField] private PlayerMovement pm;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debugger();
<<<<<<< Updated upstream
=======
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(GameObject.FindGameObjectWithTag("Enemy") == null && !cardsDropped){
            cardsDropped = true;
            Debug.Log("spawn cards");
            GameObject upgrade1 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(225, 75), Quaternion.identity);
            upgrade1.transform.SetParent(canvas.transform, false);
            GameObject upgrade2 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(925, 75), Quaternion.identity);
            upgrade2.transform.SetParent(canvas.transform, false);
            Instantiate(door, new Vector2(player.position.x, player.position.y + 2), Quaternion.identity);
        }

>>>>>>> Stashed changes
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
    }

    public void AddDamage(){
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("damage+");
        pm.baseDamage++;
        clearUpgrades();
    }

    public void AddHealth(){
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("health+");
        pm.playerHealth++;
        clearUpgrades();
    }

    public void AddSpeed(){
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("speed+");
        pm.playerSpeed++;
        clearUpgrades();
    }
}
