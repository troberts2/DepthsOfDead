using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject[] enemies;
    [SerializeField] GameObject[] upgrades;
    [SerializeField] Transform player;
    private bool cardsDropped = false;
    [SerializeField] private GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debugger();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(GameObject.FindGameObjectWithTag("Enemy") == null && !cardsDropped){
            cardsDropped = true;
            GameObject upgrade1 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(player.position.x -2, player.position.y), Quaternion.identity);
            GameObject upgrade2 = Instantiate(upgrades[Random.Range(0, upgrades.Length)], new Vector2(player.position.x +2, player.position.y), Quaternion.identity);
            Instantiate(door, new Vector2(player.position.x, player.position.y + 2), Quaternion.identity);
        }

    }

    void Debugger(){
        if(Input.GetKey(KeyCode.R)) 
        { 
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        } 
    }
}
