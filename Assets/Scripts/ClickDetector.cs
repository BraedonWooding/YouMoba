using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class ClickDetector : MonoBehaviour
{
    public bool HandleLeftClick = true;
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;
    public string OnLeftClickMethodName = "OnLeftClick";
    public string OnRightClickMethodName = "OnRightClick";
    public string OnMiddleClickMethodName = "OnMiddleClick";
	public EventSystem eventsystem;
	public bool mouseEntered { get; set; }
	public bool mouseCopy;
	
    void Update()
    { 
		mouseCopy = mouseEntered;	
				if (eventsystem == null) {
						eventsystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
				}
				if (mouseEntered) {
						// Left click
						if (HandleLeftClick && Input.GetMouseButtonDown (0)) {
								this.SendMessage (OnLeftClickMethodName, null, SendMessageOptions.DontRequireReceiver);
						}
 
						// Right click
						if (HandleRightClick && Input.GetMouseButtonDown (1)) {
								this.SendMessage (OnRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);
						}
 
						// Middle click
						if (HandleMiddleClick && Input.GetMouseButtonDown (2)) {
								this.SendMessage (OnMiddleClickMethodName, null, SendMessageOptions.DontRequireReceiver);
						}
				}
		}
}