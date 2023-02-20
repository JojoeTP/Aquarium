using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] fishPrefabs;
    Collider2D spawnArea;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    [SerializeField] bool spawnLeft;
    [SerializeField] int fishCount;
    public int currentFish;
    private void Start()
    {
        spawnArea = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator Spawn()
    {
        yield return null;

        while (enabled)
        {
            if (currentFish < fishCount)
            {
                Vector3 position = Vector3.zero;
                position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
                position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
                position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

                Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
                int randomFishPrefab = Random.Range(0, fishPrefabs.Length);
                if(spawnLeft == false)
                {
                    rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                GameObject fishs = Instantiate(fishPrefabs[randomFishPrefab], position, rotation , this.transform);
                fishs.GetComponent<ObjectMove>().spawner = this;
                currentFish += 1;
            }
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ObjectMove>(out var objectMove))
        {
            objectMove.anim.SetTrigger("TransitionOut");
            objectMove.spawner.currentFish--;
        }
    }
}
