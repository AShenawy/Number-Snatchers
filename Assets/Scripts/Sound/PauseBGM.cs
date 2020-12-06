using UnityEngine;



// temporarily pauses BGM when instantiated. then resumes BGM when destroyed
// this is mainly for flash and flow cards which pop up
public class PauseBGM : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PauseBGM(true);
    }

    private void OnDestroy()
    {
        SoundManager.instance.PauseBGM(false);
    }
}
