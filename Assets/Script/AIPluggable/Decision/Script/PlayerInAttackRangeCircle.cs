using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInAttackRangeCircle")]
    public class PlayerInAttackRangeCircle : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isPlayerInVision = IsPlayerInVision(controller);
            return isPlayerInVision;
        }

        bool IsPlayerInVision(StateController controller)
        {
            if(controller.IsPlayerInRangeCircle(controller.attackRange))
                return true;
            else
                return false;
        }
    }
}
