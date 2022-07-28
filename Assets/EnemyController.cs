using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  Animator animator;

  public float health = 1;
  public float Health
  {
    set
    {
      health = value;

      if(health <= 0)
      {
        Defeat();
      } else {
        TakeDamage();
      }
    }

    get
    {
      return health;
    }
  }

  public void Start()
  {
    animator = GetComponent<Animator>();
  }


  public void Defeat()
  {
    animator.SetTrigger("Defeated");
  }

  public void TakeDamage()
  {
    animator.SetTrigger("Damaged");
  }

  public void RemoveEnemy() {
    Destroy(gameObject);
  }
}
