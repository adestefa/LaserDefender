using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	private bool movingRight = true;
	public float speed = 5f;
	private float xmax;
	private float xmin;
	// Use this for initialization
	void Start () {

		// find all child Position objects in EnemyFormation object
		foreach (Transform child in transform) {
			float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
			Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distanceToCamera));
			Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distanceToCamera));
			xmax = rightEdge.x;
			xmin = leftEdge.x;

			// spawn enemy into target child position
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;

			// we want each enemy to spawn inside the "EnemySpawner" object.
			// Since we attached this script to that object, let us set the new 
			// enemy to "this" transform. 
			enemy.transform.parent = child.transform;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	}

	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}  else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		//find right edge of formation using half width (pivot is in center)
		float rightEdgeOfFormation = transform.position.x + (width/2); 

		// find left edge of formation using half width (pivot is in center)
		float leftEdgeOfFormation = transform.position.x - (width/2);

		//Debug.Log ("lef:" + leftEdgeOfFormation + " Rig:" + rightEdgeOfFormation);
		//Debug.Log ("min:" + xmin + " max:" + xmax);

		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {

			movingRight = false;
		}



  }
}
