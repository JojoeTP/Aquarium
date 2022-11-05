using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInAttackRange")]
    public class PlayerInAttackRange : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isPlayerInVision = IsPlayerInVision(controller);
            return isPlayerInVision;
        }

        bool IsPlayerInVision(StateController controller)
        {
            if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.attackRange,playerLayer))
                return true;
            else
                return false;
        }
    }
}
