using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    Sprite character1;
	void Start () {
		character1 = Resources.Load<Sprite>("Boy");
 
		GameObject imageCharacter1 = GameObject.Find ("Character1");
		imageCharacter1.GetComponent<Image>().sprite = character1;
	}
}
