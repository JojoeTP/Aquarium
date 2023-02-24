using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] float speed;
    public Spawner spawner;
    [SerializeField] SpriteRenderer spriteRenderer;
    int alpha;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        alpha = 0;
        spriteRenderer.color = new Color(255, 255, 255, alpha);
        StartCoroutine(FishTransition("In"));
    }
    void Update()
    {
        this.transform.position += transform.right*speed*Time.deltaTime;
    }
    IEnumerator FishTransition(string state)
    {
        yield return new WaitForSeconds(0.05f);
        if (alpha < 255 && state == "In")
        {
            spriteRenderer.color += new Color(255, 255, 255, Time.deltaTime);
            alpha++;
            StartCoroutine(FishTransition("In"));
        }
        else if (alpha > 0 && state == "Out")
        {
            StopAllCoroutines();
            spriteRenderer.color -= new Color(255, 255, 255, Time.deltaTime);
            alpha--;
            if (alpha > 0 && alpha <= 255)
            {
                StartCoroutine(FishTransition("Out"));
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void FishDestroy()
    {
        StartCoroutine(FishTransition("Out"));
    }
}
