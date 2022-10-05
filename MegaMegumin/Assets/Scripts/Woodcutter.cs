using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _axe;
    [SerializeField] private List<Transform> _hands;

    private float _speed;
    private float _dashForce;
    private float _jumpForce;
    private float _checkRadius;
    private float _deathTimer;
    private float _attackDistance;
    private float _attackCooldown;
    private int _direction;
    private int _difficulty;
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
        _difficulty = PlayerPrefs.GetInt("Difficulty");
        _attackCooldown = 0;
        _attackDistance = 3f;
        _deathTimer = 0;
        _isAlive = true;
        _checkRadius = 0.1f;
        _lastPosition = transform.position;
        _dashForce = GetDashForce(_difficulty);
        _jumpForce = 500f;
        if (GameObject.FindGameObjectWithTag("Player") != null) { 
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (_isAlive)
        {
            if (GetDistanceToPlayer() - _attackDistance <= 0.001f && _attackCooldown == 0)
            {
                AttackPlayer();
                _attackCooldown = 5f;
            }
            if (_playerTransform != null)
            {
                _direction = _playerTransform.position.x - transform.position.x < 0 ? 1 : -1;
            }
            Flip(_direction);
            ReactToPlayer();
            ChangePositionValue();
            if (IsUnderPlayer() && _isGrounded)
            {
                Jump();
            }
            if (IsPositionChanged() == false && _hasTarget)
            {
                Dash();
            }  
            if(_attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
            }
            if(_attackCooldown < 0)
            {
                _attackCooldown = 0;
            }
        }
        else
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
        _hasTarget = GetDistanceToPlayer() < 100.0f;
    }

    public void AttackPlayer()
    {
        foreach (var handPosition in _hands)
        {
            var axe = Instantiate(_axe, handPosition.position, Quaternion.identity);
            axe.transform.SetParent(this.gameObject.transform);
        }
        _animator.SetTrigger("Attack");
    }

    private float GetDistanceToPlayer()
    {
        if (_playerTransform != null)
        {
            Vector3 _playerDirection = _playerTransform.position - transform.position;
            return _playerDirection.magnitude;
        }
        return 0;
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
        if (_playerTransform != null)
        {
            bool isUnderPlayer = _playerTransform.position.y - transform.position.y > 0 ? true : false;
            return isUnderPlayer;
        } else
        {
            return false;
        }
    }
    private float GetDashForce(int difficulty)
    {
        return (difficulty + 1) * 100f;
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce);
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _ground);
    }

    private void Death()
    {
        _animator.SetTrigger("Death");
        GetComponent<CircleCollider2D>().enabled = false;
        tag = "Untagged";
        _isAlive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            Death();
        }
    }
}