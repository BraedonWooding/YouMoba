using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
//This is for Item1 (Test)
[System.Serializable]
public class TestItem : Item
{

		public TestItem ()
		{
				itemName = "TestItem";
				name = "Test Item";
				itemTexture = Resources.Load ("Items/Item1", typeof(Sprite)) as Sprite;
				isActive = false;
				itemID = 0;
				oldActive = isActive;
				itemStats = new Stats.basicStats ();
				activeKey = KeyCode.Q;
				abilityCooldown = 0;
				abilityMaxCooldown = 5;
				bought = false;
				recipePrice = 10;
				toolTip = "Test Item \n + 10 Health \n + 20 Mana \n + 100 Max Health \n + 200 MaxMana \n this is a new Item that is all test";
				activate = false;

				for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
						itemStats.Add (Stats.statInstance.numberOfStats [i].ToString (), 0);
				}
				itemStats ["Health"] = 10;
				itemStats ["Mana"] = 20;
				itemStats ["MaxHealth"] = 100;
				itemStats ["MaxMana"] = 200;
				build = true;
				buildInto = new Item[1];
				buildIntoNames = new string[1]{"MOO Item"};
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