using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[System.Serializable]
public class Ability
{
		//The image texture
		public Sprite abilityTexture;
		//The ability ID pls name the ability the id for simplicty
		public int abilityId;
		//The name this is what the ability will be called (spaces and capitals are fine)
		public string name;
		//The stats the ability gives you (flat bonuses only!)
		public Stats.basicStats abilityStats;
		//The ability cooldown is the current cooldown (it allows the ability to work when it is 0 though you have to make this) The max is the maxcooldown you will have to include CD reduction
		public float abilityCooldown, abilityMaxCooldown;
		//The ability manager ID (which slot it is in) don't touch this
		public int abilityManagerId;
		//The key to activate (you will handle activating keys so don't worry if this is blank if its a passive)
		public KeyCode abilityKey;
		//The stats the item gives you when you activate it (you will handle this)
		public Stats.basicStats abilityActiveStats;
		//The level that the ability is currently at (if you do this at start remember to do level bonus too!)
		public int abilityLevel;
		//The tooltip that is displayed
		public string toolTip;
		//For clicking on the ablity
		public bool activate;
		//The booleans is to determine when you have used the effects so we don't keep adding them 
		public bool effectsActive, oldActive;
		//The current level that is applied just if you added 3 levels at once you could loop through without problems
		public int levelApplied;
		//The level required for actions
		public int levelRequired;

		//This is called every second (Your update function)
		public virtual void Activate ()
		{
				return;
		}
		//This is for when you want to apply a level bonus!
		public virtual void LevelBonus (int level)
		{
				return;
		}
}