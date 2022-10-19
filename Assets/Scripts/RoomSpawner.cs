using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{

    public int currentScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ScenePickRandom();
        }
    }
    public void ScenePickRandom()
    {
        int index = Random.Range(0, 3);
        while(index == SceneManager.GetActiveScene().buildIndex){
            index = Random.Range(0, 3);
        }
        SceneManager.LoadScene(index);
        
        
    }

}
