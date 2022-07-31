using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    bool canHide;
    Transform hidePosition;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
    }

    bool hideToggle = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHide == true && hideToggle == false){
            rend.sortingOrder = -1;
            // GetComponent<PlayerMovement>().enabled = false;
            hideToggle = true;
        }else if (Input.GetKeyDown(KeyCode.E) && canHide == true){
            rend.sortingOrder = 1;
            // GetComponent<PlayerMovement>().enabled = true;
            hideToggle = false;
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
