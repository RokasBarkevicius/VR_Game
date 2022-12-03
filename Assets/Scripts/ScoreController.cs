using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var CurrentPlayer = PoolGameController.GameInstance.CurrentPlayer;
        var OtherPlayer = PoolGameController.GameInstance.OtherPlayer;

        text.text = String.Format("{0} - {1}\n{2} - {3}",
        CurrentPlayer.Name, CurrentPlayer.Points, OtherPlayer.Name, OtherPlayer.Points);

    }
}
