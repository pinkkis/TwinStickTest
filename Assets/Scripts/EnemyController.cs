using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float health = 1f;
	public float moveTimeMin = 0.5f;
	public float moveTimeMax = 4f;
	public float jumpPower = 10f;
	public int scoreValue = 1;
	public float jumpSpeedUpModifier = 5f;
	public GameObject explosion;

	private GameObject player;
	private float nextMoveTime;
	private float moveTimer;
	private int jumpsTaken = 0;
	private Rigidbody rb;
	private GameStateController gameState;
	private AudioSource audioJump;
	private AudioSource audioDie;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		rb = GetComponent<Rigidbody>();
		gameState = GameObject.Find("GameState").gameObject.GetComponent<GameStateController>();

		var audioSources = GetComponents<AudioSource>();
		audioJump = audioSources[0];
		// audioDie = audioSources[1];

		moveTimer = 0f;
		nextMoveTime = GetRandomMoveTime();
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			gameState.AddPlayerScore(scoreValue);
			Instantiate(explosion, transform.position, Quaternion.identity);
			Die();
		}

		moveTimer += Time.deltaTime;

		if (moveTimer > nextMoveTime) {
			JumpAtPlayer();
		}
	}

	public void TakeDamage(float damageAmount) {
		health -= damageAmount;
	}

	void Die() {
		Destroy(gameObject);
	}

	void JumpAtPlayer() {
		var dir = GetVectorToPlayer();

		rb.AddForce((dir / dir.magnitude) * jumpPower, ForceMode.Impulse);
		audioJump.Play();

		jumpPower = Mathf.Clamp(jumpPower + (jumpsTaken / jumpSpeedUpModifier), 0, 40);

		moveTimer = 0f;
		nextMoveTime = GetRandomMoveTime();
		jumpsTaken++;
	}

	Vector3 GetVectorToPlayer() {
		return player.transform.position - transform.position;
	}

	float GetRandomMoveTime() {
		return Random.Range(moveTimeMin, moveTimeMax);
	}

	void OnTriggerEnter(Collider other)
	{
		// if an enemy got outside and hit bounds, remove it
		if (other.CompareTag("OutOfBounds")) {
			Die();
		}
	}
}
