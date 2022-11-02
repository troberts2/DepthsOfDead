using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject resumeGame;
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
        }
    }

    public void resume()
    {
        Time.timeScale = 1f;
        resumeGame.SetActive(false);

    }
}
