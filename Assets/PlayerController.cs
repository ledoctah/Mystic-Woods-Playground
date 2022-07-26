using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public float speed = 1f;
  public float collisionOffset = 0.05f;
  public ContactFilter2D movementFilter;
  Vector2 movementInput;
  Rigidbody2D rb;
  List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    if(movementInput == Vector2.zero)
    {
      return;
    }

    bool canMoveInBothDirections = TryMove(movementInput);

    if(canMoveInBothDirections) {
      return;
    }

    bool canMoveHorizontally = TryMove(new Vector2(movementInput.x, 0f));

    if(canMoveHorizontally) {
      return;
    }

    bool canMoveVertically = TryMove(new Vector2(0f, movementInput.y));

    if(canMoveVertically) {
      return;
    }
  }

  private bool TryMove(Vector2 direction) {
    int count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);

    // if user is colliding with something the player will not be allowed to move
    if(count != 0)
    {
      return false;
    }

    rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

    return true;
  }

  void OnMove(InputValue movementValue)
  {
    movementInput = movementValue.Get<Vector2>();
  }
}
