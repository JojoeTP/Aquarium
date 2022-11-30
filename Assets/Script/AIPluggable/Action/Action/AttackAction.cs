using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/AttackAction")]
    public class AttackAction : Action
    {
        public LayerMask playerLayer;
        
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        void Attack(StateController controller)
        {
            if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.attackRange,playerLayer))
            {
                controller.ToggleAttack(true);
                Debug.Log("Attack");
                Attack();
            }
        }

        void Attack()
        {
            Debug.Log("Hit");
        }
    }
}
