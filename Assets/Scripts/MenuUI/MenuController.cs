using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject resumeGame;
    [SerializeField] GameObject quitGame;
    [SerializeField] GameObject restartGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            resumeGame.SetActive(true);
            quitGame.SetActive(true);
            restartGame.SetActive(true);
        }
    }

    public void resume()
    {
        Time.timeScale = 1f;
        resumeGame.SetActive(false);
        quitGame.SetActive(false);
        restartGame.SetActive(false);
    }
    public void quitApp()
    {
        Application.Quit();
    }
    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }
}
