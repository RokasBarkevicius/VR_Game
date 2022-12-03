using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoolGameController : MonoBehaviour {

	public GameObject cue;
	public GameObject cueBall;
	//public GameObject redBalls;
	public GameObject mainCamera;
	public GameObject winnerMessage;
    //public GameObject turnMessage;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    public bool foul = false;

	//public const float MIN_DISTANCE = 27.5f;
	//public const float MAX_DISTANCE = 32f;
	
	//public IGameObjectState currentState;

	public Player CurrentPlayer;
	public Player OtherPlayer;

	private bool currentPlayerContinuesToPlay = false;

	static public PoolGameController GameInstance {
		get;
		private set;
	}

	void Start() {
		CurrentPlayer = new Player("P1");
		OtherPlayer = new Player("P2");

		GameInstance = this;
		winnerMessage.GetComponent<Canvas>().enabled = false;
        text2.text = string.Format("Current player: {0}", CurrentPlayer.Name);
		//currentState = new GameStates.WaitingForStrikeState(this);
	}
	
	/*void Update() {
		currentState.Update();
	}

	void FixedUpdate() {
		currentState.FixedUpdate();
	}

	void LateUpdate() {
		currentState.LateUpdate();
	}*/

	public void BallPocketed(int ballNumber, string ballType) {
        if(CurrentPlayer.Points < 7 && ballNumber == 8){
            CurrentPlayer.pocketed8Ball();
            EndMatch();
        } 
        else if(CurrentPlayer.Points == 0 && CurrentPlayer.type == ""){
            //currentPlayerContinuesToPlay = true;
		    CurrentPlayer.Collect(ballNumber);
            CurrentPlayer.setType(ballType);
            if(ballType == "Solids"){
                OtherPlayer.setType("Stripes");
            }
            else{
                OtherPlayer.setType("Solids");
            }
        }
        else if(ballType == CurrentPlayer.type){
            currentPlayerContinuesToPlay = true;
		    CurrentPlayer.Collect(ballNumber);
        }
        else if(ballType != CurrentPlayer.type){

            OtherPlayer.Collect(ballNumber);
            NextPlayer();
        }
        else if(CurrentPlayer.Points == 7 && ballNumber == 8){
            CurrentPlayer.Collect(ballNumber);
            EndMatch();
        }
        else{
            Fouled("ball pocketed");
        }

	}

	public void NextPlayer() {
		if (currentPlayerContinuesToPlay) {
            
			currentPlayerContinuesToPlay = false;
			Debug.Log(CurrentPlayer.Name + " continues to play");
			return;
		}
        text2.text = string.Format("Current player: {0}", OtherPlayer.Name);

		Debug.Log(OtherPlayer.Name + " will play");
		var aux = CurrentPlayer;
		CurrentPlayer = OtherPlayer;
		OtherPlayer = aux;
	}

	public void EndMatch() {
		Player winner = null;
		if (CurrentPlayer.Points == 8 || OtherPlayer.ballPocketed8)
			winner = CurrentPlayer; 
		else if (OtherPlayer.Points == 8 || CurrentPlayer.ballPocketed8)
			winner = OtherPlayer;

		var msg = "Game Over\n";

		if (winner != null)
			msg += string.Format("The winner is '{0}'", winner.Name);

		text.text = msg;
		winnerMessage.GetComponent<Canvas>().enabled = true;
	}

    public void Fouled(string message){
        foul = true;
        Debug.Log(string.Format("fouled {0}", message) );
    }

}
