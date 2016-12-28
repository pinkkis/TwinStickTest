using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var exp = GetComponent<ParticleSystem>();
		var sound = GetComponent<AudioSource>();

		sound.Play();
		exp.Play();
		Destroy(gameObject, exp.duration);
	}
}
