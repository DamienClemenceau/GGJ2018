﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Player player;
    public Text curStamina, maxStamina;
    public Text deathCounter;
    public Text curCollect, maxCollect;

    void Start () {
        curStamina.text = player.stamina.ToString();
        maxStamina.text = player.maxStamina.ToString();

        deathCounter.text = "0";

        curCollect.text = player.blopCollected.ToString();
        maxCollect.text = GameObject.FindObjectsOfType<MiniBlop>().Length.ToString();
    }

    private void LateUpdate()
    {
        curStamina.text = player.stamina.ToString();
        curCollect.text = player.blopCollected.ToString();
    }
}