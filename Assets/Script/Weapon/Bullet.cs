using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> Pool { get; set; }

    [SerializeField] protected float speed;
    [SerializeField] protected float atk;
    [SerializeField] protected float lifetime;
    [SerializeField] protected int maxHitCount;
    [SerializeField] protected Color destinationColor = Color.red;
    protected Color sourceColor;

    protected SpriteRenderer bulletRenderer;

    public float Speed { get { return speed; } }

    private void Awake()
    {
        Pool = gameObject.GetComponent<BulletObjectPool>().Pool;
        bulletRenderer = gameObject.GetComponent<SpriteRenderer>();
        sourceColor = bulletRenderer.color;
    }
}