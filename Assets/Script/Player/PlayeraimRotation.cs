using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeraimRotation : MonoBehaviour
{

    [SerializeField] private SpriteRenderer playerREN;
    [SerializeField] private SpriteRenderer armREN;
    [SerializeField] private Transform armPivot;

    private Player _player;

    //けしけしけし

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    void Start()
    {
        _player.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAim)
    {
        RotateArm(newAim);
    }

    private void RotateArm(Vector2 newAim)
    {
        float rotZ = Mathf.Atan2(newAim.y, newAim.x) * Mathf.Rad2Deg;

        armREN.flipY = Mathf.Abs(rotZ) > 90f;
        playerREN.flipX = armREN.flipY;
        armPivot.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
