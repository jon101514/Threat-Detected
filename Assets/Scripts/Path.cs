/** Jonathan So, jonso.gamedev@gmail.com
 * Based on OctoManGames' "Not Galaga" tutorial. Defines a path for enemies to move on.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public Color pathColor = Color.green;

	private Transform[] objArray;

	public bool visualizePath = false;
	[Range(1, 20)] public int lineDensity = 1; 
	public List<Transform> pathObjList = new List<Transform>();

	public List<Vector3> bezierObjList = new List<Vector3>();

	private int overload;

	// Upon startup, make our path.
	void Start () {
		CreatePath();
	}

	// NOTE: This will only work if the Scene view is visible due to the Gizmo system.
	void OnDrawGizmos() {
		if (visualizePath) {
			///////////////////
			// Straight Path //
			///////////////////
			Gizmos.color = pathColor;
			// Fill the array
			objArray = GetComponentsInChildren<Transform>();
			// Clear the list.
			pathObjList.Clear();
			// Put all children into list
			foreach (Transform obj in objArray) {
				if (obj != this.transform) {
					pathObjList.Add(obj);
				}
			}
			// Draw the path using lines and spheres.
			for (int i = 0; i < pathObjList.Count; i++) {
				Vector3 pos = pathObjList[i].position;
				if (i > 0) { // Do not take the first point in the list due to the i - 1 part.
					Vector3 previous = pathObjList[i - 1].position;
					Gizmos.DrawLine(previous, pos);
					Gizmos.DrawWireSphere(pos, 0.3f);
				}
			}

			/////////////////
			// Curved Path //
			/////////////////
			// Check Overload
			if (pathObjList.Count > 0) {
				if (pathObjList.Count % 2 == 0) { // Even case
					pathObjList.Add(pathObjList[pathObjList.Count - 1]); // Duplicate the final point
					overload = 2;			
				} else {	// Uneven
					pathObjList.Add(pathObjList[pathObjList.Count - 1]); // Duplicate the final point... twice.
					pathObjList.Add(pathObjList[pathObjList.Count - 1]); 
					overload = 3;
				}
			
				// Curve Creation
				bezierObjList.Clear(); // Check Overload
				Vector3 lineStart = pathObjList[0].position;
				// Note: We add 2 instead of 1 here because the point in between p1 and p2 is the "handle".
				for (int i = 0; i < pathObjList.Count - overload; i += 2) {
					for (int j = 0; j <= lineDensity; j++) {
						Vector3 lineEnd = GetPoint(pathObjList[i].position, pathObjList[i + 1].position, pathObjList[i + 2].position, j / (float) lineDensity);
						Gizmos.color = Color.red; // Draw the line in red.
						Gizmos.DrawLine(lineStart, lineEnd);
						Gizmos.color = Color.blue; // Draw a blue ball.
						Gizmos.DrawWireSphere(lineStart, 0.1f);
						lineStart = lineEnd; // Make the next linestart this current lineEnd.
						bezierObjList.Add(lineStart); // Add the linestart position so we can actually use it in a practical way.
					}
				}
			}
		}
	}

	// Returns a point on a Quadratic Bezier Curve specified by 3 points.
	// [p0, p1, p2] - points used for the quadratic curve.
	// [t] - time used for interpolation.
	Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
	}

	// Make the path by adding points to the path object list (or bezier object list).
	private void CreatePath() {
		////////////////
		// Straight Path
		////////////////

		// Fill the array
		objArray = GetComponentsInChildren<Transform>();
		// Clear the list.
		pathObjList.Clear();
		// Put all children into list
		foreach (Transform obj in objArray) {
			if (obj != this.transform) {
				pathObjList.Add(obj);
			}
		}
		////////////////
		// Curved Path
		////////////////
		// Check Overload
		if (pathObjList.Count % 2 == 0) { // Even case
			pathObjList.Add(pathObjList[pathObjList.Count - 1]); // Duplicate the final point
			overload = 2;			
		} else {						// Uneven case
			pathObjList.Add(pathObjList[pathObjList.Count - 1]); // Duplicate the final point... twice.
			pathObjList.Add(pathObjList[pathObjList.Count - 1]); 
			overload = 3;
		}
		// Curve Creation
		bezierObjList.Clear(); // Check Overload
		Vector3 lineStart = pathObjList[0].position;
		// Note: We add 2 instead of 1 here because the point in between p1 and p2 is the "handle".
		for (int i = 0; i < pathObjList.Count - overload; i += 2) {
			for (int j = 0; j <= lineDensity; j++) {
				Vector3 lineEnd = GetPoint(pathObjList[i].position, pathObjList[i + 1].position, pathObjList[i + 2].position, j / (float) lineDensity);
				lineStart = lineEnd; // Make the next linestart this current lineEnd.
				bezierObjList.Add(lineStart); // Add the linestart position so we can actually use it in a practical way.
			}
		}
	}
}
