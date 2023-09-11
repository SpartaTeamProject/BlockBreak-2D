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
        Debug.Log("OnFire");
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        GameObject obj = Instantiate(_bullet, _bulletPivot.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = _controller.bulletDir.normalized * 5f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
