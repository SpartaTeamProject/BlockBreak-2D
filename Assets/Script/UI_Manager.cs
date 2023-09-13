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
    public GameObject Retrybtn;

    public float HP_full;
    public float HP;

    

    void Awake()
    {
        if (I == null)
        {
            I = this; //ΩÃ±€≈Ê»≠
        }
        else
        {
            Destroy(gameObject); // æ¿ ¿Ãµø Ω√ ¡ﬂ∫πª˝º∫ πÊ¡ˆ
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

        if (HP <= 0)
        {
            gameOver();
        }

        
    }

    public void HP_Add(int XP)
    {
        HP += XP;
        if (HP_full < HP)
        {
            HP = HP_full;
        }
    }

    public void HP_minus(int XP)
    {
        HP -= XP;
    }

    public void gameOver()
    {
        Retrybtn.SetActive(true);
        Time.timeScale = 1.0f;
       
    }
}
