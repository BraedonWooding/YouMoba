using UnityEngine;
using System.Collections;

public class BrainBile_Skin01 : CharacterBase
{
		
		public BrainBile_Skin01 ()
		{
				characterType = CharacterType.skin;
				portrait = Resources.Load ("Portrait/BrainBile_Skin01", typeof(Sprite)) as Sprite;
				modelPrefab = Resources.Load ("Models/BrainBile_Skin_01", typeof(GameObject)) as GameObject;
				lore = "Brain Re-constructed over a long time.";
				startingAudio = Resources.Load ("Sounds/BrainBile_Default_Load", typeof(AudioClip)) as AudioClip;
				characterName = "Brain Bile the pink";
				title = "The Deciever";
				characterID = 0;
		}
}