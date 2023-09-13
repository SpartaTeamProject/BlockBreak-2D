using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager I;
    public GameObject hpimage;
    public Image image;

    public float HP_full;
    public float HP;

    void Awake()
    {
        if (I == null)
        {
            I = this; //�̱���ȭ
        }
        else
        {
            Destroy(gameObject); // �� �̵� �� �ߺ����� ����
        }
    }

    void Start()
    {
         image = hpimage.transform.GetComponent<Image>();
    }

    void Update()
    {
        HP -= Time.deltaTime;
        image.fillAmount = (HP / HP_full);
    }

    void HP_Add(int XP)
    {
        HP += XP;
    }

    void HP_minus(int XP)
    {
        HP -= XP;
    }
}
