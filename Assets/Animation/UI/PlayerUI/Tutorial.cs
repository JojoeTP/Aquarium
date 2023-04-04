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
    }
    public void AddListenerToButton()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => { CloseTutorialImage(); });
    }

    public void ShowTutorialImage(Sprite sprite)
    {
        tutorialImage.sprite = sprite;
        PlayerUI.inst.TutorialFadeIn();
        closeButton.interactable = true;
    }


    void CloseTutorialImage()
    {
        PlayerUI.inst.TutorialFadeOut();
        closeButton.interactable = false;
    }
}
