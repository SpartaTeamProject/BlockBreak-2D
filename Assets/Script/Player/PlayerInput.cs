using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Player
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void OnMove(InputValue InputVec)
    {
        Vector2 moveVec = InputVec.Get<Vector2>();
        CallMoveEvent(moveVec);
    }

    void OnLook(InputValue InputVec)
    {
        Vector2 newAim = InputVec.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }
}
