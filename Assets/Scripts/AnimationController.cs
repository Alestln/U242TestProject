using Assets.Scripts;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private TouchingDirections _touchingDirections;
    private PlayerController _playerController;

    // Animation parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isGroundedHash;
    private int _yVelocityHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _playerController = GetComponent<PlayerController>();

        CacheAnimationHashes();
    }

    private void CacheAnimationHashes()
    {
        _isWalkingHash = Animator.StringToHash(PlayerAnimationStrings.IsMoving);
        _isRunningHash = Animator.StringToHash(PlayerAnimationStrings.IsRunning);
        _isGroundedHash = Animator.StringToHash(PlayerAnimationStrings.IsGrounded);
        _yVelocityHash = Animator.StringToHash(PlayerAnimationStrings.yVelocity);
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        UpdateYVelocity();
    }

    private void UpdateAnimator()
    {
        _animator.SetBool(_isWalkingHash, _playerController.IsMoving);
        _animator.SetBool(_isRunningHash, _playerController.IsRunning);
        _animator.SetBool(_isGroundedHash, _touchingDirections.IsGround);
    }

    private void UpdateYVelocity()
    {
        _animator.SetFloat(_yVelocityHash, _playerController.rigidBody.velocity.y);
    }

    public void Flip(float x)
    {
        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}