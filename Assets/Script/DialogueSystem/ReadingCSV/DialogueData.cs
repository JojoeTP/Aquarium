using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData 
{
    public int id;
    public string dialogue;

    public DialogueData(DialogueData data){
        id = data.id;
        dialogue = data.dialogue;
    }
}
