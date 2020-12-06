﻿using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour
{
    AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            source.Play();
    }
}
