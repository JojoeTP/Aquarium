using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/RedHoodAttackAction")]
    public class RedHoodAttackAction : Action
    {
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        void Attack(StateController controller)
        {
            if(controller.IsPlayerInRange(controller.attackRange,controller.AttackOffset))
            {
                controller.ToggleAttack(true);
                AiRedHoodController.inst.OnAttackPlayer();
                PlayerManager.inst.PlayerCam.ShakeCamera();
            }
        }
    }
}
