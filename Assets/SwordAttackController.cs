using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackController : MonoBehaviour
{
  Vector2 rightAttackOffset;
  Collider2D swordCollider;

  public void Start()
  {
    swordCollider = GetComponent<Collider2D>();
    rightAttackOffset = transform.position;
  }

  public void AttackRight()
  {
    swordCollider.enabled = true;
    transform.position = rightAttackOffset;
  }

  public void AttackLeft()
  {
    swordCollider.enabled = true;
    transform.position = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
  }

  public void StopAttack()
  {
    swordCollider.enabled = false;
  }
}
