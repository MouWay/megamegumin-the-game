using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _missile;
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _gun;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _missilePosition;
    [SerializeField] private Transform _shoulderPosition;
    [SerializeField] private LayerMask _ground;

    private float _speed;
    private float _jumpForce;
    private float _checkRadius;
    private bool _isAlive;
    private bool _isGrounded;
    private Vector3 _moveVector;
    private Vector3 _direction;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _playerCollider;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _direction = GetDirection();
        _isAlive = true;
        _playerCollider = _player.GetComponent<BoxCollider2D>();
        _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        _animator = _player.GetComponent<Animator>();
        _rigidBody = _player.GetComponent<Rigidbody2D>();
        _speed = 5f;
        _jumpForce = 800f;
        _checkRadius = 0.1f;
    }

    private void Update()
    {
        CheckGround();
        if (_isAlive)
        {
            SetCursorDirectionToShoot();
            Move();
            Flip();

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        } else
        {
            if (_isGrounded)
            {
                GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce);
    }
    
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _ground);
    }

    private void Shoot()
    {
        int direction = _spriteRenderer.flipX ? -1 : 1;
        Instantiate(_missile, _missilePosition.position, Quaternion.Euler(_gun.transform.eulerAngles));
        _animator.SetTrigger("Shoot");
    }

    private void Move()
    {
        _animator.SetFloat("moveX", Mathf.Abs(Input.GetAxis("Horizontal")));
        float _horizontalMove = _speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        _moveVector = new Vector3(_horizontalMove, 0, 0);
        _player.transform.position += _moveVector;
    }

    private void Flip()
    {
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = true;
            _shoulderPosition.localPosition = new Vector3(-0.11f, 0.07f, 0);
            _playerCollider.offset = new Vector2(-0.05f, 0);
        } 
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = false;
            _shoulderPosition.localPosition = new Vector3(0.11f, 0.07f, 0);
            _playerCollider.offset = new Vector2(0.05f, 0);
        }
    }

    private void SetCursorDirectionToShoot()
    {
        _direction = GetDirection();
        var angle = Vector2.Angle(_direction, Vector2.right) + 180;
        _hand.transform.localEulerAngles = new Vector3(0f, 0f, GetDirection().y > 0 ? angle : -angle);
        if (GetDirection().x > 0)
        {
            _hand.GetComponent<SpriteRenderer>().flipY = true;
            _gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            _hand.GetComponent<SpriteRenderer>().flipY = false;
            _gun.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private void Death()
    {
        _isAlive = false;
        _animator.SetTrigger("Death");
        GetComponent<BoxCollider2D>().enabled = false;
        tag = "Untagged";
        GetComponent<AudioSource>().enabled = true;
    }

    private Vector3 GetDirection()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mousePosition - transform.position;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyWeapon"))
        {
            Death();
        }
    }
}
