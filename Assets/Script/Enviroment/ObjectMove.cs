using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    float speed = 10f;
    public Spawner spawner;
    [SerializeField] SpriteRenderer spriteRenderer;
    int alpha;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        alpha = 0;
        spriteRenderer.color = new Color(255, 255, 255, alpha);
        StartCoroutine(FishTransition("In"));
        speed = Random.Range(7,speed);
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
            alpha++;
            spriteRenderer.color += new Color(255, 255, 255, Time.deltaTime);
            StartCoroutine(FishTransition("In"));
        }
        else if (alpha > 0 && state == "Out")
        {
            StopAllCoroutines();
            StartCoroutine(DestroyGameObject());
        }
    }

    public void FishDestroy()
    {
        StartCoroutine(FishTransition("Out"));
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
