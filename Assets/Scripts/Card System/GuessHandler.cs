using UnityEngine;
using UnityEngine.UI;

public class GuessHandler : MonoBehaviour
{
    public delegate void OnSubmit();
    public event OnSubmit onInputSubmitted;
    
    public InputField input;
    public int submittedGuess;


    public void Submit()
    {
        submittedGuess = int.Parse(input.text);
        onInputSubmitted?.Invoke();
    }

    public void ClearInputField()
    {
        input.text = "";
    }
}
