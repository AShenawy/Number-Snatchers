using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InfoCard : MonoBehaviour
{
    public event Action onCardDestroyed; 
    public InfoType cardType;
    public Text descriptionText;
    [Range(0f, 10f)]    // how long will the card be up for
    public float cardDuration = 3f;


    private void Start()
    {
        StartCoroutine(DestroyCard());
    }

    IEnumerator DestroyCard()
    {
        yield return new WaitForSeconds(cardDuration);
        Destroy(gameObject);
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    private void OnDestroy()
    {
        onCardDestroyed?.Invoke();
    }
}

public enum InfoType { PlayerChallengeRight, PlayerChallengeWrong, NoChallenge, OpponentChallenge, PlayerGuessRight, 
                       PlayerGuessWrong, EnemyGuessRight, TurnEnd, NewRound, CardDeal, CardPick }
