using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Inst;
    [SerializeField] bool isPermutation = false;
    [SerializeField] int participateID;
    ItemEffectInfo itemEffectData;
    public ItemEffectInfo ItemEffectData {get {return itemEffectData;}}

    [Header("ItemEffect")]
    [SerializeField] Item winkEffectPrefab;
    [SerializeField] Item invertColorEffectPrefab;
    [SerializeField] Item lightEffectPrefab;
    [SerializeField] Item outlineEffectPrefab;
    [SerializeField] Item buttonEffectPrefab;

    [SerializeField] List<ItemScriptableObject> itemData = new List<ItemScriptableObject>();

    public int ParticipateId {get{ return participateID;} set {participateID = value;}}

    private void Awake() 
    {
        if(Inst == null)
            Inst = this;    
    }

    void Start()
    {
        
    }

    // void SetUpItem()
    // {
    //     foreach(var n in itemData)
    //     {
    //         Item item = Instantiate(items[RandomItemEffect()],n.itemData.ItemPosition,Quaternion.identity);
    //         item.item = n;
    //         item.SetSprite();
    //     }
    // }

    //Run when load scene
    public void SetUpItemPermutation()
    {
        LoadItemDataJson(participateID);

        foreach(var n in itemEffectData.ItemEffectSettingList)
        {
            Item item = null;
            switch(n.effectTYPE)
            {
                case EFFECTTYPE.WINK :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(winkEffectPrefab);
                            item.item = m;
                            item.SetItem();
                            if (item != null)
                            {
                                SetItemAction(item);
                            }
                        }
                    }
                    break;
                case EFFECTTYPE.INVERTCOLOR :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(invertColorEffectPrefab);
                            item.item = m;
                            item.SetItem();
                            if (item != null)
                            {
                                SetItemAction(item);
                            }
                        }
                    }
                    break;
                case EFFECTTYPE.LIGHT :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(lightEffectPrefab);
                            item.item = m;
                            item.SetItem();
                            if (item != null)
                            {
                                SetItemAction(item);
                            }
                        }
                    }
                    break;
                case EFFECTTYPE.FIREFLY :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(outlineEffectPrefab);
                            item.item = m;
                            item.SetItem();
                            if (item != null)
                            {
                                SetItemAction(item);
                            }
                        }
                    }
                    break;
                case EFFECTTYPE.BUTTON :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(buttonEffectPrefab);
                            item.item = m;
                            item.SetItem();
                            if (item != null)
                            {
                                SetItemAction(item);
                            }
                        }
                    }
                    break;
            }
            
        }
    }

    void LoadItemDataJson(int index)
    {
        itemEffectData = ItemEffectInfo.LoadItemEffectJSON(index);
    }

    void SetItemAction(Item item)
    {
        switch (item.item.itemData.ItemType)
        {
            case ITEMTYPE.ITEM1:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.item.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM2:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.item.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM3:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.item.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM4:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.item.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM5:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.OnPickUpLabyrinthCoin();
                        DialogueManager.inst.currentDialogue = item.item.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM6:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM7:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM8:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM9:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM10:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
        }
    }

    // int RandomItemEffect()
    // {
    //     var randNum = Random.RandomRange(0,items.Count);
    //     return randNum;
    // }   

}
