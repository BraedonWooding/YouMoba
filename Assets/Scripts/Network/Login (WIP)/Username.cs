using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class Username : MonoBehaviour
{
		public static string username;
		InputField field;
		static bool created = false;
		public List<CharacterBase> charactersToSpawn = new List<CharacterBase> ();
		GameObject[] greenSide = new GameObject[6];
		GameObject[] yellowSide = new GameObject[6];
		public CharacterBase personalCharacter;

		void Start ()
		{
				if (!created) {
						created = true;
				} else {
						Destroy (this);
				}
				username = PlayerPrefs.GetString ("Username");
				field = GameObject.Find ("Input_Username").GetComponent<UnityEngine.UI.InputField> ();
				field.ActivateInputField ();
				field.text = username;
				field.DeactivateInputField ();
				DontDestroyOnLoad (this.gameObject);
		}

		void OnApplicationQuit ()
		{
				PlayerPrefs.SetString ("Username", username);
		}

		public void OnLevelWasLoaded (int level)
		{
				//Do: greenSide = GameObject.Find("WhateverYourGreenSideIsCalled");
				//Do: yellowSide = GameObject.Find("WhateverYourYellowSideIsCalled");
				if (level == 2) {
						int i = 0;
						foreach (Transform child in GameObject.Find("SpotsToSpawnYellow").transform) {
								yellowSide [i] = child.gameObject;
								i++;
						}
						i = 0;
						foreach (Transform child in GameObject.Find("SpotsToSpawnGreen").transform) {
								greenSide [i] = child.gameObject;
								i++;
						}
						int g = 0;
						int y = 0;
						for (i = 0; i < charactersToSpawn.Count; i++) {
								GameObject go = (GameObject)Instantiate (charactersToSpawn [i].modelPrefab);
								if (i % 2 == 0) {
										go.transform.SetParent (greenSide [g].transform); //Spawn Green
										go.transform.position = new Vector3 (greenSide [g].transform.position.x, greenSide [g].transform.position.y + 8, greenSide [g].transform.position.z);
										go.transform.localScale = new Vector3 (1, 1, 1);
										g++;
								} else {
										go.transform.SetParent (yellowSide [y].transform); //Spawn Yellow
										go.transform.position = new Vector3 (yellowSide [y].transform.position.x, yellowSide [y].transform.position.y + 8, yellowSide [y].transform.position.z);
										go.transform.localScale = new Vector3 (1, 1, 1);
										y++;
								}
								go.AddComponent <Stats> ();
								if (personalCharacter.portrait == charactersToSpawn [i].portrait) {
										personalCharacter.AddScript (go);
										go.AddComponent <MoveToSpace> ();
								}
						}
				}
		}
}