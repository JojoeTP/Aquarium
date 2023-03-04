using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Slider loadingBar;

    void Update() 
    {
        UpdateLoadingBar();    
    }

    void UpdateLoadingBar()
    {
        loadingBar.value = SceneController.inst.loadingProgress; //TODO: อาจะปลี่ยนเป็นวงหมุนๆแทน
    }

    public void EnableCavnas(bool enable)
    {
        canvas.enabled = enable;
    }
}
