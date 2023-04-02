using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveText : MonoBehaviour
{
    public static SaveText inst;
    [SerializeField] GameObject saveText;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        StartCoroutine(SetActiveFalseDelay(0f));
    }
    public void ShowSaveText()
    {
        StartCoroutine(SetActiveFalseDelay(3.0f));
    }
    void SetActiveTrue()
    {
        saveText.SetActive(true);
    }

    IEnumerator SetActiveFalseDelay(float time)
    {
        SetActiveTrue();
        yield return new WaitForSeconds(time);
        saveText.SetActive(false);
    }
}
