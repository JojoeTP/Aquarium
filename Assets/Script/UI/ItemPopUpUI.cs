using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopUpUI : MonoBehaviour
{
    public static ItemPopUpUI inst;

    Canvas canvas;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] Button closeButton;

    Item pickUpItem;

    void Awake() 
    {
        canvas = GetComponent<Canvas>();    
    }
    
    void Start()
    {
        inst = this;

        Initialize();
    }

    void Initialize()
    {
        canvas.enabled = false;

        closeButton.GetComponent<Button>().onClick.AddListener(() => 
        {
            OnCloseItemPopUpUI();
        });

    }

    public void OnOpenItemPopUpUI(Item item)
    {
        pickUpItem = item;
        SetItemDetail(item.itemObject.itemData);
        canvas.enabled = true;

        
    }
    
    void OnCloseItemPopUpUI()
    {
        canvas.enabled = false;

        pickUpItem.OnPickUpEvent();
        pickUpItem.gameObject.SetActive(false);
    }

    void SetItemDetail(ItemData item)
    {
        itemImage.sprite = item.ItemSprite;
        itemName.text = item.ItemName;
    }
}
