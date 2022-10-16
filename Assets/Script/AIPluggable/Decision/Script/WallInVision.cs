using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decesion/WallInVision")]
public class WallInVision : Decision
{
    public LayerMask wallLayer;

    public override bool Decide(StateController controller)
    {
        bool isWallInVision = IsWallInVision(controller);
        return isWallInVision;
    }

    bool IsWallInVision(StateController controller)
    {
        if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.frontVisionRange,wallLayer))
            return true;
        else
            return false;
    }
}
