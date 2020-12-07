using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChallengeHandler : MonoBehaviour
{
    public event System.Action<int> onChallengeSubmitted;
    public event System.Action onChallengePassed;

    public InputField input;
    public Button submitButton;
    public Text challengeDetails;
    public int humanChallenge;


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

        if (Input.GetButtonDown("Cancel"))
        {
            PassChallenge();
            DestroyObject();
            GetComponent<ButtonSFX>().PlayClickSFX();
        }

        if (Input.GetButtonDown("Submit") && submitButton.interactable == true)
        {
            Submit();
            DestroyObject();
            GetComponent<ButtonSFX>().PlayClickSFX();
        }
    }

    public void Submit()
    {
        humanChallenge = int.Parse(input.text);
        onChallengeSubmitted?.Invoke(humanChallenge);
    }

    public void PassChallenge()
    {
        onChallengePassed?.Invoke();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
