using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class AddressablesManager : MonoBehaviour
{
    public static AddressablesManager inst;

    string MAP_SPRITEATLAS = "MapSpriteAtlas";
    
    AsyncOperationHandle<SpriteAtlas> handle;
    bool isHandleHasValue;

    void Awake() 
    {
        inst = this;    
    }

    void OnDestroy()
    {
        ClearSpriteHandle();
    }

    /// <summary>
    /// Clear it when exit game
    /// </summary>
    public void ClearSpriteHandle()
    {
        if (!isHandleHasValue) return;

        //reset information here

        Addressables.Release(handle);

        handle = default;
        isHandleHasValue = false;
    }

    public void LoadSpriteAtlas()
    {
        StartCoroutine(LoadingSpriteRoutine());
    }

    /// <summary>
    /// Load sprite atlas when start game
    /// </summary>
    private IEnumerator LoadingSpriteRoutine()
    {
        handle = Addressables.LoadAssetAsync<SpriteAtlas>(MAP_SPRITEATLAS);
        
        Debug.Log($"SpriteAtlas Loading : {handle.PercentComplete}");
        
        isHandleHasValue = true;

        yield return new WaitUntil(() => handle.IsDone);

        // var atlas = handle.Result;

        // var sprite = atlas.GetSprite(itemInfo.IconName);

        // displayItemImage.sprite = sprite;
        // if(itemInfo.Type == ItemType.Consumable)
        // {
        //     displayItemDetail.text = userDataManager.UserData.UserDataInventory.ConsumableItems["test01"].ToString();
        // }
        // else if (itemInfo.Type == ItemType.Craftable)
        // {
        //     displayItemDetail.text = userDataManager.UserData.UserDataInventory.CraftableItems["test01"].ToString();
        // }

        // // Set other information here

        // loadingSpriteCoroutine = null;
    }

    public SpriteAtlas GetSpriteAtlas()
    {
        return handle.Result;
    }
}
