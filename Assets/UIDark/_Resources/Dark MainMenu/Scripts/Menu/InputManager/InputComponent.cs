using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputComponent : MonoBehaviour {

	[SerializeField] private Text _buttonText; // Button Text
	[SerializeField] private string _defaultKeyName; // Key/Name
	[SerializeField] private KeyCode _defaultKeyCode; // Key Code

	public KeyCode keyCode { get; set; }

	private IEnumerator coroutine;
	private string tmpKey;

	public Text buttonText
	{
		get{ return _buttonText; }
	}

	public string defaultKeyName
	{
		get{ return _defaultKeyName; }
	}
	
	public KeyCode defaultKeyCode
	{
		get{ return _defaultKeyCode; }
	}

	public void ButtonSetKey() // Transition to the Waiting mode
	{
		tmpKey = _buttonText.text;
		_buttonText.text = "Press Key";
		coroutine = Wait();
		StartCoroutine(coroutine);
	}

    // Waiting You Button
	// 'Escape' - cancel
	IEnumerator Wait()
	{
		while(true)
		{
			yield return null;

			if(Input.GetKeyDown(KeyCode.Escape))
			{
				_buttonText.text = tmpKey;
				StopCoroutine(coroutine);
			}

			foreach(KeyCode k in KeyCode.GetValues(typeof(KeyCode)))
			{
				if(Input.GetKeyDown(k) && !Input.GetKeyDown(KeyCode.Escape))
				{
					keyCode = k;
					_buttonText.text = k.ToString();
					StopCoroutine(coroutine);
				}
			}
		}
	}
}
