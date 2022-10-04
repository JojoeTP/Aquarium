using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    void Start()
    {
        Read();
    }
    void Read()
    {
        // StreamReader stringReader = new StreamReader("D:\\University\\Year_4\\FinalProject\\Aquarium\\Assets\\Script\\DialogueSystem\\ReadingCSV\\DialogueData.csv");
        StreamReader stringReader = new StreamReader("Assets\\Script\\DialogueSystem\\ReadingCSV\\DialogueData.csv");
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = stringReader.ReadLine();
            if(data_string == null){
                endOfFile = true;
                break;
            }

            var data_values = data_string.Split(',');
            Debug.Log(data_values[0].ToString() + " " + data_values[1].ToString());
        }
    }

}
