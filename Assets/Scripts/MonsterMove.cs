using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class MonsterMove : MonoBehaviour
{

		private List<Vector3> destination = new List<Vector3> ();
		public GameObject[] gameobjects;
		private Seeker seeker;
		private CharacterController controller;

		//The calculated path
		public Path path;
	
		//The AI's speed per second
		public float speed = 10;
	
		//The max distance from the AI to a waypoint for it to continue to the next waypoint
		public float nextWaypointDistance = 3;
	
		//The waypoint we are currently moving towards
		private int currentWaypoint = 0;
	
		public void Start ()
		{
				for (int i = 0; i < gameobjects.Length; i++) {
						destination.Add (gameobjects [i].transform.position);
				}

				seeker = GetComponent<Seeker> ();
				controller = GetComponent<CharacterController> ();
		
				seeker.StartPath (transform.position, destination [destination.Count - 1], OnPathComplete);
		}
	
		public void OnPathComplete (Path p)
		{
				if (!p.error) {
						path = p;
						path.vectorPath = destination;

						currentWaypoint = 0;
				}
		}
	
		public void FixedUpdate ()
		{
				if (path == null) {
						return;
				}
		
				if (currentWaypoint >= path.vectorPath.Count) {
						return;
				}
		
				//Direction to the next waypoint
				Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
				dir *= speed;
				controller.SimpleMove (dir);
		
				//Check if we are close enough to the next waypoint
				//If we are, proceed to follow the next waypoint
				if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
						currentWaypoint++;
						return;
				}
		}
		//Cause our monsters will be destroyed this way we don't have errors and stuff
		public void OnDisable ()
		{
				seeker.pathCallback -= OnPathComplete;
		} 
}