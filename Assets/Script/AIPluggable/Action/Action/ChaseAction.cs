using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    void Patrol(StateController controller)
    {
        controller.transform.Translate(controller.moveDirection * controller.chaseSpeed * Time.deltaTime); 
    }
}
