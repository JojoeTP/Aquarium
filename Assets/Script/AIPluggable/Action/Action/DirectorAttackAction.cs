using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/DirectorAttackAction")]
    public class DirectorAttackAction : Action
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
                AiDirectorController.inst.OnAttackPlayer();
                Debug.Log("Attack");
            }
        }
    }
}
