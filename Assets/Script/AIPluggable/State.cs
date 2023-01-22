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
        public List<Action> LoopActions;
        public List<Transition> transitions;
        public float timeBeforeSwitchState;

        bool isActionDone = false;

        public void InitState()
        {
            isActionDone = false;
        }

        public void FixedUpdateState(StateController controller)
        {
            if(!isActionDone)
                DoActionsOneTime(controller);
            
            RunActions(controller);
            CheckTransition(controller);
        }

        void RunActions(StateController controller)
        {
            for (int i = 0; i < LoopActions.Count; i++)
            {
                LoopActions[i].Act(controller);
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
            // if(!isActionDone)
            //     return;
                
            // for (int i = 0; i < transitions.Count; i++)
            // {
            //     bool decisionSucceeded = transitions[i].decision.Decide(controller);

            //     if(decisionSucceeded)
            //     {
            //         controller.TransitionToState(transitions[i].trueState);
            //     }
            //     else
            //     {
            //         controller.TransitionToState(transitions[i].falseState);
            //     }
            // }

            if(controller.TimeBeforeSwitchState > 0)
                return;

            if(oneTimeActions != null && !isActionDone)
                return;

            SwitchState(controller);
        }

        void SwitchState(StateController controller)
        {
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
