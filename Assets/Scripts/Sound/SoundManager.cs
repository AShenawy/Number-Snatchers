using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip BGMClip;
    public AudioClip winClip, loseClip, drawClip;
    AudioSource BGMPlayer;
    AudioSource SFXPlayer;


    void Awake()
    {
        // Make it Singelton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);

        PlayBGM();
    }

    private void Start()
    {
        AddSFXPlayer();
    }

    void AddSFXPlayer()
    {
        SFXPlayer = gameObject.AddComponent<AudioSource>();
        SFXPlayer.playOnAwake = false;
        SFXPlayer.loop = false;
    }

    void PlayBGM()
    {
        BGMPlayer = gameObject.AddComponent<AudioSource>();
        BGMPlayer.clip = BGMClip;
        BGMPlayer.loop = true;
        BGMPlayer.volume = 0.2f;
        BGMPlayer.Play();
    }

    public void PlayEndChimes(EndType type)
    {
        if (type == EndType.Win)
            SFXPlayer.PlayOneShot(winClip, 0.75f);
        else if (type == EndType.Lose)
            SFXPlayer.PlayOneShot(loseClip, 0.75f);
        else
            SFXPlayer.PlayOneShot(drawClip, 0.75f);
    }

    public void PauseBGM(bool value)
    {
        if (value)
            BGMPlayer.Pause();
        else
            BGMPlayer.Play();
    }

    public void PlaySFX(AudioClip sfxClip, float volScale = 0.75f)
    {
        SFXPlayer.PlayOneShot(sfxClip, volScale);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayEndChimes(EndType.Win);
        }
    }
}