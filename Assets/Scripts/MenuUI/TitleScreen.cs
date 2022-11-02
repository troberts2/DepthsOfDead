using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject toggleTutorial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (toggleTutorial.activeInHierarchy == false)
        {
            int index = Random.Range(0, 3);
            while (index == SceneManager.GetActiveScene().buildIndex)
            {
                index = Random.Range(0, 3);
            }
            SceneManager.LoadScene(index);

            Debug.Log("this will start the game");
        }
        else if (toggleTutorial.activeInHierarchy == true)
        {
            Debug.Log("would start tutorial");
        }





    }

    public void ToggleOn()
    {
        Debug.Log("attempting to toggle");
        toggleTutorial.SetActive(true);

        
        

        
    }
    public void ToggleOff()
    {
        if (toggleTutorial.activeInHierarchy == true)
        {
            toggleTutorial.SetActive(false);

        }

    }
}
