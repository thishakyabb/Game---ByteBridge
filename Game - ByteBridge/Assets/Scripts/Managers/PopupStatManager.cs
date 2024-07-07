using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopupStatManager: MonoBehaviour  
{
    public static PopupStatManager Instance;
    public GameObject statTextObject;
    public float popupDuration;
    public float pollRate;
    public GameObject damageStatCanvas;
    public TMP_FontAsset damageFontAsset;
    public TMP_FontAsset healFontAsset;
    public TMP_FontAsset critFontAsset;
    public TMP_FontAsset lifestealFontAsset;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void DamageStat(string statText, Transform t,string id)
    {
        
        var o = Instantiate(statTextObject,t.position,Quaternion.identity,damageStatCanvas.transform);
        var stat = o.GetComponent<PopupStat>();
        stat.id = id;
        stat.text = statText;
        stat.fontAsset = damageFontAsset;
        stat.color = new Color(255, 158, 0);
        Destroy(stat,2f);

    }

    public void HealStat(string statText, Transform t,string id)
    {
        
        var o = Instantiate(statTextObject,t.position,Quaternion.identity,damageStatCanvas.transform);
        var stat = o.GetComponent<PopupStat>();
        stat.id = id;
        stat.text = statText;
        stat.fontAsset = healFontAsset;
        stat.color = new Color(66, 255, 0);
        Destroy(stat,2f);
    }
    public void CritStat(string statText, Transform t,string id)
    {
        
        var o = Instantiate(statTextObject,t.position,Quaternion.identity,damageStatCanvas.transform);
        var stat = o.GetComponent<PopupStat>();
        stat.id = id;
        stat.text = statText;
        stat.fontAsset = critFontAsset;
        stat.color = new Color(255, 0, 0);
        Destroy(stat,2f);
    }
    public void LifeStealStat(string statText, Transform t,string id)
    {
        
        var o = Instantiate(statTextObject,t.position,Quaternion.identity,damageStatCanvas.transform);
        var stat = o.GetComponent<PopupStat>();
        stat.id = id;
        stat.text = statText;
        stat.fontAsset = lifestealFontAsset;
        stat.color = new Color(255, 0, 172);
        Destroy(stat,2f);
    }
}