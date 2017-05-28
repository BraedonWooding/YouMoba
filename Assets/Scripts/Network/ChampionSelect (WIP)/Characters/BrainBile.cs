using UnityEngine;
using System.Collections;

public class BrainBile : CharacterBase
{
		
		public BrainBile ()
		{
				characterType = CharacterType.character;
				portrait = Resources.Load ("Portrait/BrainBile_Default", typeof(Sprite)) as Sprite;
				modelPrefab = Resources.Load ("Models/BrainBile_Default", typeof(GameObject)) as GameObject;
				;
				lore = "Brain Re-constructed over a long time.";
				startingAudio = Resources.Load ("Sounds/BrainBile_Default_Load", typeof(AudioClip)) as AudioClip;
				characterName = "Brain Bile";
				title = "The Deciever";
				characterID = 0;
		}
}