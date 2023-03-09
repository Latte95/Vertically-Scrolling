using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public const string bulletBordTag = "Border Bullet";
  public const string playerBulletTag = "Player Bullet";
  public int dmg;
  
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 총알이 맵을 벗어나면 소멸
    if (collision.gameObject.tag == bulletBordTag)
      Destroy(gameObject);
  }
}
