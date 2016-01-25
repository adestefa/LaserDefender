using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	void OnDrawGizmos() {

		// show blank object's position with new sphere
		Gizmos.DrawWireSphere (transform.position, 1);

	}
}
