using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    int level = 0;
    int Player = 0;
    int totalScore;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void addPlayer(int score)
    {
        Player += 1;
        level = Player / 5;
    }
}
