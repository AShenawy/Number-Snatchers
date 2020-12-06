using UnityEngine;
using System.Collections;

public class ButtonSFX : MonoBehaviour
{
    public AudioClip clickSFX;
    
    public void PlayClickSFX()
    {
        SoundManager.instance.PlaySFX(clickSFX);
    }
}
