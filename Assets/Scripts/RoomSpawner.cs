using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{

    public int currentScene;

    public void ScenePickRandom()
    {
        int index = Random.Range(1, 5);
        SceneManager.LoadScene(index);
        
        
    }

}
