using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Image tutorialImage;
    [SerializeField] Button closeButton;
    private void Start()
    {
        AddListenerToButton();
        tutorialImage.gameObject.SetActive(false);
    }
    public void AddListenerToButton()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => { TutorialTransition.inst.TutorialFadeOut(); });
    }

    public void SetTutorialSprite(Sprite sprite)
    {
        tutorialImage.sprite = sprite;
    }
    public void ShowTutorialImage()
    {
        tutorialImage.gameObject.SetActive(true);
        closeButton.interactable = true;
    }

    public void CloseTutorialImage()
    {
        tutorialImage.gameObject.SetActive(false);
        closeButton.interactable = false;
    }
}
