using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject toggleTutorial;
    [SerializeField]private JsonSerializer Serializer;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private AudioSource StartEffect;

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


           
                
                int index = Random.Range(1, 6);
                while (index == SceneManager.GetActiveScene().buildIndex)
                {
                    index = Random.Range(1, 6);
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

    public void TutorialSceen()
    {
        SceneManager.LoadScene(10);
    }
    public void TutorialSceen2()
    {
        SceneManager.LoadScene(11);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
