using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/DirectorAttackAction")]
    public class DirectorAttackAction : Action
    {
        public LayerMask playerLayer;
        
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        void Attack(StateController controller)
        {
            if(controller.IsPlayerInRange(controller.attackRange))
            {
                controller.ToggleAttack(true);
                AiDirectorController.inst.OnAttackPlayer();
                Debug.Log("Attack");
            }
        }
    }
}
