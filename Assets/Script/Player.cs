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

  // 플레이어 속도
  private float speed = 1.3f;
  // 플레이어가 벽에 닿았는지 판단하는 bool값
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
    // 벽에 닿아있을 경우 움직이지 않도록 함. 충돌만 이용하면 캐릭터가 진동하는 현상 발생.
    float h = Input.GetAxisRaw("Horizontal");
    if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
      h = 0;
    float v = Input.GetAxisRaw("Vertical");
    if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
      v = 0;

    // 플레이어 이동
    Vector3 curPos = myTransform.position;    // 플레이어 현재 위치
    Vector3 nextPos = speed * deltaTime * new Vector3(h, v, 0); // 이동해야 될 위치
    myTransform.position = curPos + nextPos;
    // 이동 애니메이션
    if (h != anim.GetInteger("InputH"))
      anim.SetInteger("InputH", (int)h);
  }

  // 플레이어가 벽에 닿았는지 판단
  private void OnTriggerStay2D(Collider2D collision)
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
  
  // 플레이어가 벽에서 나왔는지 판단
  private void OnTriggerExit2D(Collider2D collision)
  {
    collidingObject = collision.gameObject;
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
