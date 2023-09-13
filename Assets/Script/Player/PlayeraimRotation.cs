using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayeraimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerREN;
    [SerializeField] private SpriteRenderer armREN;
    [SerializeField] private Transform armPivot;

    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMaxDistance;
    private Vector3 center;

    private Player _player;
    private Camera _mainCam;

    private void Awake()
    {
        _player = GetComponent<PlayerInput>();
        _mainCam = Camera.main;
    }
    
    void Start()
    {
        _player.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAim)
    {
        RotateArm(newAim);
        TranslateCam(newAim);
    }

    private void RotateArm(Vector2 newAim)
    {
        Vector3 temp = new Vector3(newAim.x, newAim.y);

        float rotZ = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;

        armREN.flipY = Mathf.Abs(rotZ) > 90f;
        playerREN.flipX = armREN.flipY;
        armPivot.rotation = Quaternion.Euler(0,0,rotZ);
    }

    private void TranslateCam(Vector2 newAim)
    {
        Vector3 cameraPos = _mainCam.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = _mainCam.transform.position.z;
        Vector3 playerPos = _player.transform.position;

        Debug.Log("PlayerPos: " + playerPos.x + " " + playerPos.y);
        Debug.Log("MousePos: "+mousePos.x + " " + mousePos.y);

        Debug.Log("CameraPos: "+cameraPos.x + " " + cameraPos.y);

        Vector3 offset = new Vector3(cameraMaxDistance, cameraMaxDistance);

        //center = new Vector3((playerPos.x + mousePos.x) / 2, (playerPos.y + mousePos.y) / 2, _mainCam.transform.position.z);
        center = new Vector3((playerPos.x + mousePos.x) / 2, (playerPos.y + mousePos.y) / 2, _mainCam.transform.position.z);

        Debug.Log("Center: " + center.x + " " + center.y);

        _mainCam.transform.position = Vector3.Lerp(cameraPos, center, Time.deltaTime * cameraSpeed);
    }
}
