using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _speed;
    [SerializeField] private string _animKey;

    private Rigidbody2D _rb;
    private bool _facingRight;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        if (Mathf.Abs(_joystick.Horizontal) > Mathf.Abs(movement))
        {
            movement = _joystick.Horizontal;
        }

        _rb.velocity = new Vector2(movement * _speed, _rb.velocity.y);

        if (!_facingRight && movement < 0)
        {
            Flip();
        }
        else if (_facingRight && movement > 0)
        {
            Flip();
        }

        if (movement > 0 || movement < 0)
        {
            _anim.SetBool(_animKey, true);
        }
        else
            _anim.SetBool(_animKey, false);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
