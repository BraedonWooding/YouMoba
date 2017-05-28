using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class EffectSystem : MonoBehaviour
{
		public List<Effect> currentEffects = new List<Effect> ();
		public List<Effect> appliedEffects = new List<Effect> ();
		public Stats statsCopy;
		public GameObject effectPrefab;

		public void Update ()
		{
				for (int i = 0; i < currentEffects.Count; i++) {
						currentEffects [i].effectApplied = true;
						Effect currentEffect = currentEffects [i];
						statsCopy.ApplyEffectDuration (currentEffect.effectDuration, currentEffect.effectStatEffecting, currentEffect.effectDmgFlat, currentEffect.effectDmgPercentage);
						appliedEffects.Add (currentEffects [i]);
						currentEffects.Remove (currentEffects [i]);
				}

				for (int i = 0; i < appliedEffects.Count; i++) {
						appliedEffects [i].effectDuration -= Time.deltaTime;
						if (appliedEffects [i].effectDuration < 0 && appliedEffects [i].effectDuration > -2) {
								appliedEffects.Remove (appliedEffects [i]);
						}
				}
		}

		public void OnHit (string enemyHitName)
		{
				for (int i = 0; i < Mathf.Max(currentEffects.Count, appliedEffects.Count); i++) {
						if (i < currentEffects.Count) {
								currentEffects [i].OnHitEffect (enemyHitName);
						}
						if (i < appliedEffects.Count) {
								appliedEffects [i].OnHitEffect (enemyHitName);
						}
				}
		}
}

[System.Serializable]
public class Effect
{
		public float effectFullDuration;
		public float effectDuration; // < -2 for infinite
		public float effectDmgFlat;
		public float effectDmgPercentage;
		public Stats.stat effectStatEffecting;
		public Sprite effectImage;

		public bool effectApplied;

		public Effect (float duration, float dmgFlat, float dmgPercentage, Stats.stat statEffecting, Sprite img)
		{
				effectFullDuration = duration;
				effectDuration = duration;
				effectDmgFlat = dmgFlat;
				effectDmgPercentage = dmgPercentage;
				effectStatEffecting = statEffecting;
				effectImage = img;
		}

		public virtual void OnHitEffect (string nameOfEnemy)
		{

		}
}