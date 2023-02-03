using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerInBackVision")]
    public class PlayerInBackVision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return IsPlayerInBackVision(controller);
        }

        bool IsPlayerInBackVision(StateController controller)
        {
            if(controller.IsPlayerInRange(-controller.visionRange))
                return true;
            else
                return false;
        }
    }
}