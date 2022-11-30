using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/ContinueChase")]
    public class ContinueChase : Decision
    {
        public LayerMask playerLayer;

        public override bool Decide(StateController controller)
        {
            bool isContinueChase = IsContinueChase(controller);
            return isContinueChase;
            
        }

        bool IsContinueChase(StateController controller)
        {
            if(controller.chasingTime > 0)
            {
                controller.ToggleChasing(true);
                controller.ToggleAttack(false);
                return true;
            }
            
            if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.chasingRange,playerLayer))
            {
                controller.ToggleChasing(true);
                controller.ToggleAttack(false);
                return true;
            }

            controller.ToggleChasing(false);
            controller.ToggleAttack(false);
            return false;
        }
    }
}
