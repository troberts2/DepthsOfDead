using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    // Start is called before the first frame update
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource Music;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayAudioClip(AudioClip PlayThis)
    {
        menuSound.PlayOneShot(PlayThis);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
