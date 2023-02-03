using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] string path;
    void Start()
    {
        Read();
    }
    public void Read()
    {
        StreamReader stringReader = new StreamReader("Assets/StreamingAssets/DialogueData/" + path + ".csv");
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_string = stringReader.ReadLine();

            if (data_string == null)
            {
                endOfFile = true;
                break;
            }

            var data_values = data_string.Split(',');


            if (data_values[0] == "Id" || data_values[0] == "")
            {

            }
            else
            {
                //ID,character,charaterImage,dialogueText,choice1,choice2,choice1Text,choice2Text,type
                DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5], data_values[6], data_values[7], data_values[8]);
                //Debug.Log(newDialogue.character);
                DialogueManager.inst.openWith.Add(data_values[0], newDialogue);
            }
        }
    }

}
