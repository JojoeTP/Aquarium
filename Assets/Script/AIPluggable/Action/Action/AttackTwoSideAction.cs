using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/AttackTwoSideAction")]
    public class AttackTwoSideAction : Action
    {
        public LayerMask playerLayer;
        
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        void Attack(StateController controller)
        {
            if(controller.IsPlayerInRangeCircle(controller.attackRange))
            {
                controller.ToggleAttack(true);
                AiMermaidController.inst.OnAttackPlayer();
                Debug.Log("Attack");
                PlayerManager.inst.PlayerCam.ShakeCamera();
            }
        }
    }
}
