using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSpriteHolder : MonoBehaviour
{
		public Image imgComponent;
		public CharacterBase character;
		public MainNetwork copyOfMainNetwork;

		public void Setup ()
		{	
				GetComponent<Image> ().sprite = character.portrait;
		}
		public void Clicked ()
		{
				copyOfMainNetwork.CallChangeCharacter (GetComponent<Image> ().sprite.name, character.ToString (), Network.player.guid, copyOfMainNetwork.myPlayerSpot);
		}
}