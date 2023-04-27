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
    [SerializeField] List<ItemScriptableObject> itemList = new List<ItemScriptableObject>();

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
                    // CreateItem(n,buttonEffectPrefab);
                    break;
            }
            
        }
        //Cheat
        PlayerCheatItem.inst.SortItems();
    }

    void CreateItem(ItemEffectSetting itemEffectSetting ,Item itemPrefab)
    {
        Item item = null;

        foreach(var m in itemList)
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
                    //Cheat
                    PlayerCheatItem.inst.items.Add(item);
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
                        ActionEventManager.inst.UnLockDoor_Ch1_D04_01();

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM2:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                        ActionEventManager.inst.ChangeDialogueCh1_D06_01();

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM3:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.SetActiveDialogueCh1_D10_01();
                        ActionEventManager.inst.SetActiveDialogueCh1_D11_01();
                        ActionEventManager.inst.SetActiveFalse_Wall_Cafeteria();
                        ActionEventManager.inst.LockCafeteriaDoor();
                        ActionEventManager.inst.isMap1Done = true;

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM4:
                item.triggerEvents.AddListener( () => 
                    {
                        // ActionEventManager.inst.SetActiveDialogueCh2_D02_01();
                        ActionEventManager.inst.StartPuzzle();
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM5:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.OnPickUpLabyrinthCoin();
                        ActionEventManager.inst.LockLabyrinthDoor();
                        ActionEventManager.inst.isMap2Done = true;
                        // DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        // DialogueManager.inst.StartDialogue();

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM6:
                item.triggerEvents.AddListener( () => 
                    {
                        //เก็บเสร็จเปิด dialogue เเล้วค่อย ให้ alert
                        AiRedHoodController.inst.canPlayerDie = true;
                        ActionEventManager.inst.SetActiveDialogueCh3_D05_01();
                        ActionEventManager.inst.LockDoor_Ch3_D06_01_Config();

                        PlayerCheatItem.inst.AddSpawnItem(6);
                    }
                );
                break;
            case ITEMTYPE.ITEM7:
                item.triggerEvents.AddListener( () => 
                    {
                        DialogueManager.inst.currentDialogue = item.itemObject.itemData.dialogueItemId;
                        DialogueManager.inst.StartDialogue();
                        ActionEventManager.inst.UnLockDoor_Ch3_D04_01_Config();

                        PlayerCheatItem.inst.AddSpawnItem(7);
                    }
                );
                break;
            case ITEMTYPE.ITEM8:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.SetActiveDialogueCh3_D08_01();
                        ActionEventManager.inst.SetActiveDialogueCh3_D09_01();
                        ActionEventManager.inst.OnPickUpCircusCoin();
                        ActionEventManager.inst.LockCircusDoor();
                        ActionEventManager.inst.isMap3Done = true;

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM9:
                item.triggerEvents.AddListener( () => 
                    {
                        ActionEventManager.inst.EnableAIMermaid(false);
                        ActionEventManager.inst.SetActiveDialogueCh4_D03_01();

                        PlayerCheatItem.inst.RemoveItems();
                    }
                );
                break;
            case ITEMTYPE.ITEM10:
                item.triggerEvents.AddListener( () => 
                    {
                        AiDirectorController.inst.spawnAI = true;
                        ActionEventManager.inst.isMap4Done = true;
                        ActionEventManager.inst.LockAquariamDoor();
                        ActionEventManager.inst.SetActiveDialogueCh4_D05_01();
                        ActionEventManager.inst.SetActiveDialogueCh4_D06_01();
                        ActionEventManager.inst.SetActiveDialogueCh4_D07_01();

                        PlayerCheatItem.inst.RemoveItems();
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
