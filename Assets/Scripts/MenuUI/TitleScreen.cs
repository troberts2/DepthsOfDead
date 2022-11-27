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
        pm.SetInit();
        Serializer.Save();
        if (toggleTutorial.activeInHierarchy == false)
        {
            int index = Random.Range(1, 12);
            while (index == SceneManager.GetActiveScene().buildIndex)
            {
                index = Random.Range(1, 12);
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
    public void Tutorial()
    {
        SceneManager.LoadScene(14);
    }
    public void TutorialNext()
    {
        SceneManager.LoadScene(15);

    }
    public void ReturnToStart()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene(11);
    }
}
