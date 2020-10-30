using UnityEngine;
using UnityEngine.UI;

public class GuessHandler : MonoBehaviour
{
    public delegate void OnSubmit(int val);
    public event OnSubmit onInputSubmitted;
    
    public InputField input;
    public int humanGuess;


    public void Submit()
    {
        humanGuess = int.Parse(input.text);
        onInputSubmitted?.Invoke(humanGuess);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    //public void ClearInputField()
    //{
    //    input.text = "";
    //}
}
