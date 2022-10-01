using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "PluggableAI/Action/WalkAction")]
public class WalkAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    void Patrol(StateController controller)
    {
        controller.transform.Translate(controller.moveDirection * controller.moveSpeed * Time.deltaTime); 
    }
}