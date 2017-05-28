using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class Ability1 : Ability
{
		//This is for my creating prefab
		public GameObject go;

		public Ability1 ()
		{
				//this is so their is no dragging!  Great tool and you have to do this since you can't drag using these scripts!
				go = Resources.Load ("Abilities/Prefab", typeof(GameObject)) as GameObject;
				//Give it a name
				name = "AbilityTest";
				abilityStats = new Stats.basicStats ();
				//Setting the cooldown to 0 (set it to whatever you want) by default should be 0
				abilityCooldown = 0;
				//And the max cooldown is what it says
				abilityMaxCooldown = 3f;
				//Ability ID is important which is why the script should be named as the ability ID
				abilityId = 1;
				//The ability Key (later on we will have the player changeable keys)
				abilityKey = KeyCode.W;
				//The default level (1 = available at start)
				abilityLevel = 1;
				abilityActiveStats = new Stats.basicStats ();
				//The same as the created object but this is for the texture of the sprite
				abilityTexture = Resources.Load ("Abilities/Ability1", typeof(Sprite)) as Sprite;
				//The tooltip (you will need to include every detail
				toolTip = "AbilityTest";
				//MAKE SURE THIS IS FALSE!!! (VERY IMPORTANT)!!
				activate = false;
				levelRequired = 1;
				for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
						abilityStats.Add (Stats.statInstance.numberOfStats [i].ToString (), 0);
				}
		}

		public override void LevelBonus (int level)
		{
				levelApplied = level;
				Debug.Log (level);
				abilityLevel++;
		}

		public override void Activate ()
		{
				//This is what you should have your section look like (the activate is for when you click on the ability!
				if (Input.GetKeyDown (abilityKey) || activate && Stats.statInstance.canCast) {
						//Your cooldown shouldn't be less then 0 but just incase if the numbers muck up!
						if (abilityCooldown <= 0) {
								GameObject bo = (GameObject)MonoBehaviour.Instantiate (go);
								bo.name = "Debugging";
								abilityCooldown = abilityMaxCooldown;
								activate = false;
						} else {
								activate = false;
						}
				}
				//This just is simple and just reduces cooldown if you have more than a 0 cooldown!
				if (abilityCooldown > 0) {
						abilityCooldown -= Time.deltaTime;
				}
		}
}