using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnFireEvent;

    public void CallMoveEvent(Vector2 vector2)
    {
        OnMoveEvent?.Invoke(vector2);
    }
    public void CallLookEvent(Vector2 vector2)
    {
        OnLookEvent?.Invoke(vector2);
    }
    public void CallFireEvent()
    {
        OnFireEvent?.Invoke();
    }
}
