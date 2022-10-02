using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;

    private float _speed;
    private float _dashForce;
    private float _jumpForce;
    private float _checkRadius;
    private float _deathTimer;
    private int _direction;
    private bool _hasTarget;
    private bool _isGrounded;
    private bool _isAlive;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Transform _playerTransform;
    private Vector3 _lastPosition;
    private Vector3 _currentPosition;


    private void Start()
    {
        _deathTimer = 0;
        _isAlive = true;
        _checkRadius = 0.01f;
        _lastPosition = transform.position;
        _dashForce = 300f;
        _jumpForce = 500f;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (_isAlive)
        {
            _direction = _playerTransform.position.x - transform.position.x < 0 ? 1 : -1;
            Flip(_direction);
            ReactToPlayer();
            ChangePositionValue();
            if (IsPositionChanged() == false && _hasTarget)
            {
                Dash();
            }
            if (IsUnderPlayer() && _isGrounded)
            {
                Jump();
            }
        } else
        {
            _deathTimer += Time.deltaTime;
            if (_deathTimer > 2)
            {
                Destroy(this.gameObject);
            }
            if (_isGrounded)
            {
                GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    public void MoveToPlayer()
    {
        transform.position += _speed * Time.deltaTime * _direction * Vector3.left;
    }

    public void ReactToPlayer()
    {
        Vector3 _playerDirection = _playerTransform.position - transform.position;
        _hasTarget = _playerDirection.magnitude < 100.0f;
    }

    public void AttackPlayer()
    {
        //Behavior if player is inside attack area
    }

    private void Flip(int direction)
    {
        _spriteRenderer.flipX = direction == 1 ? true : false;
    }

    protected void Dash()
    {
        Vector3 dashDirection = Vector3.left * _direction;
        _rigidBody.AddForce(dashDirection * _dashForce);
    }

    private void ChangePositionValue()
    {
        _lastPosition = _currentPosition;
        _currentPosition = transform.position;
    }

    private bool IsPositionChanged()
    {
        Vector3 positionChange = _currentPosition - _lastPosition;
        bool isPositionChanged = positionChange.magnitude > 0.06f;
        return isPositionChanged;
    }

    private bool IsUnderPlayer()
    {
        bool isUnderPlayer = _playerTransform.position.y - transform.position.y > 0 ? true : false;
        return isUnderPlayer;
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce);
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _ground);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            _isAlive = false;
            _animator.SetTrigger("Death");
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            _isAlive = false;
            _animator.SetTrigger("Death");
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}