using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}

