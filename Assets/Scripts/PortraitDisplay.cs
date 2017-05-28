using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class PortraitDisplay : MonoBehaviour
{

		public StartingChar starChar;
		public Image portrait;
		public Text level;

		void Update ()
		{
				if (starChar == null) {
						starChar = GameObject.FindGameObjectWithTag ("Player").GetComponent<StartingChar> ();
				} else {
						portrait.sprite = starChar.characterPortrait;
						level.text = Stats.statInstance.level.ToString ();
				}
		}
}