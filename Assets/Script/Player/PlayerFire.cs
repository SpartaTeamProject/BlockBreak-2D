using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private PlayerInput _controller;

    public GameObject _bullet;
    private Vector2 _aimDir = Vector2.right;

    [SerializeField] private Transform _bulletPivot;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _controller.OnFireEvent += Fire;
    }

    void Fire()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        Instantiate(_bullet, _bulletPivot.position, Quaternion.identity);
        //GameObject obj = Instantiate(_bullet, _bulletPivot.position, Quaternion.identity);
        //bj.GetComponent<Rigidbody2D>().velocity = _controller.bulletDir.normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
