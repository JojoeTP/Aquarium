using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInAttackRangeIncludeBehide")]
    public class PlayerInAttackRangeIncludeBehide : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isPlayerInVision = IsPlayerInVision(controller);
            return isPlayerInVision;
        }

        bool IsPlayerInVision(StateController controller)
        {
            if(controller.IsPlayerInRangeIncludeBehide(controller.chasingRange))
                return true;
            else
                return false;
        }
    }
}
