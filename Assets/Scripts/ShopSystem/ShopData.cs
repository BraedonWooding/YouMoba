using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class ShopData : MonoBehaviour
{

		public Item currentItem;
		public Image spriteHolder;
		ShopDatabase privateDatabase;
		public Text txt;
		public string Name = "ShopItem";

		void Start ()
		{
				privateDatabase = ShopDatabase.shopInstance;
				Setup ();
		}

		public void Setup ()
		{
				if (!this.gameObject.activeInHierarchy) {
						this.gameObject.SetActive (true);
				}

				spriteHolder = GetComponentInChildren<Image> ();
				name = Name + currentItem.itemID;
				txt.text = currentItem.name;
		}

		void Update ()
		{
				if (currentItem.itemTexture == null) {
						spriteHolder.gameObject.SetActive (false);
						txt.gameObject.SetActive (false);
				} else if (currentItem.itemTexture != null) {
						spriteHolder.gameObject.SetActive (true);
						spriteHolder.sprite = currentItem.itemTexture;
						txt.gameObject.SetActive (true);
						txt.text = currentItem.finalPrice.ToString ();
				}
		}



		public void BuyItem ()
		{
				privateDatabase.BuyItem (currentItem.itemName, true);
		}

		public void Select ()
		{
				privateDatabase.RefreshNextWindow (currentItem.name);
		}
}