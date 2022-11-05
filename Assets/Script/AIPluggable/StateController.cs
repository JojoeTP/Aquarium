using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PluggableAI;

public class StateController : MonoBehaviour
{
    [Header("Debugger")]
    
    public Vector3 InteractOffset;
    [SerializeField] Color InteractColor;
    public Vector3 AttackOffset;
    [SerializeField] Color AttackColor;
    public Vector3 VisonOffset;
    [SerializeField] Color VisionColor;
    public Vector3 ChasingRangeOffset;
    [SerializeField] Color ChaseColor;
    public Vector3 StateLableOffset;
    

    [Header("STATUS")]
    //Speed
    public float moveSpeed = 4f;
    public float chasingSpeed = 6f;
    [Header("-------")]
    //Sensation
    public float frontVisionRange = 10f;
    public float backVisionRange = 2f;
    public float interactRange = 1f;
    [Header("-------")]
    //Attack
    public float attackRange = 0f;
    [Header("-------")]
    //Chase
    public float chasingRange = 0f;
    public float chasingTime = 0f;
    
    [Header("Variable")]
    public Vector3 moveDirection = Vector3.right;
    public State currentState;
    public State remainState;
    public float waitingTime;

   
    private void Update() 
    {
        
    }

    private void FixedUpdate() 
    {
        // currentState.FixedUpdateState(this);
        currentState.FixedUpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            waitingTime = nextState.waitingTime;
            currentState = nextState;
            nextState.DoActionsOneTime(this);
            OnExitState();
        }
    }


    void OnExitState()
    {
        
    }

    public void Turning()
    {
        moveDirection *= -1f;
        transform.localScale = new Vector3(transform.localScale.x * -1f,transform.localScale.y,transform.localScale.z);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = InteractColor;
        Gizmos.DrawWireSphere(transform.position + InteractOffset,interactRange); //INTERACT RANGE
        
        Gizmos.color = AttackColor;
        Gizmos.DrawRay(transform.position + AttackOffset,moveDirection * attackRange); //HIT RANGE

        Gizmos.color = VisionColor;
        Gizmos.DrawRay(transform.position + VisonOffset,moveDirection * frontVisionRange); //VISION RANGE
        Gizmos.DrawRay(transform.position + VisonOffset,-moveDirection * backVisionRange); //VISION RANGE

        Gizmos.color = ChaseColor;
        Gizmos.DrawRay(transform.position + ChasingRangeOffset,moveDirection * chasingRange); //VISION RANGE

        Handles.Label(transform.position + StateLableOffset,currentState.name);
    }
}
