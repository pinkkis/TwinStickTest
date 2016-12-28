using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {
	public int currentWave;
	public bool gameInProgress = false;
	public Rigidbody enemy;
	public int baseWaveTime = 5;
	public float difficultyMultiplier = 1f;
	public int maxEnemies = 100;

	public int playerScore = 0;

	private GameObject enemyContainer;

	private float waveTimer = 0f;

	// Use this for initialization
	void Start () {
		enemyContainer = GameObject.Find("EnemyContainer").gameObject;

		currentWave = 1;
		gameInProgress = true;
	}

	// Update is called once per frame
	void Update () {

		if (!gameInProgress)
			return;

		waveTimer += Time.deltaTime;

		if (waveTimer > (baseWaveTime * difficultyMultiplier) || enemyContainer.transform.childCount == 0) {
			waveTimer = 0f;
			currentWave++;

			SpawnWave();
		}
	}

	void SpawnWave() {
		int waveSize = 2 + (int) Mathf.Round(currentWave * 1.5f);
		int spawnSize = Mathf.Min(waveSize, maxEnemies - enemyContainer.transform.childCount);

		for (var i = 0; i < spawnSize; i++) {
			SpawnEnemy();
		}
	}

	void SpawnEnemy() {
		Vector3 pos = new Vector3(Random.Range(-20f, 20f), enemyContainer.transform.position.y + 15, Random.Range(-20f, 20f));
		Rigidbody e = Instantiate(enemy, pos, Random.rotation);
		e.transform.parent = enemyContainer.transform;
	}

	public void AddPlayerScore(int amount) {
		playerScore += amount;
	}
}
