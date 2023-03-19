using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInAttackRangeAndMove")]
    public class PlayerInAttackRangeAndMove : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isPlayerInVision = IsPlayerInVision(controller);
            return isPlayerInVision;
        }

        bool IsPlayerInVision(StateController controller)
        {
            if(controller.IsPlayerInRange(controller.attackRange) && PlayerManager.inst.PlayerMovement.IsMove())
                return true;
            else
                return false;
        }
    }
}
