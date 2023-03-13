using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu (menuName = "PluggableAI/Action/WalkAction")]
    public class WalkAction : Action
    {
        public override void Act(StateController controller)
        {
            Walk(controller);
        }

        void Walk(StateController controller)
        {
            controller.ToggleTimeOut(true);
            controller.transform.Translate(controller.moveDirection * controller.moveSpeed * Time.deltaTime); 
        }
    }
}