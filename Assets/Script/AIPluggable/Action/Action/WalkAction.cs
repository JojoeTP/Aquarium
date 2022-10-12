using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "PluggableAI/Action/WalkAction")]
public class WalkAction : Action
{
    public override void Act(StateController controller)
    {
        Walk(controller);
    }

    void Walk(StateController controller)
    {
        controller.transform.Translate(controller.moveDirection * controller.moveSpeed * Time.deltaTime); 
    }
}