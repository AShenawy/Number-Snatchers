using UnityEngine;
using UnityEngine.UI;

public class GuessHandler : MonoBehaviour
{
    public event System.Action<int> onInputSubmitted;
    public InputField input;
    public Button submitButton;
    public int humanGuess;

    private void Update()
    {
        if (input.text == "")
            submitButton.interactable = false;
        else
            submitButton.interactable = true;
    }

    public void Submit()
    {
        humanGuess = int.Parse(input.text);
        onInputSubmitted?.Invoke(humanGuess);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
