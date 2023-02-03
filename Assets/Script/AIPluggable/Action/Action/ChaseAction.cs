using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/ChaseAction")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        void Chase(StateController controller)
        {
            controller.ToggleChasing(true);
            controller.ToggleAttack(false);
            controller.transform.Translate(controller.moveDirection * controller.chasingSpeed * Time.deltaTime); 
        }
    }
}
