using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;

    private Vector2 _movment = Vector2.zero;//초기화 길게 안쓰려고 이렇게 함
    private Rigidbody2D _rigidbody;
    public Animator spritAnim;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        spritAnim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _player.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApllyMovement(_movment);
    }

    private void Move(Vector2 direction)
    {
        _movment = direction;
    }

    private void ApllyMovement(Vector2 direction)
    {
        direction = direction * 5;
        _rigidbody.velocity = direction;

        if (direction.x == 0 && direction.y == 0)
        {
            spritAnim.SetBool("isRun",false);
        }
        else
        {
            spritAnim.SetBool("isRun", true);
        }
    }
}
