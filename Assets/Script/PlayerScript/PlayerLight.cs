using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public GameObject playerLight;
    bool isLight = false;

    void Update()
    {
        PlayerLightInput();
    }

    void PlayerLightInput(){
        if (Input.GetKeyDown(KeyCode.F) && isLight == false){
            playerLight.SetActive(false);
            isLight = true;
        }
        else if(Input.GetKeyDown(KeyCode.F) && isLight == true){
            playerLight.SetActive(true);
            isLight = false;

        }
    }
}
