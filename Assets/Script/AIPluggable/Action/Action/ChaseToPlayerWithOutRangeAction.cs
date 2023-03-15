using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Action/ChaseToPlayerWithOutRangeAction")]
    public class ChaseToPlayerWithOutRangeAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        void Chase(StateController controller)
        {
            controller.ToggleChasing(true);
            // controller.ToggleAttack(false);

            Vector2 direction = controller.GetPlayerDirection();
            // float magnitude = direction.magnitude;
            // if (magnitude > 1)
            // {
            //     direction.Normalize();
            // }

            // float clampedMagnitude = Mathf.Clamp(magnitude, -1f, 1f);
            // direction = direction.normalized * clampedMagnitude;

            controller.transform.Translate(direction * controller.chasingSpeed * Time.deltaTime); 

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            controller.gameObject.transform.Find("Monster3").transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
