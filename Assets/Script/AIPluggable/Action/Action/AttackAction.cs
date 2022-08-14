using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/Attack")]
public class AttackAction : Action
{
    public float attackTimeElapsed = 2;
    public LayerMask playerLayer;
    
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    void Attack(StateController controller)
    {

        if(Physics2D.Raycast(controller.transform.position,controller.moveDirection,controller.hitRange,playerLayer))
        {
            controller.isAttack = true;
        }

        if(controller.isAttack)
        {
            if(controller.CheckAttackDelay())
            {
                controller.countDownAttack = 0f;
                Attack();
            }

            if(controller.CheckIfCountDownElapsed(attackTimeElapsed))
            {
                controller.isAttack = false;
                controller.attackTimeElapsed = 0;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Hit");
    }
}

