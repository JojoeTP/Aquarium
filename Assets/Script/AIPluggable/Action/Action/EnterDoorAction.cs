using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/EnterDoor")]
    public class EnterDoorAction : Action
    {
        
        public override void Act(StateController controller)
        {
            EnterDoor(controller);
        }

        void EnterDoor(StateController controller)
        {
            var overlapObj = Physics2D.OverlapCircleAll(controller.transform.position,controller.interactRange);

            foreach(var n in overlapObj)
            {
                if(n.TryGetComponent<DoorSystem>(out DoorSystem door))
                {
                    if(door == controller.enteredDoor)
                        break;
                
                    controller.ResetEnteredDoor();
                    controller.enteredDoor = door.connectDoor.GetComponent<DoorSystem>();

                    door.EnemyEnterDoor(controller.transform);
                    break;
                }
            }
        }
    }
}
