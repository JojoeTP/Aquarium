using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PluggableAI;

public class StateController : MonoBehaviour
{
    [Header("Debugger")]
    
    [SerializeField] Vector3 interactOffset;
    [SerializeField] Color InteractColor;
    [SerializeField] Vector3 AttackOffset;
    [SerializeField] Color AttackColor;
    [SerializeField] Vector3 visonOffset;
    [SerializeField] Color visionColor;
    [SerializeField] Vector3 chasingRangeOffset;
    [SerializeField] Color ChaseColor;
    [SerializeField] Color disappearColor;
    [SerializeField] Vector3 disappearRangeOffset;
    public Vector3 stateLableOffset;

    [Header("STATUS")]
    //Speed
    public float moveSpeed = 4f;
    public float chasingSpeed = 6f;
    [Header("-------")]
    //Sensation
    public float visionRange = 10f;
    public float interactRange = 1f;
    [Header("-------")]
    //Attack
    public float attackRange = 0f;
    [Header("-------")]
    //Chase
    public float chasingRange = 0f;
    public float chasingTime = 0f;
    float elapsedChasingTime;
    float elapsedTimeBeforeDie = 0;
    [Header("-------")]
    public float disappearRange = 0f;
    
    [Header("Variable")]
    public Vector3 moveDirection = Vector3.right;
    public State currentState;
    public State remainState;
    float timeBeforeSwitchState;
    public Animator animator;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;

    [Header("Door")]
    public float timeBeforEnterSameDoorAgain;
    float elapsedTimeEnterDoor;
    public DoorSystem enteredDoor;

    public float ElapsedchasingTime {get {return elapsedChasingTime;}}
    public float ElapsedTimeBeforeDie {get {return elapsedTimeBeforeDie;} set {elapsedTimeBeforeDie = value;}}
    public float TimeBeforeSwitchState {get {return timeBeforeSwitchState;}}

    private void Update() 
    {
        
    }

    private void FixedUpdate() 
    {
        timeBeforeSwitchState -= Time.deltaTime;
        elapsedTimeEnterDoor -= Time.deltaTime;
        elapsedChasingTime -= Time.deltaTime;

        if(elapsedTimeEnterDoor <= 0)
            ResetEnteredDoor();

        currentState.FixedUpdateState(this);
    }

    public void ResetEnteredDoor()
    {
        elapsedTimeEnterDoor = timeBeforEnterSameDoorAgain;
        enteredDoor = null;
    }

    public void ResetChasingTime()
    {
        elapsedChasingTime = chasingTime;
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            timeBeforeSwitchState = nextState.timeBeforeSwitchState;
            currentState = nextState;
            OnEnterState(nextState);
        }
    }

    void OnEnterState(State nextState)
    {
        nextState.InitState();
    }

    public bool IsPlayerInRange(float range)
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.HIDING)
            if(Physics2D.Raycast(transform.position,moveDirection,range,playerLayer))
                return true;

        return false;
    }

    public bool IsPlayerInRangeCircle(float range)
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.HIDING)
            if(Physics2D.OverlapCircle(transform.position,range,playerLayer))
                return true;

        return false;
    }

    public bool IsPlayerInRangeIncludeBehide(float range)
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.HIDING)
            if(Physics2D.OverlapCircle(transform.position,range,playerLayer))
                return true;

        return false;
    }

    public bool IsWallInRange(float range)
    {
        if(Physics2D.Raycast(transform.position,moveDirection,range,wallLayer))
            return true;

        return false;
    }

    public Vector2 GetPlayerDirection()
    {
        Vector2 direction = PlayerManager.inst.transform.position - transform.position;
        direction += new Vector2(0,4.5f);
        return direction.normalized;
    }

    public bool IsPlayerBehide()
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.HIDING)
        {
            if (transform.position.x > PlayerManager.inst.transform.position.x && transform.localScale.x > 0)
                return true;
            
            if (transform.position.x < PlayerManager.inst.transform.position.x && transform.localScale.x < 0)
                return true;
        }

        return false;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = InteractColor;
        Gizmos.DrawWireSphere(transform.position + interactOffset,interactRange); //INTERACT RANGE
        
        Gizmos.color = AttackColor;
        Gizmos.DrawWireSphere(transform.position + AttackOffset,attackRange); //HIT RANGE

        Gizmos.color = visionColor;
        Gizmos.DrawWireSphere(transform.position + visonOffset,visionRange); //VISION RANGE

        Gizmos.color = ChaseColor;
        Gizmos.DrawWireSphere(transform.position + chasingRangeOffset,chasingRange); //VISION RANGE

        Gizmos.color = disappearColor;
        Gizmos.DrawWireSphere(transform.position + disappearRangeOffset,disappearRange); //disappear RANGE
    }

    public void ToggleChasing(bool enabled)
    {
        if(animator == null)
            return;

        animator.SetBool("M_Trigger",enabled);
    }
    public void ToggleAttack(bool enabled)
    {
        if(animator == null)
            return;

        // animator.SetBool("M_Attack",enabled);
        animator.SetTrigger("M_Attack_Trigger");
    }
    public void ToggleTimeOut(bool enabled)
    {
        if(animator == null)
            return;

        animator.SetBool("M_Timeout",enabled);
    }
}
