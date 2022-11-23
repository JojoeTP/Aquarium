using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseMenu : MonoBehaviour {

    public GameObject pauseMenuUi;

	public static bool IsPaused = false;

    

    public void Start()
    { 

	}
	
    public void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape)) 
        {		
		  if (IsPaused = !IsPaused)
		  {
			Time.timeScale = 0.0f;
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
			IsPaused = false;
			pauseMenuUi.SetActive(true);
			
			
		  }

        }
	}
    


	//--------------------------------------------------------------//

	// Update is called once per frame
	public void ResumeGame () 
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		IsPaused = false;
		pauseMenuUi.SetActive(false);
	
	}

  }

