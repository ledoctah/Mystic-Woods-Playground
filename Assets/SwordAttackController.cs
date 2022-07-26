using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackController : MonoBehaviour
{
  public float damage = 3;
  public Collider2D swordCollider;
  Vector2 rightAttackOffset;

  public void Start()
  {
    rightAttackOffset = transform.position;
  }

  public void AttackRight()
  {
    swordCollider.enabled = true;
    transform.localPosition = rightAttackOffset;
  }

  public void AttackLeft()
  {
    swordCollider.enabled = true;
    transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
  }

  public void StopAttack()
  {
    swordCollider.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag == "Enemy")
    {
      EnemyController enemy = other.GetComponent<EnemyController>();

      if(enemy != null)
      {
        enemy.Health -= damage;
      }
    }
  }
}
