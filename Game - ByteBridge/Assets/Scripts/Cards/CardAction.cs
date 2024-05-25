
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardAction : MonoBehaviour
{
    public Sprite itemSprite;
    public GameObject itemImageComponent;
    public TextMeshProUGUI cardDescGameObject;
    public String cardDesc;
    
    public virtual void OnEnable()
    {
        var image = itemImageComponent.GetComponent<Image>();
        image.sprite = itemSprite;
        cardDescGameObject.text = cardDesc;
    }

    public virtual void CardDo()
    {
        Debug.Log("Card action");
    }
}
