using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();

    [SerializeField] List<ItemScriptableObject> itemData = new List<ItemScriptableObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetUpItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpItem()
    {
        foreach(var n in itemData)
        {
            Item item = Instantiate(items[RandomItemEffect()],n.itemData.ItemPosition,Quaternion.identity);
            item.item = n;
            item.SetSprite();
        }
    }

    int RandomItemEffect()
    {
        var randNum = Random.RandomRange(0,items.Count);
        return randNum;
    }   
}
