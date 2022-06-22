using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        // transform.Translate
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        direction = direction.normalized * playerSpeed * Time.deltaTime;

        transform.position += direction;
    }
}
