using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private float deltaTime;
  private SpriteRenderer spriteRenderer;
  public GameObject player;

  public float speed;
  public int health;
  public Sprite[] sprites;

  public string enemyName;

  #region 총알
  public GameObject eBullet1;
  public GameObject eBullet2;

  private float bullet1Speed = 5;
  private float bullet2Speed = 3;
  public float shotSpeedDelay;  // 적 공격 속도
  public float curShotDelay;    // 공격한 지 얼마나 지났는지
  private float leftBullet = 0.4f;  // 적L 왼쪽 총알 간격
  private float rightBullet = 0.4f; // 적L 오른쪽 총알 간격
  #endregion

  private void Awake()
  {
    deltaTime = Time.deltaTime;
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    Fire();
    Reload();
  }

  private void Fire()
  {
    // ******** 적 공격 구현 ******** //

    // 공격속도보다 빨리 발사할 경우 공격 중단
    if (curShotDelay < shotSpeedDelay)
      return;

    // 적 종류에 따라 발사하는 총알이 달라짐
    Vector3 dirVec = (player.transform.position - transform.position).normalized;
    switch (enemyName)
    {
      case "S":
        GameObject bullet = Instantiate(eBullet1, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(bullet1Speed * dirVec, ForceMode2D.Impulse);
        break;
      case "L":
        GameObject bulletR = Instantiate(eBullet2, transform.position + rightBullet * Vector3.right, transform.rotation);
        GameObject bulletL = Instantiate(eBullet2, transform.position + leftBullet * Vector3.left, transform.rotation);
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        rigidR.AddForce(bullet2Speed * dirVec, ForceMode2D.Impulse);
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        rigidL.AddForce(bullet2Speed * dirVec, ForceMode2D.Impulse);
        break;
    }

    // 공격 경과 시간 초기화
    curShotDelay = 0;
  }

  private void Reload()
  {
    curShotDelay += deltaTime;
  }

  private void OnHit(int dmg)
  {
    // 적 체력 감소
    health -= dmg;
    // 피격 애니메이션
    spriteRenderer.sprite = sprites[1];
    Invoke("ReturnSprite", 0.1f);

    // 적 소멸
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }

  private void ReturnSprite()
  {
    spriteRenderer.sprite = sprites[0];
  }

  // 적이 맵을 넘어갔을 때, 총알이 적에게 맞았을 때
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 맵을 넘어간 경우
    if (collision.gameObject.tag == Bullet.tagBulletBord)
      Destroy(gameObject);
    // 플레이어 공격에 피격 된 경우
    else if (collision.gameObject.tag == Bullet.tagPlayerBullet)
    {
      Bullet bullet = collision.gameObject.GetComponent<Bullet>();
      OnHit(bullet.dmg);
      // 총알 소멸
      Destroy(collision.gameObject);
    }
  }
}
