using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public List<Action> actions;
    public List<Transition> transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Act(controller);
        }
    }

    void CheckTransition(StateController controller)
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
