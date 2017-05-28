using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class MiniMapCameraRadius : MonoBehaviour
{
		public Camera miniMapCamera;
		public Camera mainCamera;
		public GameObject mapHolder;
		public LayerMask layerMask;
		public bool mouseEntered { get; set; }

		void Update ()
		{
				if (mouseEntered) {
						if (Input.GetMouseButton (0)) {
								RaycastHit hit;
								Ray ray = miniMapCamera.ScreenPointToRay (Input.mousePosition);
				
								// Trace ray from minimap viewport, ignoring everything except the ground
								if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
										Vector3 miniMapPosition = hit.point;
										Vector3 camViewCenter;
										RaycastHit cameraView;
										Vector3 camDestPos;
					
										// Project a ray from the center of the main camera to find current world position
										Ray cameraCenter = mainCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f));
										Physics.Raycast (cameraCenter, out cameraView, Mathf.Infinity, layerMask);
										camViewCenter = cameraView.point;
					
										// Calculate change to move from current position to new minimap location                    
										camDestPos = miniMapPosition - camViewCenter;
										camDestPos.y = 0;          // maintain current height
										mapHolder.transform.position += camDestPos;
								}
						}
				}
				this.transform.position = mapHolder.transform.position;
				this.transform.position = new Vector3 (this.transform.position.x, 50, this.transform.position.z);
		}
}