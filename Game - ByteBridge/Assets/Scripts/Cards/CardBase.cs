using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardBase: MonoBehaviour
{
    private PlayerManager playerInstance;
    
    private CardAction cardActionScript;
    private Animator anim;
    private void Start()
    {
     
        playerInstance = PlayerManager.Instance;
        cardActionScript = GetComponent<CardAction>();
        

    }

    public void ButtonClick()
    {
        cardActionScript.CardDo();
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("flip");
    }
}
