using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Item : MonoBehaviour
{
    public ItemScriptableObject item;
    public ShowInteractKey showInteractKey;
    public Transform interactIconPrefab;
    public Transform effectPrefab;

    public UnityEvent triggerEvents;

    void Start()
    {
        GenerateInteractIcon();
        GenerateEffect();
    }
    public void SetItem()
    {
        this.gameObject.name = item.itemData.ItemName;
        this.transform.position = item.itemData.ItemPosition;
        this.transform.localScale = item.itemData.ItemScale;
        SetSprite();
    }

    public void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemData.ItemSprite; 
    }

    void GenerateInteractIcon()
    {
        Transform icon;

        if(interactIconPrefab != null)
        {
            icon = Instantiate(interactIconPrefab,transform.position,Quaternion.identity);
            showInteractKey.keyImage = icon.gameObject;
            icon.gameObject.SetActive(false);
            icon.SetParent(this.transform);
        }
    }

    void GenerateEffect()
    {
        Transform effect;

        if(effectPrefab != null)
        {
            effect = Instantiate(effectPrefab,transform.position,Quaternion.identity);
            effect.SetParent(this.transform);
        }
    }

    public float GetPickUpTime()
    {
        return Time.time - RecordTimeManager.Inst.getInRoomTime;
    }

    public void OnPickUpEvent()
    {
        triggerEvents.Invoke();
    }
}
