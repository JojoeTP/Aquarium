using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HitRedScreen : MonoBehaviour
{
    public static HitRedScreen inst;
    [SerializeField] Volume HitVolume;
    public float speed;

    private void Awake() {
        inst = this;
        HitVolume.weight = 0;
    }

    public void OnHit()
    {
        HitVolume.weight = 1;
        StartCoroutine(NormalScreen());
    }

    IEnumerator NormalScreen()
    {
        yield return null;
        if(HitVolume.weight > 0)
        {
            HitVolume.weight -= 0.1f * Time.deltaTime * speed;
            StartCoroutine(NormalScreen());
        }
    }
}
