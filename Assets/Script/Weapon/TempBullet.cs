using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class TempBullet : Bullet
{
    private Rigidbody2D rb2D;
    private Vector3 _direction;
    private float _currentSpeed;
    private float _currentAtk;
    private float _currentHitCount;

    private const string MONSTER_TAG = "Monster";

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bulletRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log("color1: " + destinationColor.r + "/" + destinationColor.g + "/" + destinationColor.b);
        this.gameObject.SetActive(true);
    }

    void Start()
    {
        _currentSpeed = speed;
        _currentAtk = atk;
        _currentHitCount = 0;
        sourceColor = bulletRenderer.color;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        _direction = rb2D.velocity.normalized;
    }

    // 오브젝트 풀링에 의한 활성화/비활성화
    void OnEnable()
    {
        rb2D.velocity *= _currentSpeed;
        Invoke("ReturnToPool", lifetime);
    }

    void OnDisable()
    {
        ResetBullet();
    }

    // Pool 관련 처리
    void ResetBullet()
    {
        _direction = Vector3.zero;
        _currentSpeed = speed;
        _currentAtk = atk;
        _currentHitCount = 0;
        bulletRenderer.color = sourceColor;
    }

    void ReturnToPool()
    {
        Pool.Release(this);
    }

    // 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        //rb2D.velocity = Vector3.Reflect(_direction, contact.normal);

        rb2D.velocity = GetReflect(_direction, contact.normal)* _currentSpeed;

        //if (collision.gameObject.CompareTag(MONSTER_TAG))
        HitEnemy(this);
    }

    Vector3 GetReflect(Vector3 inDirection, Vector2 normalVec)
    {
        float dotVec = Vector3.Dot(inDirection, normalVec);
        Vector3 projecVec = normalVec * dotVec;

        Vector3 reflecVec = -2f* projecVec;
        reflecVec += _direction;

        return reflecVec;
    }

    void HitEnemy(TempBullet target)
    {
        if (_currentHitCount >= maxHitCount)
            this._currentHitCount = 0;
        this._currentHitCount++;

        //Debug.Log("color1: " + bulletRenderer.color.r + "/" + bulletRenderer.color.g + "/" + bulletRenderer.color.b + "/" + bulletRenderer.color.a);
        bulletRenderer.color = Color.Lerp(sourceColor, destinationColor, _currentHitCount/ maxHitCount);
        _currentSpeed += (speed*0.2f);
        _currentAtk += (atk*0.5f);
    }
}
