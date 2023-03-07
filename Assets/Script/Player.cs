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

  #region 테두리
  private const float top = 5.2f - 1.0f;
  private const float bottom = -5.2f + 1.0f;
  private float left = -2.78f + 1.0f;
  private float right = 2.78f - 1.0f;
  #endregion

  // 플레이어 속도
  [SerializeField]
  private float speed = 0.02f;
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
    // 벽에 닿아있을 경우 움직이지 않도록 함. RigidBody를 이용하면 캐릭터가 진동하는 현상 발생.
    float h = Input.GetAxisRaw("Horizontal");
    if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
      h = 0;
    float v = Input.GetAxisRaw("Vertical");
    if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
      v = 0;

    // 플레이어 이동
    Vector3 curPos = myTransform.position;  // 플레이어 현재 위치
    Vector3 targetPos = curPos + speed * new Vector3(h, v, 0);  // 이동해야 될 위치
    // 플레이어 위치 업데이트
    myTransform.position = new Vector3(
      Mathf.Clamp(targetPos.x, left, right), Mathf.Clamp(targetPos.y, bottom, top), 0f);
    // 이동 애니메이션
    // h값이 바뀌었을 때만 InputH값을 변경
    if (h != anim.GetInteger("InputH"))
      anim.SetInteger("InputH", (int)h);
  }
}
