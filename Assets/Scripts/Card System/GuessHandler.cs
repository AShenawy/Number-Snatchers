using UnityEngine;
using UnityEngine.UI;

public class GuessHandler : MonoBehaviour
{
    public event System.Action<int> onInputSubmitted;
    public InputField input;
    public Button submitButton;
    public int humanGuess;


    private void Start()
    {
        input.ActivateInputField();
        input.Select();
    }

    private void Update()
    {
        if (input.text == "")
            submitButton.interactable = false;
        else
            submitButton.interactable = true;

        if (Input.GetButtonDown("Submit") && submitButton.interactable == true)
        {
            Submit();
            DestroyObject();
            GetComponent<ButtonSFX>().PlayClickSFX();
        }
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
