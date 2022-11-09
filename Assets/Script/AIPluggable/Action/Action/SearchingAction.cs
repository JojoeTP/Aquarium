using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu (menuName = "PluggableAI/Action/SearchingAction")]
    public class SearchingAction : Action
    {
        public override void Act(StateController controller)
        {
            Searching(controller);
        }

        void Searching(StateController controller)
        {
            // หันซ้ายหันขวา
            Debug.Log("Searching");
        }
    }
}