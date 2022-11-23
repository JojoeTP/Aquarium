using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClicks : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource clickSource;

    public AudioClip hoverSound;
    public AudioClip clickSound;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        clickSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickSource.PlayOneShot(clickSound);
    }
}

