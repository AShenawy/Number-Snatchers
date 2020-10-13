using UnityEngine;
using UnityEngine.UI;

public class GuessHandler : MonoBehaviour
{
    public delegate void OnSubmit(int value);
    public event OnSubmit onInputSubmitted;
    
    public InputField input;


    public void Submit()
    {
        //TODO submit the player input to process
    }

    public void ClearInputField()
    {
        input.text = "";
    }
}
