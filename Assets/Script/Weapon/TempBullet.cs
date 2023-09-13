using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempBullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    public int speed;
    public int livingTime;

    private Vector3 direction;

    const string CollisionTag = "Wall";

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb2D.velocity *= speed;
        Invoke("Destroy", livingTime);
    }

    private void FixedUpdate()
    {
        direction = rb2D.velocity.normalized;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Debug.DrawRay(contact.point, contact.normal, UnityEngine.Color.red);
        Debug.Log(contact.normal.x + " " + contact.normal.y);

        Vector3 reflecVec2 = Vector3.Reflect(direction, contact.normal);

        //Debug.DrawRay(contact.point, reflecVec, UnityEngine.Color.yellow);
        //Debug.DrawRay(contact.point, direction, UnityEngine.Color.magenta);

        rb2D.velocity = GetReflect(direction, contact.normal) * speed;
        //rb2D.velocity = reflecVec2.normalized * speed;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    Vector3 GetReflect(Vector3 inDirection, Vector2 normalVec)
    {
        //float factor = -2f * Vector3.Dot(inDirection, normalVec);
        //return new Vector3(factor * normalVec.x + inDirection.x,
        //    factor * normalVec.y + inDirection.y,
        //    factor * normalVec.z + inDirection.z);
        float dotVec = Vector3.Dot(inDirection, normalVec);
        Vector3 projecVec = normalVec * dotVec;
        Vector3 reflecVec = -2f* projecVec;
        reflecVec += direction;

        return reflecVec;

    }
}
