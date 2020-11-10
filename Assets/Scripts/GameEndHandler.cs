using UnityEngine;
using System.Collections;

public class GameEndHandler : MonoBehaviour
{
    public event System.Action onButtonClicked;

    public void OnPlayAgain()
    {
        onButtonClicked?.Invoke();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
