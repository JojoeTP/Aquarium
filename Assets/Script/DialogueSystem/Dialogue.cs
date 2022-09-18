using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue" , menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string name;

    [TextArea(5,15)]
    public string[] sentences;
}
