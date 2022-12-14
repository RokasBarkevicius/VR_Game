using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    PoolGameController pgController;
    GameObject[] solidBalls;
    GameObject[] stripedBalls;
    GameObject ball8;
    CueHandler ch;

    // Start is called before the first frame update
    void Start()
    {
        pgController = gameObject.GetComponent<PoolGameController>();
        solidBalls = GameObject.FindGameObjectsWithTag("Solids");
        stripedBalls = GameObject.FindGameObjectsWithTag("Stripes");
        ball8 = GameObject.FindGameObjectWithTag("8Ball");
        ch = pgController.cue.GetComponent<CueHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pgController.foul && !ch.hitCueBall){
            PlayerFouled();
        }
        if(ch.hitCueBall){
            StrikedCueBall();
        }
        
    }

    void FixedUpdate(){
        
    }

    public void PlayerFouled(){
        pgController.NextPlayer();
        pgController.foul = false;
    }

    public void StrikedCueBall(){
        var cueBallBody = pgController.cueBall.GetComponent<Rigidbody>();
        if (!(cueBallBody.IsSleeping() || cueBallBody.velocity == Vector3.zero))
            return;
        getBalls();
        if(ball8 != null){
            var ball8Body = ball8.GetComponent<Rigidbody>();
            if (!(ball8Body.IsSleeping() || ball8Body.velocity == Vector3.zero))
                return;
        }
        
        foreach (var sb in solidBalls) {
            var rigidbody = sb.GetComponent<Rigidbody>();
            if (!(rigidbody.IsSleeping() || rigidbody.velocity == Vector3.zero))
                return;
        }
        foreach (var sb in stripedBalls) {
            var rigidbody2 = sb.GetComponent<Rigidbody>();
            if (!(rigidbody2.IsSleeping() || rigidbody2.velocity == Vector3.zero))
                return;
        }
        if(pgController.foul){
            pgController.currentPlayerContinuesToPlay = false;
            pgController.foul = false;
        }
        ch.hitCueBall = false;
        pgController.NextPlayer();
        
    }

    public void getBalls(){
        solidBalls = GameObject.FindGameObjectsWithTag("Solids");
        stripedBalls = GameObject.FindGameObjectsWithTag("Stripes");
        ball8 = GameObject.FindGameObjectWithTag("8Ball");
    }


}
