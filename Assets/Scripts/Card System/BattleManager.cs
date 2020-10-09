using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Text targetNumberDisplay;
    public Text currentNumberDisplay;
    public Dealer dealer;
    public BattleMode battleMode;

    // Start is called before the first frame update
    void Start()
    {
        dealer.FillDeck(battleMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum BattleMode { Beginner, Intermediate, Expert }