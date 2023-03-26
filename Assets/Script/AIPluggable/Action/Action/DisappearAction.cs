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
            if(AiJunitorController.inst.junitorController != null)
            {
                AiJunitorController.inst.DestroyJunitorAI();
                // UITransition.inst.JunitorTransitionIn();
            }

            if(AiRedHoodController.inst.redHoodController != null)
            {
                AiRedHoodController.inst.DestroyRedHoodAI();
                // UITransition.inst.RedHoodTransitionIn();
            }

            if(AiDirectorController.inst.directorController != null)
                UITransition.inst.DirectorTransitionIn();
        }
    }
}