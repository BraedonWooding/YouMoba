using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuffDisplay : MonoBehaviour
{
		public Effect effectToDisplay;
		public Image imgFill;
		public Image img;
		bool startedUp = false;

		void RunStartup ()
		{
				Image[] imgs = GetComponentsInChildren<Image> ();
				img = imgs [0];
				imgFill = imgs [1];
				startedUp = true;
		}

		void Update ()
		{
				if (startedUp) {
						img.sprite = effectToDisplay.effectImage;
						if (effectToDisplay.effectDuration == -2) 
								imgFill.fillAmount = 1;
						else {
								effectToDisplay.effectDuration -= Time.deltaTime;
								imgFill.fillAmount = effectToDisplay.effectDuration / effectToDisplay.effectFullDuration;
						}
						if (effectToDisplay.effectDuration <= 0 && effectToDisplay.effectDuration > -2) {
								Destroy (this.gameObject);
						}
				}
		}
}