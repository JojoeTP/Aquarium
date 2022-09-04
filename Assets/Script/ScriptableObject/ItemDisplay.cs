using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Item item;
    public Text nameText;
    public Text descriptionText;
    public Text pageNumberText;

    void Start()
    {
        nameText.text = item.itemName;
        descriptionText.text = item.itemDescription;
        pageNumberText.text = item.pageNumber.ToString();
    }

}
