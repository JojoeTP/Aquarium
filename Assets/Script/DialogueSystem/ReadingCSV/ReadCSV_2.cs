using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCSV_2 : MonoBehaviour
{
    public string id;
    public List<string> characterName = new List<string>();
    public List<string> characterImage = new List<string>();
    public List<string> dialogue = new List<string>();
    //public List<string> decision1 = new List<string>();
    //public List<string> decision2 = new List<string>();

    [SerializeField] string path;
    void Awake()
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

            // Debug.Log(data_values[0]);
            if (data_values[0] == id)
            {
                //Debug.Log(data_values[0].ToString() + " " + data_values[1].ToString() + " " + data_values[2].ToString());
                //characterName.Add(data_values[1].ToString());
                //characterImage.Add(data_values[2].ToString());
                //dialogue.Add(data_values[3].ToString());
                //decision1.Add(data_values[3].ToString());
                //decision2.Add(data_values[4].ToString());

                DialogueInfo newDialogue = new DialogueInfo(id, data_values[1], data_values[2],data_values[3]);
                DialogueManager_2.inst.openWith.Add(id,newDialogue);
            }
        }
    }
}
