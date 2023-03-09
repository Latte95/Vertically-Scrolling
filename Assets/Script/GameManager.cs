using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private float deltaTime;

  public GameObject[] enemyObjects;
  public Transform[] spawnPoints;
  public GameObject player;

  public float maxSpawnDelay; // 적이 리스폰 되는데 필요한 시간
  public float curSpawnDelay; // 가장 마지막 적이 리스폰 되고 지난 시간
  private const int enemyTypeNum = 3;   // 적 종류 수
  private const int enemySpawnNum = 7;  // 적 스폰 위치 수
  private const int leftSpawn = 5;      // 적 스폰 위치 수
  private const int rightSpawn = 6;     // 적 스폰 위치 수

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
    int ranEnemy = Random.Range(0, enemyTypeNum); // 생성될 적
    int ranPoint = Random.Range(0, enemySpawnNum); // 생성될 위치
    // 적 생성
    GameObject enemy = Instantiate(enemyObjects[ranEnemy],
            spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
    Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
    Enemy enemyLogic = enemy.GetComponent<Enemy>();
    enemyLogic.player = player;

    if (ranPoint == leftSpawn)
    {
      enemy.transform.Rotate(90 * Vector3.forward);
      rigid.velocity = new Vector2(enemyLogic.speed, -1);
    }
    else if (ranPoint == rightSpawn)
    {
      enemy.transform.Rotate(90 * Vector3.back);
      rigid.velocity = new Vector2(-enemyLogic.speed, -1);
    }
    else
      rigid.velocity = enemyLogic.speed * Vector2.down;


    // 초기화
    maxSpawnDelay = Random.Range(2f, 3f);
    curSpawnDelay = 0;
  }

  public void RespawnPlayer()
  {
    Invoke("RespawnPlayerExe", 1.5f);
  }
  private void RespawnPlayerExe()
  {
    player.transform.position = 4.5f * Vector3.down;
    player.SetActive(true);
  }
}
