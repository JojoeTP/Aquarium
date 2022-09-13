using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class ItemDisplay : MonoBehaviour
{
    public ItemScriptableObject item;
    public Text nameText;
    public Text descriptionText;
    public Text pageNumberText;

    void Start()
    {
        nameText.text = item.itemData.ItemName;
        descriptionText.text = item.itemData.ItemDescription;
        pageNumberText.text = item.pageNumber.ToString();
    }

}
