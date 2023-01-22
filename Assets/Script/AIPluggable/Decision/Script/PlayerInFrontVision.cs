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
            if(controller.IsPlayerInRange(controller.visionRange))
                return true;
            else
                return false;
        }
    }
}