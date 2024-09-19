using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    private TouchingDirections touchingDirections;
    public Rigidbody2D rigidBody { get; private set; }

    private AnimationController animationController;

    [Header("Stats")]
    public float walkSpeed = 10f;
    public float runSpeed = 15f;
    public float jumpSpeed = 40f;
    public float slideSpeed = 3f;
    public float wallSlideLerp = 1.5f;

    [Header("Booleans")]
    public bool canMove;
    public bool wallSlide;
    public bool sliding;
    public bool wallJumped;

    public bool IsRunning { get; private set; }
    public bool IsMoving { get; private set; }

    public Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
        rigidBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleWallSlide();
        HandleGroundedState();
    }

    private void HandleMovement()
    {
        if (!canMove) return;

        float currentSpeed = IsRunning ? runSpeed : walkSpeed;

        if (wallSlide && IsMovingTowardsWall())
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        else if (!wallJumped)
        {
            rigidBody.velocity = new Vector2(moveInput.x * currentSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, new Vector2(moveInput.x * currentSpeed, rigidBody.velocity.y), wallSlideLerp * Time.fixedDeltaTime);
        }

        if (moveInput.x != 0 && !wallSlide)
        {
            animationController.Flip(moveInput.x);
        }
    }

    private void HandleWallSlide()
    {
        if (!touchingDirections.IsGround && touchingDirections.IsOnWall)
        {
            if (rigidBody.velocity.y > 0)
            {
                wallSlide = false;
                return;
            }

            if (IsMovingTowardsWall())
            {
                StartWallSlide();
            }
            else
            {
                StopWallSlide();
            }
        }
        else
        {
            StopWallSlide();
        }
    }

    private bool IsMovingTowardsWall()
    {
        return (moveInput.x >= 0 && touchingDirections.OnRightWall) || (moveInput.x <= 0 && touchingDirections.OnLeftWall);
    }

    private void StartWallSlide()
    {
        if (!wallSlide)
        {
            rigidBody.velocity = Vector2.zero;
            sliding = true;
        }

        wallSlide = true;
        rigidBody.velocity = new Vector2(0, -slideSpeed);
    }

    private void StopWallSlide()
    {
        wallSlide = false;
        sliding = false;
    }

    private void HandleGroundedState()
    {
        if (touchingDirections.IsGround)
        {
            wallSlide = false;
            sliding = false;
            wallJumped = false;
        }
    }

    private void Jump(Vector2 dir)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(dir * jumpSpeed, ForceMode2D.Impulse);
    }

    private void WallJump()
    {
        StartCoroutine(DisableMovement(0.05f));

        Vector2 wallDir = touchingDirections.OnRightWall ? Vector2.left : Vector2.right;
        Jump(Vector2.up + wallDir);

        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (touchingDirections.IsGround)
            {
                Jump(Vector2.up);
            }
            else if (touchingDirections.IsOnWall)
            {
                WallJump();
            }
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGround)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
}