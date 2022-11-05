using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    public float checkID = 0;
    public float id;
    string dataCheck;
    public List<string> character = new List<string>();
    public List<string> dialogue = new List<string>();
    public List<string> decision1 = new List<string>();
    public List<string> decision2 = new List<string>();

    [SerializeField] string path;
    void Awake()
    {
        Read();
    }
    public void Read()
    {
        StreamReader stringReader = new StreamReader("Assets/StreamingAssets/DialogueData/" + path + ".csv");
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = stringReader.ReadLine();

            if(data_string == null){
                endOfFile = true;
                break;
            }

            var data_values = data_string.Split(',');

            // Debug.Log(checkID);
            if(data_values[0] != "Id" && data_values[0] != id.ToString() ){

                if(checkID > id){
                    Debug.Log("1");
                    endOfFile = true;
                    break;
                }
                if(dataCheck != data_values[0]){
                    dataCheck = data_values[0];
                    checkID++;
                }
            }

            // Debug.Log(data_values[0]);
            if(data_values[0] == id.ToString()){
                Debug.Log(data_values[0].ToString() + " " + data_values[1].ToString() + " " + data_values[2].ToString());
                character.Add(data_values[1].ToString());
                dialogue.Add(data_values[2].ToString());
                decision1.Add(data_values[3].ToString());
                decision2.Add(data_values[4].ToString());
            }
        }
    }

}
