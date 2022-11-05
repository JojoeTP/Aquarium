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
                return true;
            
            if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.chasingRange,playerLayer))
                return true;

            return false;
        }
    }
}
