using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/ChaseToPlayerWithOutRangeAction")]
    public class ChaseToPlayerWithOutRangeAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        void Chase(StateController controller)
        {
            controller.ToggleChasing(true);
            controller.ToggleAttack(false);
            controller.transform.Translate(controller.GetPlayerDirection() * controller.chasingSpeed * Time.deltaTime); 
        }
    }
}
