using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float health = 1;
  public float Health
  {
    set
    {
      health = value;

      if(health <= 0)
      {
        Defeat();
      }
    }

    get
    {
      return health;
    }
  }


  public void Defeat()
  {
    Destroy(gameObject);
  }
}
