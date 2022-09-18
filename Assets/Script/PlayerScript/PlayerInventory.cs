using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemScriptableObject> itemList;

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
}
