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

        AddEventAction();
        
        closeButton.GetComponent<Button>().onClick.AddListener(() => 
        {
            OnCloseItemPopUpUI();
        });
    }

    void AddEventAction()
    {
        PlayerManager.inst.PlayerInteract.OnOpenItemPopUpUI += OnOpenItemPopUpUI;
    }

    void RemoveEventAction()
    {
        PlayerManager.inst.PlayerInteract.OnOpenItemPopUpUI -= OnOpenItemPopUpUI;
    }

    public void OnOpenItemPopUpUI(Item item)
    {
        pickUpItem = item;
        SetItemDetail(item.itemObject.itemData);
        canvas.enabled = true;
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.GETTINGITEM;
    }
    
    void OnCloseItemPopUpUI()
    {
        canvas.enabled = false;

        pickUpItem.OnPickUpEvent();
        pickUpItem.gameObject.SetActive(false);
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }

    void SetItemDetail(ItemData item)
    {
        itemImage.sprite = item.ItemSprite;
        itemName.text = item.ItemName;
    }

    void OnDestroy() 
    {
        RemoveEventAction();
    }
}
