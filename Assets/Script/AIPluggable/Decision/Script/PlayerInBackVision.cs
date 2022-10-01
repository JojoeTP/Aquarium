using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInBackVision")]
public class PlayerInBackVision : Decision
{
    public LayerMask playerLayer;

    public override bool Decide(StateController controller)
    {
        return IsPlayerInBackVision(controller);
    }

    bool IsPlayerInBackVision(StateController controller)
    {
        if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.backVisionRange,playerLayer))
            return true;
        else
            return false;
    }
}