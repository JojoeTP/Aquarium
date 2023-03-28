using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/WallInVision")]
    public class WallInVision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool isWallInVision = IsWallInVision(controller);
            return isWallInVision;
        }

        bool IsWallInVision(StateController controller)
        {
            if(controller.IsWallInRange(controller.attackRange,controller.AttackOffset))
                return true;
            else
                return false;
        }
    }
}
