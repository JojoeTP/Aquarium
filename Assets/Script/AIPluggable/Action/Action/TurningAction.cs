using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Action/TurningAction")]
public class TurningAction : Action
{
    public override void Act(StateController controller)
    {
        Turning(controller);
    }

    void Turning(StateController controller)
    {
        controller.Turning();
    }
}
