using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/Waiting")]
    public class Waiting : Decision
    {
        public override bool Decide(StateController controller)
        {
            return CanSwitchState(controller);
        }

        bool CanSwitchState(StateController controller)
        {
            
            controller.waitingTime -= Time.deltaTime * 1f;
            if(controller.waitingTime < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
