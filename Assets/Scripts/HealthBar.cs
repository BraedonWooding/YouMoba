using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class HealthBar : MonoBehaviour
{

		public List<Argument> arguments = new List<Argument> ();
		public Text txtHealth;
		public Image healthBar;
		public Image outline;
		public Mask mask;
		public float HP;
		public float maxHP;
		public float divideRange;
		public GameObject dead;
		public bool canDie;
		public string textHealth = "HP: ";
		public bool Y;
		public Stats.stat[] stats;

		public void Update ()
		{
				HP = Stats.statInstance.finalStatsInspector [stats [0].ToString ()];
				maxHP = Stats.statInstance.finalStatsInspector [stats [1].ToString ()];
				divideRange = HP / maxHP;
				txtHealth.text = textHealth + HP + "/" + maxHP;
				if (!Y && divideRange > 0 && divideRange < Mathf.Infinity) {
						mask.rectTransform.localScale = new Vector3 (divideRange, 1, 1);
				} else if (Y && divideRange > 0 && divideRange < Mathf.Infinity) {
						mask.rectTransform.localScale = new Vector3 (1, divideRange, 1);
				}
				if (arguments.Count > 0) {
						for (int i = 0; i < arguments.Count; i++) {
								if (!arguments [i].and) {
										switch (arguments [i].sign) {
										case "<":
												if (HP < arguments [i].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case ">":
												if (HP > arguments [i].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case "=":
												if (HP == arguments [i].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case ">=":
												if (HP >= arguments [i].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case "<=":
												if (HP <= arguments [i].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										}
								} else if (arguments [i].and) {
										switch (arguments [i].sign) {
										case "<":
												if (HP < arguments [i].Amount * maxHP && HP >= arguments [arguments [i].andV].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case ">":
												if (HP > arguments [i].Amount * maxHP && HP <= arguments [arguments [i].andV].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case "=":
												Debug.LogError ("You can't just have a =");
												break;
										case ">=":
												if (HP < arguments [i].Amount * maxHP && HP >= arguments [arguments [i].andV].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										case "<=":
												if (HP > arguments [i].Amount * maxHP && HP <= arguments [arguments [i].andV].Amount * maxHP) {
														if (arguments [i].signs != null)
																healthBar.sprite = arguments [i].signs;
												}
												break;
										}
								}

						}
				}
				if (canDie) {
						if (HP <= 0) {
								dead.SetActive (true);
						} else {
								dead.SetActive (false);
						}
				}

		}
}
