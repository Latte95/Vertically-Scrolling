using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  private float deltaTime;

  public GameObject[] enemyObjects;
  public Transform[] spawnPoints;

  public float maxSpawnDelay; // 적이 리스폰 되는데 필요한 시간
  public float curSpawnDelay; // 가장 마지막 적이 리스폰 되고 지난 시간

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
    int ranEnemy = Random.Range(0,3); // 생성될 적
    int ranPoint = Random.Range(0,5); // 생성될 위치
    // 적 생성
    Instantiate(enemyObjects[ranEnemy],
            spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

    // 초기화
    maxSpawnDelay = Random.Range(0.5f, 3f);
    curSpawnDelay = 0;
  }
}
