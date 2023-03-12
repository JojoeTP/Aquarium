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
            if(Vector3.Distance(controller.transform.position,PlayerManager.inst.transform.position) <= controller.attackRange)
            {
                controller.ToggleAttack(true);
                Debug.Log("Attack");
            }

        }
    }
}
