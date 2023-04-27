using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheatItem : MonoBehaviour
{
    public static PlayerCheatItem inst;
    [HideInInspector] public List<Item> items;
    [SerializeField] List<ItemScriptableObject> itemScriptableObjects;
    [SerializeField] List<Item> sortItems;
    [SerializeField] List<Item> spawnItems;

   void Awake()
   {
        inst = this;
   }

    private void Start()
    {
        InputSystemManager.Inst.onCheatItem += OnSpawnItem;
    }

    private void OnDestroy()
    {
        InputSystemManager.Inst.onCheatItem -= OnSpawnItem;
    }

    void OnSpawnItem()
    {
        if (spawnItems.Count > 0)
        {
            if (!PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(spawnItems[0].itemObject.itemData.ItemID))
            {
                PlayerManager.inst.PlayerInventory.AddItem(spawnItems[0].itemObject);
                ItemPopUpUI.inst.OnOpenItemPopUpUI(spawnItems[0]);
                spawnItems.Remove(spawnItems[0]);
            }
        }
        else
        {
            print("No Item to spawn");
        }
    }

    public void AddSpawnItem(int index)
    {
        if (PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(sortItems[index].itemObject.itemData.ItemID))
        {
            return;
        }
        RemoveItems();
        spawnItems.Add(sortItems[index]);
    }

    public void RemoveItems()
    {
        spawnItems.Clear();
    }
    public void SortItems()
    {
        for (int i = 0; i < itemScriptableObjects.Count; i++) 
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].itemObject.name == itemScriptableObjects[i].name)
                    sortItems.Add(items[j]);
            }
        }
    }
}
