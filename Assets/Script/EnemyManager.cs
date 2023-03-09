using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  private float deltaTime;

  public GameObject[] enemyObjects;
  public Transform[] spawnPoints;

  public float maxSpawnDelay;
  public float curSpawnDelay;

  private void Awake()
  {
    deltaTime = Time.deltaTime;
  }

  private void Update()
  {
    curSpawnDelay += deltaTime;

    if (curSpawnDelay > maxSpawnDelay)
      SpawnEnemy();
  }

  private void SpawnEnemy()
  {
    int ranEnemy = Random.Range(0,3);
    int ranPoint = Random.Range(0,5);
    Instantiate(enemyObjects[ranEnemy],
            spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

    maxSpawnDelay = Random.Range(0.5f, 3f);
    curSpawnDelay = 0;
  }
}
