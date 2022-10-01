using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Gizmos")]
    
    public Vector3 InteractOffset;
    [SerializeField] Color InteractColor;
    public Vector3 HitOffset;
    [SerializeField] Color HitColor;
    public Vector3 VisonOffset;
    [SerializeField] Color VisionColor;
    

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

   
    private void Update() 
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate() 
    {

    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
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
        // Gizmos.color = InteractColor;
        // Gizmos.DrawWireSphere(transform.position + InteractOffset,InteractRadius); //INTERACT RANGE
        
        // Gizmos.color = HitColor;
        // Gizmos.DrawRay(transform.position + HitOffset,moveDirection * hitRange); //HIT RANGE

        // Gizmos.color = VisionColor;
        // Gizmos.DrawRay(transform.position + VisonOffset,moveDirection * visionRange); //VISION RANGE
    }
}
