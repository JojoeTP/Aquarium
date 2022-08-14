using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    bool canHide;
    Transform hidePosition;

    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
    }

    public bool hide = false;

    void Update()
    {
        PlayerHideInput();
    }

    void PlayerHideInput(){
        if (Input.GetKeyDown(KeyCode.E) && canHide == true && hide == false){
            rend.sortingOrder = -1;
            hide = true;
        }else if (Input.GetKeyDown(KeyCode.E) && canHide == true){
            rend.sortingOrder = 1;
            hide = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("HidingPlace")){
            canHide = true;
            // hidePosition = new Vector3(other.transform.position,other.transform.rotation,0) ;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("HidingPlace")){
            canHide = false;
        }
    }
}
