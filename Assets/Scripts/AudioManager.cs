using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }  //Setting up the Audio Manager as this object, or something



    private void Awake()
    {
        if (Instance == null)                 //This has something to do with 
        {                                     // defining the audio manager and
            Instance = this;                  // making sure it isn't destroyed 
            DontDestroyOnLoad(gameObject);    // when changing between scenes
        }                                     // or something
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {

    }

}
