using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Inst;
    [SerializeField] bool testScene = false;
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
            switch(n.effectTYPE)
            {
                case EFFECTTYPE.WINK :
                    CreateItem(n,winkEffectPrefab);
                    break;
                case EFFECTTYPE.INVERTCOLOR :
                    CreateItem(n,invertColorEffectPrefab);
                    break;
                case EFFECTTYPE.LIGHT :
                    CreateItem(n,lightEffectPrefab);
                    break;
                case EFFECTTYPE.FIREFLY :
                    CreateItem(n,outlineEffectPrefab);
                    break;
                case EFFECTTYPE.BUTTON :
                    CreateItem(n,buttonEffectPrefab);
                    break;
                case EFFECTTYPE.NONE :
                    CreateItem(n,buttonEffectPrefab);
                    break;
            }
            
        }
    }

    void CreateItem(ItemEffectSetting itemEffectSetting ,Item itemPrefab)
    {
        Item item = null;

        foreach(var m in itemData)
        {
            if(itemEffectSetting.iTEMTYPE.HasFlag(m.itemData.ItemType))
            {
                if(!PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(m.itemData.ItemID))
                {
                    item = Instantiate(itemPrefab);
                    item.itemObject = m;
                    item.SetItem();
                    if (item != null)
                    {
                        SetItemAction(item);
                    }
                }
            }
        }
    }

    void LoadItemDataJson(int index)
    {
        itemEffectData = ItemEffectInfo.LoadItemEffectJSON(index);
    }

    void SetItemAction(Item item)
    {
        switch (item.itemObject.itemData.ItemType)
        {
            case ITEMTYPE.ITEM1:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM2:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM3:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.SetActiveDialogueCh1_D11_01();
                        ActionEventManager.inst.SetActiveFalse_Wall_Cafeteria();
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM4:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM5:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.OnPickUpLabyrinthCoin();
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                    }
                );
                break;
            case ITEMTYPE.ITEM6:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.AlertText(10.0f);
                        ActionEventManager.inst.SpawnSister(false, 10.0f);
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

    public ItemData GetItemByID(string id)
    {
        return itemData.Find(n => n.itemData.ItemID == id).itemData;
    } 

    // int RandomItemEffect()
    // {
    //     var randNum = Random.RandomRange(0,items.Count);
    //     return randNum;
    // }   

}
