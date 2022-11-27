using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{
    public int currentScene;
    private PlayerMovement pm;
    private int bossSceneNum = 13;
    private JsonSerializer Serializer;
    void Start(){
        Serializer = GetComponent<JsonSerializer>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ScenePickRandom();
        }
    }
    public void OnDisable()
    {
        Debug.Log("debug");
    }
    public void ScenePickRandom()
    {
        Serializer.Save();
        if(pm.roomNum > 5){
            SceneManager.LoadScene(bossSceneNum);
        }else{
            int index = Random.Range(1, 12);
            while(index == currentScene){
                index = Random.Range(1, 12);
            }
            SceneManager.LoadScene(index);
        }  
    }

}
