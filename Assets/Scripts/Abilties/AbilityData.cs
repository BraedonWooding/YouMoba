using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class AbilityData : MonoBehaviour
{

		public Ability currentAbility;
		public Image[] spriteHolder;

		void Awake ()
		{
				spriteHolder = GetComponentsInChildren<Image> ();
		}

		void Update ()
		{
				if (currentAbility.abilityTexture == null) {
						spriteHolder [1].gameObject.SetActive (false);
				} else if (currentAbility.abilityTexture != null) {
						spriteHolder [1].gameObject.SetActive (true);
						spriteHolder [1].sprite = currentAbility.abilityTexture;
				}
				if (currentAbility.abilityKey != KeyCode.None) {
						GetComponentInChildren<Text> ().text = currentAbility.abilityKey.ToString ();
				}
				if (currentAbility.abilityCooldown > 0) {
						GetComponentInChildren<Text> ().text += "  " + currentAbility.abilityCooldown.ToString (".0");
				}
				if (currentAbility.levelApplied > 0) {
						for (int i = 0; i < currentAbility.levelApplied; i++) {
								spriteHolder [i + 2].color = Color.green;
						}
				}
		}

		public void Press ()
		{
				currentAbility.activate = true;
		}

		public void LevelAdd ()
		{
				if (AbilityManager.abilityInstance.unusedPoints > 0) {
						currentAbility.LevelBonus (currentAbility.levelApplied + 1);
						AbilityManager.abilityInstance.unusedPoints--;
						if (AbilityManager.abilityInstance.unusedPoints == 0) {
								AbilityManager.abilityInstance.ActivateButtons (false);
						}
				}
		}
}