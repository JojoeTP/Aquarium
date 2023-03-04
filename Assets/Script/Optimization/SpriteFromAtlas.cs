using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField] string spriteKey;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = AddressablesManager.inst.GetSpriteAtlas().GetSprite(spriteKey);
    }
}
