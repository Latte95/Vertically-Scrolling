using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
  public float speed;
  public int health;
  public Sprite[] sprites;

  SpriteRenderer spriteRenderer;
  Rigidbody2D rigid;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    rigid = GetComponent<Rigidbody2D>();
    rigid.velocity = Vector2.down * speed;
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

  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 맵을 넘어간 경우
    if (collision.gameObject.tag == Bullet.bulletBordTag)
      Destroy(gameObject);
    // 플레이어 공격에 피격 된 경우
    else if (collision.gameObject.tag == Bullet.playerBulletTag)
    {
      Bullet bullet = collision.gameObject.GetComponent<Bullet>();
      OnHit(bullet.dmg);
      // 총알 소멸
      Destroy(collision.gameObject);
    }
  }
}
