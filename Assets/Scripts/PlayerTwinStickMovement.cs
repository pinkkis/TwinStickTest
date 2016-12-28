using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwinStickMovement : MonoBehaviour {
	public float movementSpeed = 10f;
	public GameObject bullet;
	public float shootDelay = 0.1f;
	public string movementHorizontalAxis = "Horizontal";
	public string movenentVerticalAxis = "Vertical";
	public string aimHorizontalAxis = "AimHorizontal";
	public string aimVerticalAxis = "AimVertical";

	private bool canShoot = true;
	private GameObject bulletSpawnPoint;
	private GameObject bulletContainer;
	private AudioSource audioSource;

	void Start()
	{
		bulletSpawnPoint = transform.FindChild("BulletSpawnPoint").gameObject;
		bulletContainer = GameObject.Find("BulletContainer").gameObject;
		audioSource = GetComponent<AudioSource>();
	}

	void ResetShot() {
		canShoot = true;
	}

	void Update () {
		// move
		Vector3 movementStickVector = (Vector3.right * Input.GetAxis(movementHorizontalAxis) + Vector3.forward * Input.GetAxis(movenentVerticalAxis)).normalized;
		transform.position += movementStickVector * movementSpeed * Time.deltaTime;

		// shoot / aim
		Vector3 aimStickVector = (Vector3.right * Input.GetAxis(aimHorizontalAxis) + Vector3.forward * Input.GetAxis(aimVerticalAxis)).normalized;

		if (canShoot && aimStickVector.sqrMagnitude > 0.0f) {
			transform.rotation = Quaternion.LookRotation(aimStickVector, Vector3.up);
			Shoot();
		}
	}
	void Shoot() {
		audioSource.Play();
		GameObject b = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
		b.transform.parent = bulletContainer.transform;

		canShoot = false;
		Invoke("ResetShot", shootDelay);
	}
}
