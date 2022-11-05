using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInFrontVision")]
    public class PlayerInFrontVision : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            return IsPlayerInFrontVision(controller);
        }

        bool IsPlayerInFrontVision(StateController controller)
        {
            if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.frontVisionRange,playerLayer))
                return true;
            else
                return false;
        }
    }
}