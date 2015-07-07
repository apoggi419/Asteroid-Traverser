using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float enemySpeed;
	public GameObject enemy;
	public GameObject enemyShot;
	public Transform enemyShotSpawn;
	public float fireRate;
	private float nextFire;

	void Update(){
		if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (enemyShot, enemyShotSpawn.position, enemyShotSpawn.rotation);
				audio.Play ();
		}
	}
}
