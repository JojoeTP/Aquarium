using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleState
{
    STOP,
    PLAYING,
    FINSIH,
}

public class DoorEncryption : MonoBehaviour
{
    [SerializeField] PuzzleState currentstate = PuzzleState.STOP;

    // [SerializeField]  float countTime;
    // float currentTime;
    [SerializeField] DoorSystem doorTrigger;
    [SerializeField] Transform startPostion;
    [SerializeField] Transform finishPostion;

    [Header("Blood Line")]
    [SerializeField] GameObject leftBlood;
    [SerializeField] GameObject upperBlood;
    [SerializeField] GameObject rightBlood;

    [Header("Status")]
    [SerializeField] string baseEncryptionCode;
    string encryptionCode;
    int currentCodeIndex = 0;
    int revertIndex = 0;
    int correctCount = 0;
    int failCount = 0;
    
    [Header("InvertCodeEvent")]
    [SerializeField] SpriteRenderer labyrinthBG;
    [SerializeField] Sprite[] labyrinthSprite;
    // bool isAlreadyInvertCode; 

    [Header("Option")]
    [SerializeField] DoorSystem leftDoor;
    [SerializeField] DoorSystem upDoor;
    [SerializeField] DoorSystem rightDoor;
    
    void Start()
    {
        ChangeState(PuzzleState.STOP);
    }

    void FixedUpdate()
    {
        // Timer();
    }

    void SettingStartPuzzle()
    {
        doorTrigger.triggerConditionEvent.RemoveAllListeners();
        doorTrigger.triggerConditionEvent.AddListener( () => {
            //setlocation before start puzzle
            doorTrigger.connectDoor = startPostion;

            StartPuzzle();
        }); 
    }

    void StartPuzzle()
    {
        ChangeState(PuzzleState.PLAYING);
    }

    void ClearPuzzle()
    {
        ChangeState(PuzzleState.FINSIH);
    }

    void ChangeState(PuzzleState state)
    {
        currentstate = state;

        switch(currentstate)
        {
            case PuzzleState.STOP :
                SettingStartPuzzle();
                SettingDoor();
                break;
            case PuzzleState.PLAYING :
                // currentTime = countTime;
                currentCodeIndex = 0;
                correctCount = 0;
                failCount = 0;
                encryptionCode = baseEncryptionCode;
                labyrinthBG.sprite = labyrinthSprite[0];
                // isAlreadyInvertCode = false;
                ToggleBloodLine();
                break;
            case PuzzleState.FINSIH :
                RemoveAllListeners();
                break;
        }
    }

    void Timer()
    {   
        switch(currentstate)
        {
            case PuzzleState.STOP :
                break;
            case PuzzleState.PLAYING :
                // if(currentTime > 0)
                //     currentTime -= Time.deltaTime;

                // if(currentTime <= 0)
                // {
                //     OnTimeOut();
                // }

                break;
            case PuzzleState.FINSIH :
                break;
        }
    }

    void EnterEncryptionDoor(char c)
    {
        if(encryptionCode[currentCodeIndex] == c)
        {
            correctCount++;
            currentCodeIndex++;

            if(currentCodeIndex < baseEncryptionCode.Length)
                ToggleBloodLine();
        }
        else
        {
            failCount++;
            labyrinthBG.sprite = labyrinthSprite[failCount];
        }
    }

    void SettingDoor()
    {
        leftDoor.triggerConditionEvent.RemoveAllListeners();
        upDoor.triggerConditionEvent.RemoveAllListeners();
        rightDoor.triggerConditionEvent.RemoveAllListeners();

        leftDoor.triggerConditionEvent.AddListener( () => {
            EnterEncryptionDoor('L');
            OnSelectDoor(leftDoor);
        }); 

        upDoor.triggerConditionEvent.AddListener( () => {
            EnterEncryptionDoor('U');
            OnSelectDoor(upDoor);
        }); 

        //downDoor.triggerConditionEvent.AddListener( () => {
        //    EnterEncryptionDoor('D');
        //    OnSelectDoor(downDoor);
        //}); 

        rightDoor.triggerConditionEvent.AddListener( () => {
            EnterEncryptionDoor('R');
            OnSelectDoor(rightDoor);
        }); 
    }

    void OnSelectDoor(DoorSystem door)
    {
        if(encryptionCode.Length == currentCodeIndex)
        {
            OnFinish(door);
            return;
        }

        if(failCount >= 3)
        {
            OnFail(door);
            return;
        }

        door.connectDoor = startPostion;

        if(currentCodeIndex > revertIndex)
            labyrinthBG.sprite = labyrinthSprite[failCount];

        // if(!isAlreadyInvertCode)
        RandomInvertCode();
            
    }

    void OnFinish(DoorSystem door)
    {
        door.connectDoor = finishPostion;
        doorTrigger.connectDoor = finishPostion;

        door.triggerConditionEvent.AddListener( () => {
            OnFinishEvent();
        }); 

        ChangeState(PuzzleState.FINSIH);
    }

    void OnFinishEvent()
    {
        //Add event when finish encryption
    }

    void OnFail(DoorSystem door)
    {
        RemoveAllListeners();

        //warp to start Pos
        door.connectDoor = doorTrigger.transform;
        door.PlayerEnterDoor(PlayerManager.inst.transform);

        ChangeState(PuzzleState.STOP);
    }

    // void OnTimeOut()
    // {
    //     RemoveAllListeners();

    //     //warp to start Pos
    //     doorTrigger.connectDoor = doorTrigger.transform;
    //     doorTrigger.EnterDoor(PlayerManager.inst.transform);

    //     ChangeState(PuzzleState.STOP);
    // }

    void RemoveAllListeners()
    {
        doorTrigger.triggerConditionEvent.RemoveAllListeners();

        leftDoor.triggerConditionEvent.RemoveAllListeners();
        upDoor.triggerConditionEvent.RemoveAllListeners();
        //downDoor.triggerConditionEvent.RemoveAllListeners();
        rightDoor.triggerConditionEvent.RemoveAllListeners();
    }

    void RandomInvertCode()
    {
        if(Random.Range(0,100) > 50)
        {
            // isAlreadyInvertCode = true;

            char? invertChar = null;
            string newCode = "";
            revertIndex = currentCodeIndex;

            // if(encryptionCode[currentCodeIndex] == 'U')
            //     revertIndex += 1;

            switch(encryptionCode[revertIndex])
            {
                case 'L' :
                    invertChar = 'R';
                    break;
                case 'U' :
                    invertChar = 'U';
                    break;
                //case 'D' :
                //    invertChar = 'U';
                //    break;
                case 'R' :
                    invertChar = 'L';
                    break;
            }    

            for(int i = 0; i < encryptionCode.Length; i++)
            {
                if(i == revertIndex)
                {
                    newCode = newCode + invertChar;
                }
                else
                    newCode = newCode + encryptionCode[i];
            }

            encryptionCode = newCode;
            
            labyrinthBG.sprite = labyrinthSprite[3];
        }
    }

    void ToggleBloodLine()
    {
        char cuurentCode = baseEncryptionCode[currentCodeIndex];
        switch(cuurentCode)
        {
            case 'L':
                leftBlood.SetActive(true);
                upperBlood.SetActive(false);
                rightBlood.SetActive(false);
                break;
            case 'U':
                leftBlood.SetActive(false);
                upperBlood.SetActive(true);
                rightBlood.SetActive(false);
                break;
            case 'R':
                leftBlood.SetActive(false);
                upperBlood.SetActive(false);
                rightBlood.SetActive(true);
                break;
            
        }
    }
}
