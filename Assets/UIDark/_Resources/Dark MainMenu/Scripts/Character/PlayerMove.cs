 using System;
 using UnityEngine;

public class PlayerMove : MonoBehaviour {
	 
private float X;
private float Y;
 
public float Sensitivity;
public float speed = 20f;



	
void Update () 
    {
	if(GameInput.Key.GetKey("Forward"))
	{
	 transform.localPosition += transform.forward * speed * Time.deltaTime;
	} 
	
	if(GameInput.Key.GetKey("Back")) 
	{
	 transform.localPosition -= transform.forward * speed * Time.deltaTime;
	}

	if(GameInput.Key.GetKey("Left"))
	{
	 transform.localPosition -= transform.right * speed * Time.deltaTime;
	} 
	
	if(GameInput.Key.GetKey("Right")) 		
	{
	transform.localPosition += transform.right * speed * Time.deltaTime;
	}
 }
}