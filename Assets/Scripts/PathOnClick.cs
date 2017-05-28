using UnityEngine;
using System.Collections;
using Pathfinding;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class PathOnClick : MonoBehaviour {

	/** Mask for the raycast placement */
	public LayerMask mask;
	
	public Transform target;
	
	Camera cam;
	
	public void Start () {
		//Cache the Main Camera
		cam = Camera.main;
//		ais2 = FindObjectsOfType(typeof(AIPath)) as AIPath[];
	}
	
	// Update is called once per frame
	void Update () {
		
		if (cam != null && Input.GetMouseButtonUp(1)) {
			UpdateTargetPosition ();
		}
		
	}
	
	public void UpdateTargetPosition () {
		//Fire a ray through the scene at the mouse position and place the target where it hits
		RaycastHit hit;
		if (Physics.Raycast	(cam.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, mask) && hit.point != target.position) {
			target.position = hit.point;
		}
	}
}