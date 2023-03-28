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
            if(controller.IsPlayerInRange(controller.attackRange,controller.AttackOffset))
                return true;
            else
                return false;
        }
    }
}
