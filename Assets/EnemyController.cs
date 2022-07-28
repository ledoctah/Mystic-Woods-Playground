using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  Animator animator;
  float healthBarInitialSize;
  float initialHealth;
  long lastHideTimestamp;

  public SpriteRenderer healthBar;
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

    initialHealth = Health;

    healthBarInitialSize = healthBar.transform.localScale.x;
  }


  public void Defeat()
  {
    healthBar.enabled = false;
    animator.SetTrigger("Defeated");
  }

  public void TakeDamage()
  {
    healthBar.enabled = true;

    animator.SetTrigger("Damaged");

    float percentage = Health / initialHealth;

    healthBar.transform.localScale = new Vector2(percentage * healthBarInitialSize, healthBar.transform.localScale.y);

    long time = System.DateTime.Now.ToFileTimeUtc();

    lastHideTimestamp = time;

    StartCoroutine(HideHealthBar(time));
  }

  private IEnumerator HideHealthBar(long time) {
    yield return new WaitForSeconds(2);
    
    if(time == lastHideTimestamp) {
      healthBar.enabled = false;
    }
  }

  public void RemoveEnemy() {
    Destroy(gameObject);
  }
}
