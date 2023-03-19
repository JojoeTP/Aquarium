using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerBehideAndMove")]
    public class PlayerBehideAndMove : Decision
    {
        public override bool Decide(StateController controller)
        {
            return IsPlayerPlayerBehide(controller);
        }

        bool IsPlayerPlayerBehide(StateController controller)
        {
            if(controller.IsPlayerInRange(-controller.visionRange) && PlayerManager.inst.PlayerMovement.IsMove())
                return true;
            else
                return false;
        }
    }
}