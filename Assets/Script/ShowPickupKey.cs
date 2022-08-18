using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPickupKey : MonoBehaviour
{
    public GameObject keyImage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            keyImage.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
            keyImage.SetActive(false);
    }
}
