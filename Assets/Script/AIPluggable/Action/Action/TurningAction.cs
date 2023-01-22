using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu (menuName = "PluggableAI/Action/TurningAction")]
    public class TurningAction : Action
    {
        public override void Act(StateController controller)
        {
            Turning(controller);
        }

        void Turning(StateController controller)
        {
            controller.moveDirection *= -1f;
            controller.transform.localScale = new Vector3(controller.transform.localScale.x * -1f,controller.transform.localScale.y,controller.transform.localScale.z);
        }
    }
}
