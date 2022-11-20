using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;

namespace PluggableAI
{
    [CreateAssetMenu (menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public List<Action> oneTimeActions;
        public List<Action> updateActions;
        public List<Transition> transitions;
        public float waitingTime;

        bool isActionDone = false;

        public void FixedUpdateState(StateController controller)
        {
            DoActions(controller);
            CheckTransition(controller);
        }

        void DoActions(StateController controller)
        {
            for (int i = 0; i < updateActions.Count; i++)
            {
                updateActions[i].Act(controller);
            }

            if(oneTimeActions.Count == 0)
            {
                isActionDone = true;
            }
        }

        public void DoActionsOneTime(StateController controller)
        {
            for (int i = 0; i < oneTimeActions.Count; i++)
            {
                oneTimeActions[i].Act(controller);
            }

            isActionDone = true;
        }

        void CheckTransition(StateController controller)
        {
            if(!isActionDone)
                return;
                
            for (int i = 0; i < transitions.Count; i++)
            {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);

                if(decisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }
}
