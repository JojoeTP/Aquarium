using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu (menuName = "PluggableAI/Action/DisappearAction")]
    public class DisappearAction : Action
    {
        public override void Act(StateController controller)
        {
            Disappear(controller);
        }

        void Disappear(StateController controller)
        {
            UITransition.inst.DirectorTransitionIn();
        }
    }
}