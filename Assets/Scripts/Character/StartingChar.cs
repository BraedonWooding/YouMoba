using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using AdvancedInspector;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
//THIS PIECE OF CODE USES THE ADVANCED INSPECTOR!!!
[AdvancedInspector]
public class StartingChar : MonoBehaviour
{

	#region Inspector Values

		[Inspect]
		public string
				charName;
		[Inspect]
		public int
				charId;
		//Dictionary
		[Inspect]
		public Stats.basicStats
				startingStats = new Stats.basicStats ();
		[Inspect]
		public Sprite
				characterPortrait;
		[Inspect]
		public Stats
				stat;

	#endregion

		[Inspect, Style("ToolbarButton")]
		public void Startup ()
		{
				//Debug.LogError ("Characters Starting Stats Data is being deleted");
				stat.Initialize (startingStats, "Starting Stats");
		}

		//To be used when you want to code in starting values
		public void CodeStartingValues (float[] values)
		{ //The values HAVE to be in the following order of stats (Health, Mana, HealthRegen, ManaRegen, AttackDamage, AbilityPower, AttackSpeed, MovementSpeed)
				for (int i = 0; i < Stats.statInstance.numberOfStats.Length; i++) {
						startingStats [Stats.statInstance.numberOfStats [i].ToString ()] = values [i];
				}
		}
}