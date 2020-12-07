using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDisplay : MonoBehaviour
{
    public Image nPCCardDisplay;
    public Text nPCNameDisplay;
    public Image speechBubble;
    public Text speechTextDisplay;
    public float reactionDisplayTime = 2f;     // how long the reaction is on before returning to normal state
    Sprite nPCDefault;


    public void SetDefaultSprite(Sprite defaultSprite)
    {
        nPCDefault = defaultSprite;
        nPCCardDisplay.sprite = defaultSprite;
        nPCCardDisplay.enabled = true;
    }

    public void SetName(string nPCName)
    {
        nPCNameDisplay.text = nPCName;
    }

    public void SetReaction(string text, Sprite sprite)
    {
        speechBubble.enabled = true;
        speechTextDisplay.text = text.ToUpper();
        nPCCardDisplay.sprite = sprite;

        StartCoroutine(ResetDisplay());
    }

    IEnumerator ResetDisplay()
    {
        yield return new WaitForSeconds(reactionDisplayTime);

        speechTextDisplay.text = "";
        speechBubble.enabled = false;
        nPCCardDisplay.sprite = nPCDefault;
    }
}
