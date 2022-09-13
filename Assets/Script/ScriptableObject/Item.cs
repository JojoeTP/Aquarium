using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemScriptableObject item;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemData.ItemSprite;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
