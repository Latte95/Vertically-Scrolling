using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  #region 캐싱
  private Animator anim;
  private float deltaTime;
  private Transform myTransform;
  private GameObject collidingObject;
  #endregion

  public float speed;
  public bool isTouchTop;
  public bool isTouchBottom;
  public bool isTouchLeft;
  public bool isTouchRight;

  private void Awake()
  {
    #region 캐싱
    anim = GetComponent<Animator>();
    deltaTime = Time.deltaTime;
    myTransform = transform;
    #endregion
  }

  private void Update()
  {
    float h = Input.GetAxisRaw("Horizontal");
    if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
      h = 0;
    float v = Input.GetAxisRaw("Vertical");
    if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
      v = 0;

    Vector3 curPos = myTransform.position;    // 플레이어 현재 위치
    Vector3 nextPos = speed * deltaTime * new Vector3(h, v, 0); // 이동해야 될 위치

    myTransform.position = curPos + nextPos;

    if (h != anim.GetInteger("InputH"))
      anim.SetInteger("InputH", (int)h);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    collidingObject = collision.gameObject;
    if (collidingObject.tag == "Border")
    {
      switch (collidingObject.name)
      {
        case "Top":
          isTouchTop = true;
          break;
        case "Bottom":
          isTouchBottom = true;
          break;
        case "Left":
          isTouchLeft = true;
          break;
        case "Right":
          isTouchRight = true;
          break;
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collidingObject.tag == "Border")
    {
      switch (collidingObject.name)
      {
        case "Top":
          isTouchTop = false;
          break;
        case "Bottom":
          isTouchBottom = false;
          break;
        case "Left":
          isTouchLeft = false;
          break;
        case "Right":
          isTouchRight = false;
          break;
      }
    }
  }
}
