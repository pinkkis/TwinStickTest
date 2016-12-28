using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]

public class UIScore : MonoBehaviour {
	private GameStateController gameState;

	// Use this for initialization
	void Start () {
		gameState = GameObject.Find("GameState").gameObject.GetComponent<GameStateController>();
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = "Score: " + gameState.playerScore;
	}
}
