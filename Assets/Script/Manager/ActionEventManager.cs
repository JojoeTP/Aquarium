using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager inst;

    [Header("Labyrinth")]
    public SpriteRenderer labyrinthMap;
    public Sprite labyrinthMapSprite;

    private void Awake() 
    {
        inst = this;
    }
    
#region ItemActionEvent
    public void OnPickUpLabyrinthCoin()
    {
        labyrinthMap.sprite = labyrinthMapSprite;
    }
#endregion

}
