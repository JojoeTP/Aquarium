using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] float speed;
    public Animator anim;
    public Spawner spawner;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        this.transform.position += transform.right*speed*Time.deltaTime;
    }

    public void FishDestroy()
    {
        Destroy(this.gameObject);
    }
}
