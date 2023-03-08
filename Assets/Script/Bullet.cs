using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private GameObject mGameObj;
  private const string bulletBord = "Border Bullet";

  private void Awake()
  {
    mGameObj = gameObject;
  }
  
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == bulletBord)
      Destroy(gameObject);
  }
}
