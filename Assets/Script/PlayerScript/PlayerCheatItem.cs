using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheatItem : MonoBehaviour
{
    public static PlayerCheatItem inst;
    public List<Transform> warpPosition;
    [SerializeField] bool canWarpToItem;

   void Awake()
   {
        inst = this;
   }

    private void Start()
    {
        InputSystemManager.Inst.onCheatItem += WarpPlayer;
    }

    private void OnDestroy()
    {
        InputSystemManager.Inst.onCheatItem -= WarpPlayer;
    }

    void WarpPlayer()
    {
        if (warpPosition.Count == 0 || canWarpToItem == false || PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
        {
            return;
        }
        transform.position = warpPosition[0].transform.position;
    }

    public void RemoveFirstWarpPositionIndex()
    {
        warpPosition.RemoveAt(0);
    }

    public void CanWarpToItem()
    {
        canWarpToItem = true;
    }

}
