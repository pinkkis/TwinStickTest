using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float bulletSpeed = 1f;
	public float bulletLifeTime = 2f;
	public float bulletDamage = 1f;

	private float lifeCounter;
	private bool hasHitSomething = false;

	// Use this for initialization
	void Start () {
		lifeCounter = 0;
	}

	// Update is called once per frame
	void Update () {
		lifeCounter += Time.deltaTime;

		if (lifeCounter > bulletLifeTime) {
			Die();
		}

		if (!hasHitSomething)
			transform.position += (transform.rotation * Vector3.forward) * bulletSpeed * Time.deltaTime;
	}

	public void Die() {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{

		hasHitSomething = true;

		if (other.CompareTag("Enemy")) {
			other.GetComponent<EnemyController>().TakeDamage(bulletDamage);
			Die();
		}

		if (other.CompareTag("Arena")) {
			Die();
		}
	}
}
