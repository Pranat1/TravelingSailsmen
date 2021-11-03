using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameStatsSingle : MonoBehaviour
{
    private static GameStatsSingle instance;
    public static GameStatsSingle Instance { get {return instance; } }

    public int NumberOfPoints;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void NextLevel()
    {
        NumberOfPoints += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
