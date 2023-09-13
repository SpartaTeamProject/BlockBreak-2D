using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager I;
    public GameObject hpimage;
    public Image image;
    public GameObject player;
    public float HP_full;
    public float HP;

    void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        image = hpimage.transform.GetComponent<Image>();
        /*
        if (SceneManager.GetActiveScene().name == "TestScene")
        {
            Instantiate(player);
        }
        */
    }

    void Update()
    {
        HP -= Time.deltaTime;
        image.fillAmount = (HP / HP_full);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "TestScene")
        {
            Instantiate(player);
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
}
