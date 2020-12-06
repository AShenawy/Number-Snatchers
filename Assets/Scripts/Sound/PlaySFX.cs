using UnityEngine;
using System.Collections;

public class PlaySFX : MonoBehaviour
{
    public AudioClip SFX;

    // Use this for initialization
    void Start()
    {
        if (SFX)
            SoundManager.instance.PlaySFX(SFX);
    }
}
