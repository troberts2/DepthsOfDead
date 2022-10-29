using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{
    public int currentScene;
    void Start(){
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
        int index = Random.Range(1, 5);
        while(index == currentScene){
            index = Random.Range(1, 5);
        }
        SceneManager.LoadScene(index);
        
        
    }

}
