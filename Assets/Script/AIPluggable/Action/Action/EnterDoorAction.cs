using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/EnterDoor")]
public class EnterDoorAction : Action
{
    public override void Act(StateController controller)
    {
        EnterDoor(controller);
    }

    void EnterDoor(StateController controller)
    {

    }
}
