using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _missile;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _missilePosition;

    private float _speed;
    private float _jumpForce;
    private float _checkRadius;
    private float _deathTimer;
    private bool _isAlive;
    private Vector3 _moveVector;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _playerCollider;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isGrounded;

    private void Start()
    {
        _deathTimer = 0f;
        _isAlive = true;
        _playerCollider = _player.GetComponent<BoxCollider2D>();
        _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        _animator = _player.GetComponent<Animator>();
        _rigidBody = _player.GetComponent<Rigidbody2D>();
        _speed = 4f;
        _jumpForce = 400f;
        _checkRadius = 0.1f;
    }

    private void Update()
    {
        CheckGround();
        if (_isAlive)
        {
            _animator.SetFloat("moveX", Mathf.Abs(Input.GetAxis("Horizontal")));
            float _horizontalMove = _speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            _moveVector = new Vector3(_horizontalMove, 0, 0);
            _player.transform.position += _moveVector;
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
            _deathTimer += Time.deltaTime;
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
        Instantiate(_missile, _missilePosition.position, Quaternion.Euler(0, 0, 90 - 90 * direction));
        _animator.SetTrigger("Shoot");
    }

    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            _spriteRenderer.flipX = true;
            _missilePosition.localPosition = new Vector3(0.179f, 0.135f, 0);
            _playerCollider.offset = new Vector2(-0.05f, 0);
        } 
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _spriteRenderer.flipX = false;
            _missilePosition.localPosition = new Vector3(-0.179f, 0.135f, 0);
            _playerCollider.offset = new Vector2(0.05f, 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
    }
}
