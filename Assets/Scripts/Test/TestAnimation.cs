using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class TestAnimation : MonoBehaviour, IPointerClickHandler
{
    public GameObject tableObject;
    bool canAnimate;
    bool arrived;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        AnimateCard();
    }

    void AnimateCard()
    {
        print("Animation started");

        //transform.SetParent(tableObject.transform);
        LeanTween.move(GetComponent<RectTransform>(), tableObject.GetComponent<RectTransform>().anchoredPosition, 0.75f).setOnComplete(SayHi);

        print("Animation finished");
    }

    void SayHi()
    {
        print("HI!");
    }
}
