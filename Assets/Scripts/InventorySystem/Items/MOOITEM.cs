using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
//This is for Item1 (Test)
[System.Serializable]
public class MOOITEM : Item
{

		public MOOITEM ()
		{
				itemName = "MOOITEM";
				name = "MOO Item";
				itemTexture = Resources.Load ("Items/Sword", typeof(Sprite)) as Sprite;
				isActive = false;
				oldActive = isActive;
				itemID = 1;
				itemStats = new Stats.basicStats ();
				activeKey = KeyCode.Q;
				abilityCooldown = 0;
				abilityMaxCooldown = 5;
				bought = false;
				recipePrice = 10;
				toolTip = "MOOOO Item \n + 50 Mana \n + 100 MaxMana \n + 10 AttackSpeed \n this is a new Item that is all test";
				activate = false;

				for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
						itemStats.Add (Stats.statInstance.numberOfStats [i].ToString (), 0);
				}
				itemStats ["Mana"] = 50;
				itemStats ["AttackSpeed"] = 10;
				itemStats ["MaxMana"] = 200;
				build = false;
				reqiure = true;
				requirements = new string[1]{"Test Item"};
		}
		public override void Activate () //Example of what to put in your code!!!
		{
				if (isActive != oldActive) {
						switch (isActive) {
						case true:
								{
										for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
												Stats.statInstance.finalStatsInspector [Stats.statInstance.numberOfStats [i].ToString ()]
                                                        +=
                            itemStats [Stats.statInstance.numberOfStats [i].ToString ()];
										}
										oldActive = isActive;
										break;
								}
						case false:
								{
										{
												for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
														Stats.statInstance.finalStatsInspector [Stats.statInstance.numberOfStats [i].ToString ()]
                                                            -=
                                itemStats [Stats.statInstance.numberOfStats [i].ToString ()];
												}
												oldActive = isActive;
												break;
										}
								}
						}
				}
				if (abilityCooldown > 0) {
						abilityCooldown -= Time.deltaTime;
				}
				if (activate || Input.GetKeyDown (activeKey)) {
						if (abilityCooldown <= 0) {
								Debug.Log ("Pressed");
								abilityCooldown = abilityMaxCooldown;
								activate = false;
						} else {
								activate = false;
						}
				}
		}
}