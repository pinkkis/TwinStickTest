using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {

	public GameObject cameraTarget;
	public float movementSmoothing = 1f;

	private Transform target;


	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		target = cameraTarget.transform;
	}

	void LateUpdate () {
		float tX = Mathf.Lerp(transform.position.x, target.position.x, movementSmoothing * Time.deltaTime);
		float tZ = Mathf.Lerp(transform.position.z, target.position.z, movementSmoothing * Time.deltaTime);

		transform.position = new Vector3(tX, target.position.y, tZ);
	}
}
