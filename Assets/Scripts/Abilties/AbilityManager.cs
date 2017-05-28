using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class AbilityManager : MonoBehaviour
{

		public Transform[] abilityPanels = new Transform[6];
		public Ability[] ourAbilities = new Ability[6];
		public static AbilityManager abilityInstance;
		public static int numberOfAbilities = 0;
		public int unusedPoints;
		private Transform[] upgradeButtons;

		void Awake ()
		{
				upgradeButtons = new Transform[5];
				abilityInstance = this;
				for (int i = 0; i < abilityPanels.Length; i++) {
						abilityPanels [i] = GameObject.Find ("Ability" + i).transform;
				}
				for (int i = 0; i < upgradeButtons.Length; i++) {
						upgradeButtons [i] = GameObject.Find ("UpgradeButtons" + i).transform;
				}
				ActivateButtons (false);
		}

		void Start ()
		{
				AddAbility (new Ability1 ());
		}

		public void AddAbility (Ability ability)
		{
				if (numberOfAbilities < ourAbilities.Length) {
						ourAbilities [numberOfAbilities] = ability;
						ability.abilityManagerId = numberOfAbilities;
						numberOfAbilities++;
				}
		}

		void Update ()
		{
				for (int i = 0; i < abilityPanels.Length; i++) {
						abilityPanels [i].GetComponent<AbilityData> ().currentAbility = ourAbilities [i];
				}
				for (int i = 0; i < ourAbilities.Length; i++) {
						ourAbilities [i].Activate ();
				}
				if (unusedPoints > 0) {
						ActivateButtons (true);
				}

				#region Experience

				#endregion
		}
		public void ActivateButtons (bool value)
		{
				for (int i = 0; i < upgradeButtons.Length; i++) {
						if (value) {
								AbilityData data = abilityPanels [i].GetComponent<AbilityData> ();
								if (data.currentAbility.abilityTexture != null && data.currentAbility.levelRequired <= Stats.statInstance.level) {
										upgradeButtons [i].gameObject.SetActive (value);
								}
						} else
								upgradeButtons [i].gameObject.SetActive (value);
				}
		}
}