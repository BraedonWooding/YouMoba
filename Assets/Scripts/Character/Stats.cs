using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

using AdvancedInspector;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[AdvancedInspector]
public class Stats : MonoBehaviour
{
		//You can add stats here and it should update however you will need to add at the bottom a line of code more instruction at the last command
		public enum stat
		{
				Health,
				Mana,
				HealthRegen,
				ManaRegen,
				AttackDamage,
				AbilityPower,
				AttackSpeed,
				MovementSpeed,
				MaxHealth,
				MaxMana,
				Gold,
				CurrentArmour, 
				MaxArmour,
				Experience,
				MaxExperience,
				UpgradePoints,
				AttackRange
		}

		public bool canMove, canAttack, canCast;
		public Transform tower;

		public enum TypeOfDamage
		{
				ArmourPiercing,
				Magical,
				Normal
		}
		public static Stats statInstance;
		[Inspect]
		public int
				level;

		void Awake ()
		{
				statInstance = this;
		}

		void Start ()
		{
				finalStatsInspector = this.GetComponent<StartingChar> ().startingStats;
		}

    #region Inspector Variables
	
		[Inspect]
		public float[]
				sendArray;
		[Inspect]
		public stat[]
				numberOfStats;

    #region Dictionary
		[Serializable]
		public class basicStats : UDictionary<string, float>
		{
		}
		[Inspect]
		public basicStats
				finalStatsInspector = new basicStats ();

    #endregion

    #endregion
        #region Set Get Functions
		//These are the functions for both setting and getting different aspects of the stat dictionaries

		//This function is the best way to send all the data across server

		public float[] sendStatData ()
		{
				return finalStatsInspector.Values.ToArray ();
		}

		//Function for setting stat

		public void SetStat (string statType, float amount)
		{
				finalStatsInspector [statType] = amount;
		}

		//Function for adding/removing a value from stat
	
		public void IncrementStat (string statType, float amount)
		{
				finalStatsInspector [statType] += amount;

		}

		public void IncrementPercentageStat (float amount, string statType)
		{
				finalStatsInspector [statType] *= amount;
		}
		//Function to get a stat

		public float GetStat (string statType)
		{
				return finalStatsInspector [statType];
		}

		public void ApplyDamage (float amount, string playerName, TypeOfDamage dam = TypeOfDamage.Normal)
		{
				if (dam == TypeOfDamage.Magical || dam == TypeOfDamage.ArmourPiercing || GetStat ("CurrentArmour") <= 0) {
						IncrementStat ("Health", -amount);
				} else {
						if (dam == TypeOfDamage.Normal && GetStat ("CurrentArmour") > 0) {
								if (GetStat ("CurrentArmour") - amount >= 0) {
										IncrementStat ("CurrentArmour", -amount);
								} else {
										float am = amount - GetStat ("CurrentArmour"); //Get how much extra
										SetStat ("CurrentArmour", 0);
										IncrementStat ("Health", -am);
								}
						}
				}
				if (tower != null) {
						tower.SendMessage ("DamageDealt", playerName);
				}
		}

    #region extraStats

		public void SetLevel (int amount)
		{

				level = amount;

		}

		public void IncrementLevel (int amount)
		{

				level += amount;

		}

		public int GetLevel ()
		{

				return level;

		}

    #endregion

    #endregion


    #region Apply Effects

		public IEnumerator ApplyEffectDuration (float length, stat statToApply, float amount, float amountPercentage)
		{
				float before = finalStatsInspector [statToApply.ToString ()];
				ApplyEffect (statToApply, amount, amountPercentage);
				yield return new WaitForSeconds (length);
				if (amount > 0) {
						RemoveEffect (amount, before * amountPercentage, statToApply.ToString ());
				}
		}

		public void ApplyEffect (stat statToApply, float amount, float amountPercentage)
		{
				if (amount > 0) {
						IncrementStat (statToApply.ToString (), amount);
				}
				if (amountPercentage > 0) {
						IncrementPercentageStat (amountPercentage, statToApply.ToString ());
				}
		}

		public void RemoveEffect (float amount, float amountPercentage, string statType)
		{
				if (amount > 0) {
						finalStatsInspector [statType] -= amount;
				}
				if (amountPercentage > 0) {
						finalStatsInspector [statType] -= amountPercentage;
				}
		}

		public void ResetLevel ()
		{
				level = 0;
		}

    #endregion
	
		void OnCollisionEnter (Collision col)
		{
				if (col.gameObject.name.Contains ("Tower") && col.gameObject.tag == this.tag) {
						tower = col.transform;
				}
		}

		void OnCollsionExit (Collision col)
		{
				if (col.gameObject.name.Contains ("Tower") && col.gameObject.tag == this.tag) {
						tower = null;
				}
		}


    #region Inspector Functions

		public void Initialize (UDictionary<string, float> Dict, string name)
		{
				//Just so warnings are displayed
				Debug.LogWarning ("RESETTING " + name + "!!!!");
				Dict.Clear ();
				//		Health, Mana, HealthRegen, ManaRegen, AttackDamage, AbilityPower, AttackSpeed, MovementSpeed
				for (int i = 0; i < numberOfStats.Length; i++) {
						Dict.Add (numberOfStats [i].ToString (), 0);
				}
		}

		[Inspect, Style("ToolbarButton")]
		public void InitializeAll ()
		{
				Initialize (finalStatsInspector, "Final Stats");
				ResetLevel ();
		}
    #endregion
}