using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour {

    public static string levelName;
	public string level;
	

	public void ContinueGame() 
	{	
		levelName = PlayerPrefs.GetString("lastLevel");
        SceneManager.LoadScene("Loading");
		
	}
	
	
	public void LoadScene (string level) 
	{
		levelName = level;
		SceneManager.LoadScene("Loading");
	}
	
	public void MainMenu () 
	{
		Time.timeScale = 1.0f; 
		PlayerPrefs.SetString("lastLevel", levelName);
		PlayerPrefs.Save();
		SceneManager.LoadScene("MainMenu");
	}
	
	
	public void ApplicationExitSave ()
	{	
		PlayerPrefs.SetString("lastLevel", levelName);
		PlayerPrefs.Save();
		Application.Quit();
	}
	
		
		public void ApplicationExit ()
	{	
		Application.Quit();
	}
	

}