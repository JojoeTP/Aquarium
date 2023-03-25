using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/PlayerOutOfRangeForSec")]
    public class PlayerOutOfRangeForSec : Decision
    {
        [SerializeField] float time = 10f;

        public override bool Decide(StateController controller)
        {
            bool isPlayerOutOfRange = IsPlayerOutOfRange(controller);
            return isPlayerOutOfRange;
        }

        bool IsPlayerOutOfRange(StateController controller)
        {
            if(controller.IsPlayerInRangeIncludeBehide(controller.disappearRange))
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
