using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject toggleTutorial;
    [SerializeField]private JsonSerializer Serializer;
    [SerializeField] private PlayerMovement pm;

    
    // Start is called before the first frame update
    void Start()
    {
        Serializer = GetComponent<JsonSerializer>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    public void StartGame()
    {
        pm.UpdateValues(10, 1, 5, 0);
        Serializer.Save();
        int index = Random.Range(1, 6);
        while (index == SceneManager.GetActiveScene().buildIndex)
        {
            index = Random.Range(1, 6);
        }
        SceneManager.LoadScene(index);

        Debug.Log("this will start the game");
        if (toggleTutorial.activeInHierarchy == false)
        {
            SceneManager.LoadScene(1);
            /*int index = Random.Range(1, 6);
            while (index == SceneManager.GetActiveScene().buildIndex)
            {
                index = Random.Range(1, 6);
            }
            SceneManager.LoadScene(index);

            Debug.Log("this will start the game");*/
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
