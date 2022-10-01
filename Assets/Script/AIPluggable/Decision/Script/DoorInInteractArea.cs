using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decesion/DoorInInteractArea")]
public class DoorInInteractArea : Decision
{
    // public LayerMask playerLayer;

    public override bool Decide(StateController controller)
    {
        bool isDoorInInteractArea = IsDoorInInteractArea(controller);
        return isDoorInInteractArea;
        
    }

    bool IsDoorInInteractArea(StateController controller)
    {
        var overlapObj = Physics2D.OverlapCircleAll(controller.transform.position,controller.interactRange);

        foreach(var n in overlapObj)
        {
            if(n.GetComponent<DoorSystem>() != null)
            {
                return true;
            }
        }

        return false;
    }
}
