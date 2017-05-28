using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class DetectMouseOver : MonoBehaviour
{
		public GameObject toolTip;
		public int shopItemAbility; //0 = shop, 1 = item, 2 = ability
		ShopData sData = null;
		ItemData iData = null;
		AbilityData aData = null;

		void Awake ()
		{
				toolTip = GameObject.Find ("ToolTip");
				switch (shopItemAbility) {
				case 0:
						sData = this.gameObject.GetComponent<ShopData> ();
						break;
				case 1:
						iData = this.gameObject.GetComponent<ItemData> ();
						break;
				case 2:
						aData = this.gameObject.GetComponent<AbilityData> ();
						break;
				}
		}

		void Start ()
		{
				OffToolTip ();
		}

		public void DoToolTip ()
		{ 
				toolTip.transform.position = this.transform.position + new Vector3 (150, 0, 0);
				toolTip.SetActive (true);
				if (shopItemAbility == 0) {
						toolTip.GetComponentInChildren<Text> ().text = sData.currentItem.toolTip;
				}
				if (shopItemAbility == 1) {
						toolTip.GetComponentInChildren<Text> ().text = iData.currentItem.toolTip;
				}
				if (shopItemAbility == 2) {
						toolTip.GetComponentInChildren<Text> ().text = aData.currentAbility.toolTip;
				}
				if (toolTip.GetComponentInChildren<Text> ().text == "") {
						OffToolTip ();
				}
		}

		public void OffToolTip ()
		{
				toolTip.SetActive (false);
		}
}