using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public AudioClip BGM;


    private void Awake()
    {
        // Make it Singelton
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(this);
        }
        else if (gm != this)
            Destroy(gameObject);
    }
}

public enum EndType { Win, Lose, Draw }
