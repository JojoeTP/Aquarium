using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerOutOfRangeForSec")]
    public class PlayerOutOfRangeForSec : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isPlayerOutOfRange = IsPlayerOutOfRange(controller);
            return isPlayerOutOfRange;
        }

        bool IsPlayerOutOfRange(StateController controller)
        {
            if(controller.IsPlayerInRange(controller.disappearRange))
                return false;
            else
                return true;
        }
    }
}
