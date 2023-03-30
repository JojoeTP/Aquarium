using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject alertText;

    void Awake()
    {
        this.gameObject.SetActive(false);
    }

}
