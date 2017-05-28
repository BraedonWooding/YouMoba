using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterBase
{
		public enum CharacterType
		{
				skin,
				character
		}

		public CharacterType characterType;
		public Sprite portrait;
		public GameObject modelPrefab;
		public string lore;
		public AudioClip startingAudio;
		public string characterName;
		public string title;
		public int characterID = -1;

		public CharacterBase ()
		{
				characterID = -1;
		}

		public virtual void AddScript (GameObject gameObjectToAddToo)
		{
				gameObjectToAddToo.AddComponent<Default> ();
		}
}