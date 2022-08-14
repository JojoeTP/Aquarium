using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public float normalSpeed = 4f;
    public float chaseSpeed = 6f;
    public float visionRange = 10f;
    public float hitRange = 2f;
    public Vector3 moveDirection = Vector3.right;
    public State currentState;
    public State remainState;
    public float countDownAttack = 0f;

    [Header("Attack Stat")]
    public float attackDelay = 3f;
    public float attackTimeElapsed = 0;
    public bool isAttack = false;
    
    private void Update() 
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate() 
    {
        CountAttackTimeElapsed();
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState && !isAttack)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    void CountAttackTimeElapsed()
    {
        if(isAttack)
            attackTimeElapsed += Time.deltaTime;
    }

    public bool CheckAttackDelay()
    {
        countDownAttack += Time.deltaTime;
        return countDownAttack >= attackDelay;
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        return attackTimeElapsed >= duration;
    }

    void SetDefaultValue()
    {
        attackTimeElapsed = 0f;
        countDownAttack = 0f;
    }

    void OnExitState()
    {
        SetDefaultValue();
        isAttack = false;
    }

    public void Turning()
    {
        moveDirection *= -1f;
        transform.localScale = new Vector3(transform.localScale.x * -1f,transform.localScale.y,transform.localScale.z);
    }
}
