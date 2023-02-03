using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/DoorInInteractArea")]
    public class DoorInInteractArea : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool isDoorInInteractArea = IsDoorInInteractArea(controller);
            return isDoorInInteractArea;
            
        }

        bool IsDoorInInteractArea(StateController controller)
        {
            //add condition about chase later 
            var overlapObj = Physics2D.OverlapCircleAll(controller.transform.position,controller.interactRange);

            foreach(var n in overlapObj)
            {
                if(n.TryGetComponent<DoorSystem>(out DoorSystem door))
                {
                    if(door == controller.enteredDoor)
                        return false;
                        
                    if(door.notForEnemy)
                        return false;

                    if(controller.IsPlayerInRange(controller.chasingRange))
                        return false;

                    return true;
                }
            }

            return false;
        }
    }
}
