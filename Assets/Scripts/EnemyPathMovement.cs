/** Jonathan So, jonso.gamedev@gmail.com
 * Based on OctoManGames' "Not Galaga" tutorial. An enemy moves on a path and then is destroyed when reaching the end.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMovement : MonoBehaviour {

	// PUBLIC 
	public Path pathToFollow;
	public int currentWayPointID = 0;
	public float speed = 12; 
	public float reachDistance = 0.4f; // If we're this close to the waypoint, we've reached it and can move on to the next.
	public float rotationSpeed = 5f;
	public bool useBezier = false;

	// PRIVATE
	private float distance; // Current distance to the next waypoint. 

	private void Update () {
		MoveOnPath(pathToFollow);
	}

	void MoveOnPath(Path path) {
		if (useBezier) {
			// Movement
			distance = Vector3.Distance(path.bezierObjList[currentWayPointID], transform.position);
			transform.position = Vector3.MoveTowards(transform.position, path.bezierObjList[currentWayPointID], speed * Time.deltaTime);
		} else {
			// Movement
			distance = Vector3.Distance(path.pathObjList[currentWayPointID].position, transform.position);
			transform.position = Vector3.MoveTowards(transform.position, path.pathObjList[currentWayPointID].position, speed * Time.deltaTime);
		}
		if (useBezier) {
			if (distance <= reachDistance) { // Move to the next waypoint.
				currentWayPointID++;
			}
			if (currentWayPointID >= path.bezierObjList.Count) { // Reached the end of the path, deactivate ourselves
				this.gameObject.SetActive(false);
			}
		} else {
			if (distance <= reachDistance) { // Move to the next waypoint.
				currentWayPointID++;
			}
			if (currentWayPointID >= path.pathObjList.Count) { // Reached the end of the path, deactivate ourselves
				this.gameObject.SetActive(false);
			}
		}
	}
}
