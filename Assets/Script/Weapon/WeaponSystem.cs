using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    public GameObject bullet_pos;
    public GameObject bullet;
    public float cooltime;

    float curtime;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }
    void shoot()
    {
        if (curtime <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, bullet_pos.transform.position, bullet_pos.transform.rotation);
                curtime = cooltime;
            }
        }
        curtime -= Time.deltaTime;
    }
}
