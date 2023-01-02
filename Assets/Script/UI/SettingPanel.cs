using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    public Canvas Canvas {get {return canvas;}}
    Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();        
    }
    
    void Start()
    {

    }

    void Update()
    {
        
    }
}
