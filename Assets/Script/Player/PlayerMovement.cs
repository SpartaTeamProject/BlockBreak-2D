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
    int speed = 3;

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
        transform.position += new Vector3(direction.x,direction.y,0) * Time.deltaTime * speed;
        if (transform.position.x > 2) { transform.position = new Vector3(2f, transform.position.y, 0); }
        if (transform.position.x < -2) { transform.position = new Vector3(-2f, transform.position.y, 0); }
        if (transform.position.y > 2) { transform.position = new Vector3(transform.position.x,2f, 0); }
        if (transform.position.y < -1.8f) { transform.position = new Vector3(transform.position.x, -1.8f, 0); }
        if (direction.x == 0 && direction.y == 0)
        {
            spritAnim.SetBool("isRun",false);
        }
        else
        {
            spritAnim.SetBool("isRun", true);
        }
    }

    private void OnDestroy()
    {
        _player.OnMoveEvent -= Move;
    }
}
