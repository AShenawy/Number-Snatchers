using UnityEngine;
using UnityEngine.UI;

public class ChallengeHandler : MonoBehaviour
{
    public event System.Action<int> onChallengeSubmitted;
    public event System.Action onChallengePassed;

    public InputField input;
    public Button submitButton;
    public Text challengeDetails;
    public int humanChallenge;

    private void Update()
    {
        if (input.text == "")
            submitButton.interactable = false;
        else
            submitButton.interactable = true;
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
