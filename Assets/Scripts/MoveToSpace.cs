using UnityEngine;
using System.Collections;
using Pathfinding;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class MoveToSpace : MonoBehaviour
{
		private Seeker seeker;
		public Path path;
		public Stats copyOfStats;
		private int currentWaypoint = 0;
		public CharacterController controller;
		float maxWaypointDistance = 5f;
		public GameObject currentSelected;
		Vector3 lastPos;

		void Start ()
		{
				seeker = GetComponent<Seeker> ();
				controller = GetComponent<CharacterController> ();
				copyOfStats = GetComponent<Stats> ();
		}

		public void LateUpdate ()
		{
				if (Input.GetMouseButtonDown (1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
			
						if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
								if (hit.transform.GetComponent<Stats> () != null && hit.transform.tag != "Ally") {
										//If the object is selectable
										//currentSelected.GetComponent<Stats>().notSelected = false;

										currentSelected = hit.transform.gameObject;
										seeker.StartPath (transform.position, hit.point, OnPathComplete);

								} else if (!hit.transform.name.Equals (this.name)) { //Fix this later to a layer of players
										currentSelected = null;
										seeker.StartPath (transform.position, hit.point, OnPathComplete);
								}
						}
				}
		}

		public void FixedUpdate ()
		{
				if (currentSelected != null && (lastPos != currentSelected.transform.position)) {
						seeker.StartPath (transform.position, currentSelected.transform.position, OnPathComplete);
				}

				if (path == null)
						return;

				if (currentWaypoint >= path.vectorPath.Count || (currentSelected != null && Vector3.Distance (transform.position, path.vectorPath [path.vectorPath.Count - 1]) < this.GetComponent<Stats> ().finalStatsInspector ["AttackRange"])) {
						return;
				}

				Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized * copyOfStats.GetStat ("MovementSpeed");
				controller.SimpleMove (dir);
		
				if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < maxWaypointDistance) {
						currentWaypoint++;
				}
		}

		public void OnPathComplete (Path p)
		{
				if (!p.error) {
						path = p;
						currentWaypoint = 0;
				} else {
						Debug.Log (p.error);
				}
				if (currentSelected != null) {
						lastPos = currentSelected.transform.position;
				}
		}
}