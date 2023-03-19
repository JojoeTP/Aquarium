using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerDonotMoveInTime")]
    public class PlayerDonotMoveInTime : Decision
    {
        float time = 10f;

        bool isPlayerNotMove;
        public override bool Decide(StateController controller)
        {
            return PlayerNotMove(controller);
        }

        bool PlayerNotMove(StateController controller)
        {
            if(!PlayerManager.inst.PlayerMovement.IsMove()) 
            {
                controller.ElapsedTimeBeforeDie += Time.deltaTime;

                if(controller.ElapsedTimeBeforeDie >= time)
                    return true;
            }
            else
            {
                controller.ElapsedTimeBeforeDie = 0;
                return false;
            }
                
            return false;   
        }
    }
}