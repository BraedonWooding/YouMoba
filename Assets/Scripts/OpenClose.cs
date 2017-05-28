using UnityEngine;
using System.Collections;
using System;
using AdvancedInspector;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[AdvancedInspector(InspectDefaultItems = true)]
public class OpenClose : MonoBehaviour {

	[Inspect]
	public EnclosedOpenClose[] OpenCloseArray;

	void Update () {

		for (int i = 0; i < OpenCloseArray.Length; i++) {
			if (Input.GetKeyDown (OpenCloseArray[i].closeOpenButton)) {
				OpenCloseArray[i].closeOpenObject.SetActive(!OpenCloseArray[i].closeOpenObject.activeSelf);
			}
	}
}
[Serializable]
[AdvancedInspector]
public class EnclosedOpenClose {

	[Inspect]
	public KeyCode closeOpenButton;
	[Inspect]
	public GameObject closeOpenObject;

	public EnclosedOpenClose () {

		}
	public EnclosedOpenClose (KeyCode button, GameObject closeObject) {

		closeOpenButton = button;
		closeOpenObject = closeObject;
	}
}
}