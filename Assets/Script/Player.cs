using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  #region 캐싱
  private Animator anim;
  private float deltaTime;
  private Transform myTransform;
  #endregion

  #region 테두리
  private const float top = 5.2f - 1.0f;
  private const float bottom = -5.2f + 1.0f;
  private const float leftBorder = -2.78f + 1.0f;
  private const float rightBorder = 2.78f - 1.0f;
  #endregion

  // 플레이어
  [SerializeField]
  private float playerSpeed = 0.04f;
  [SerializeField]
  private int playerPower;

  #region 총알
  public GameObject pBullet1;
  public GameObject pBullet2;

  private float bulletSpeed = 10;
  public float shotSpeedDelay;  // 플레이어 공격 속도
  public float curShotDelay;    // 공격한 지 얼마나 지났는지
  private float leftBullet = 0.2f;  // 왼쪽 총알 간격
  private float rightBullet = 0.2f; // 오른쪽 총알 간격
  #endregion

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
    Move();
    Fire();
    Reload();
  }

  private void Move()
  {
    // 플레이어 이동 범위를 제한하는데 RigidBody를 이용하면 캐릭터가 진동하는 현상 발생.
    // Mathf.Clamp를 통해 이동 범위를 제한함

    float h = Input.GetAxisRaw("Horizontal");
    // 왼쪽 또는 오른쪽 벽에서 애니메이션 정지를 위해
    if ((myTransform.position.x == rightBorder && h == 1) ||
        (myTransform.position.x == leftBorder && h == -1))
      h = 0;
    float v = Input.GetAxisRaw("Vertical");

    // 플레이어 이동
    Vector3 curPos = myTransform.position;  // 플레이어 현재 위치
    Vector3 targetPos = curPos + playerSpeed * new Vector3(h, v, 0);  // 이동해야 될 목표 위치
    // 플레이어 위치 업데이트 (위치 범위 제한)
    myTransform.position = new Vector3(
      Mathf.Clamp(targetPos.x, leftBorder, rightBorder),
      Mathf.Clamp(targetPos.y, bottom, top), 0f);
    // 이동 애니메이션
    // h값이 바뀌었을 때만 InputH값을 변경
    if (h != anim.GetInteger("InputH"))
      anim.SetInteger("InputH", (int)h);
  }

  private void Fire()
  {
    // ******** 플레이어 공격 구현 ******** //

    // 공격 버튼을 누르지 않았거나, 공격속도보다 빨리 발사할 경우 공격 중단
    if (!Input.GetButton("Fire1") || curShotDelay < shotSpeedDelay)
      return;

    // 플레이어 파워에 따라 발사하는 총알이 달라짐
    switch (playerPower)
    {
      case 1:
        GameObject bullet = Instantiate(pBullet1, myTransform.position, myTransform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(bulletSpeed * Vector2.up, ForceMode2D.Impulse);
        break;
      case 2:
        GameObject bulletR = Instantiate(pBullet1, myTransform.position + rightBullet * Vector3.right, myTransform.rotation);
        GameObject bulletL = Instantiate(pBullet1, myTransform.position + leftBullet * Vector3.left, myTransform.rotation);
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        rigidR.AddForce(bulletSpeed * Vector2.up, ForceMode2D.Impulse);
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        rigidL.AddForce(bulletSpeed * Vector2.up, ForceMode2D.Impulse);
        break;
      case 3:
        GameObject bullet2 = Instantiate(pBullet2, myTransform.position, myTransform.rotation);
        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
        rigid2.AddForce(bulletSpeed * Vector2.up, ForceMode2D.Impulse);
        break;
      default:
        bullet2 = Instantiate(pBullet2, myTransform.position, myTransform.rotation);
        rigid2 = bullet2.GetComponent<Rigidbody2D>();
        rigid2.AddForce(bulletSpeed * Vector2.up, ForceMode2D.Impulse);
        break;
    }

    // 공격 경과 시간 초기화
    curShotDelay = 0;
  }

  private void Reload()
  {
    curShotDelay += deltaTime;
  }
}
