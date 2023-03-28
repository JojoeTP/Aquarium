using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/ContinueChase")]
    public class ContinueChase : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool isContinueChase = IsContinueChase(controller);
            return isContinueChase;
            
        }

        bool IsContinueChase(StateController controller)
        {
            // controller.ToggleAttack(false);

            if(controller.ElapsedchasingTime > 0)
            {
                ContinueChasing(controller);
                return true;
            }

            if(controller.IsPlayerInRange(controller.chasingRange,controller.chasingRangeOffset))
            {
                ContinueChasing(controller);
                controller.ResetChasingTime();
                return true;
            }

            controller.ToggleChasing(false);
            return false;
        }

        void ContinueChasing(StateController controller)
        {
            controller.ToggleChasing(true);
        }
    }
}
