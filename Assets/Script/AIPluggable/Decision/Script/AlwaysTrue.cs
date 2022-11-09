using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decesion/AlwaysTrue")]
    public class AlwaysTrue : Decision
    {
        public override bool Decide(StateController controller)
        {
            return true;
        }
    }
}

