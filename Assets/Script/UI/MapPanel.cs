using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanel : MonoBehaviour
{
    public Canvas Canvas { get { return canvas; } }
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
