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
  SpriteRenderer spriteRenderer;
  Rigidbody2D rb;
  Animator animator;
  List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

  bool canMove = true;
  public SwordAttackController swordAttackController;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void FixedUpdate() {
    if(!canMove) {
      return;
    }

    if(movementInput != Vector2.zero)
    {
      bool isMoveSuccess = TryMove(movementInput);

      if(movementInput.x != 0) {
        bool isPlayerLookingLeft = movementInput.x < 0f;
        
        spriteRenderer.flipX = isPlayerLookingLeft;
      }

      if(!isMoveSuccess)
      {
        isMoveSuccess = TryMove(new Vector2(movementInput.x, 0f));
      }

      if(!isMoveSuccess)
      {
        isMoveSuccess = TryMove(new Vector2(0f, movementInput.y));
      }

      animator.SetBool("isMoving", isMoveSuccess);

      return;
    }
    
    animator.SetBool("isMoving", false);
  }

  private bool TryMove(Vector2 direction) {
    if(direction == Vector2.zero )
    {
      return false;
    }

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

  void OnFire()
  {
    animator.SetTrigger("swordAttack");
  }

  public void SwordAttack()
  {
    LockMovement();

    if(spriteRenderer.flipX) {
      swordAttackController.AttackLeft();
    } else {
      swordAttackController.AttackRight();
    }
  }

  public void EndSwordAttack()
  {
    UnlockMovement();
    swordAttackController.StopAttack();
  }

  public void LockMovement()
  {
    canMove = false;
  }

  public void UnlockMovement()
  {
    canMove = true;
  }
}
