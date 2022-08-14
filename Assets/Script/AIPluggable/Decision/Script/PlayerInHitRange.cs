using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInHitRange")]
public class PlayerInHitRange : Decision
{
    public float gizmosLevel = .5f;
    public Color gizmosColor = Color.red;
    public LayerMask playerLayer;

    public override bool Decide(StateController controller)
    {
        bool isPlayerInVision = IsPlayerInVision(controller);
        return isPlayerInVision;
    }

    bool IsPlayerInVision(StateController controller)
    {
        Debug.DrawRay(new Vector3(controller.transform.position.x,controller.transform.position.y+gizmosLevel,controller.transform.position.z) ,controller.moveDirection * controller.hitRange,gizmosColor);

        if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.hitRange,playerLayer))
            return true;
        else
            return false;
    }
}
