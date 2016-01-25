using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float speed = 5.0f;
	public float padding = 0f;
	public GameObject projectile;
	public float projectileSpeed = 0.1f;
	public float firingRate = 0.2f;
	public float health = 250f;

	float xmin = -5;
	float xmax = 5;

	void Fire() {
		Vector3 offSet = new Vector3 (0, 1, 0);
		GameObject beam = Instantiate(projectile, transform.position+offSet, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,projectileSpeed,0);
	}

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));

		xmin = leftMost.x + padding;
		xmax = rightMost.x -  padding;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
		
			transform.position += Vector3.left * speed * Time.deltaTime;
		
		} else if (Input.GetKey (KeyCode.RightArrow)) {

			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		
	}




	void OnTriggerEnter2D(Collider2D col) {
		
		// dynamically link projectile
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			Debug.Log ("Player Collided with missile");
			health -= missile.GetDamage ();
			//Debug.Log ("Hit by a projectile, h:"+health);
			if (health <= 0) {
				Destroy(this.gameObject);
			}
			missile.Hit (); // destroy the missile too
		}
	}
}
