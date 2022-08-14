using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decesion/WallInVisionRange")]
public class WallInVisionRange : Decision
{
    public float gizmosLevel = .5f;
    public Color gizmosColor = Color.red;
    public LayerMask wallLayer;

    public override bool Decide(StateController controller)
    {
        bool isWallInVision = IsWallInVision(controller);
        return isWallInVision;
    }

    bool IsWallInVision(StateController controller)
    {
        Debug.DrawRay(new Vector3(controller.transform.position.x,controller.transform.position.y+gizmosLevel,controller.transform.position.z) ,controller.moveDirection * controller.hitRange,gizmosColor);

        if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.hitRange,wallLayer))
            return true;
        else
            return false;
    }
}