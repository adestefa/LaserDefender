using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 150f;

	public float shotsPerSeconds = 0.5f;



	void Update() {
		// p(fire per frame) = time x frequency
		float probabilty = Time.deltaTime * shotsPerSeconds;
		// Convert a probability into a decision.
		// 0.8 this will be true around 80% per second
		if (Random.value < probabilty) { // has to be between 0 and 1
			Fire ();
		}


	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject missile = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D col) {
		// dynamically link projectile
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			//Debug.Log ("Hit by a projectile, h:"+health);
			if(health <= 0) {
				Destroy(this.gameObject);
			}
			missile.Hit(); // destroy the missile too
		}



		// old way usig tags
		//if (col.tag == "PlayerProjectile") {
		//	Destroy (this.gameObject);
		//	Debug.Log ("hit dude");
		//}
	}
}
