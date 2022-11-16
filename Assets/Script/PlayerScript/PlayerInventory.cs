using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemScriptableObject> itemList {get {return ItemList;}}

    List<ItemScriptableObject> ItemList = new List<ItemScriptableObject>();
    [SerializeField] public InventoryPanel inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem(ItemData item)
    {
        
    }

    public void OpenInventory()
    {
        if(inventoryPanel.gameObject.activeInHierarchy)
            inventoryPanel.gameObject.SetActive(false);
        else
            inventoryPanel.gameObject.SetActive(true);
    }

    public void OnOpenInventory()
    {
        OpenInventory();
    }
}
