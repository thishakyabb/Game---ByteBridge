using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardBase: MonoBehaviour
{
    private PlayerManager playerInstance;
    public float weight; 
    private CardAction cardActionScript;
    
    private Animator anim;
    [SerializeField] private AudioClip swoosh;
    private void Start()
    {
     
        playerInstance = PlayerManager.Instance;
        cardActionScript = GetComponent<CardAction>();
    }

    public void ButtonClick()
    {
        SoundManager.Instance.PlaySoundClip(swoosh,transform,1f);
        cardActionScript.CardDo();
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("flip");
    }
}
