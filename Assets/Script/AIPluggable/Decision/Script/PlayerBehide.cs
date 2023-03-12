using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerBehide")]
    public class PlayerBehide : Decision
    {
        public override bool Decide(StateController controller)
        {
            return IsPlayerPlayerBehide(controller);
        }

        bool IsPlayerPlayerBehide(StateController controller)
        {
            if(controller.IsPlayerBehide())
                return true;
            else
                return false;
        }
    }
}