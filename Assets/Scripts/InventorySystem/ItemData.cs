using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class ItemData : MonoBehaviour
{

		//A copy of a item to display and for functions
		public Item currentItem;
		public int currentPress;
		public Image[] spriteHolder;
		Image sprites;
		public int id;

		void Awake ()
		{
				//Just a way so less drag + dropping
				spriteHolder = GetComponentsInChildren<Image> ();
				currentPress = -1;
				sprites = this.GetComponent<Image> ();
				int.TryParse (name.Substring (0, 1), out id);
		}

		void Update ()
		{
				if (spriteHolder [1].gameObject.activeInHierarchy && currentItem.activeKey != KeyCode.None) {
						GetComponentInChildren<Text> ().text = currentItem.activeKey.ToString ();
				}
				if (currentItem.abilityCooldown > 0) {
						GetComponentInChildren<Text> ().text += "  " + currentItem.abilityCooldown.ToString ("0.0");
				}
				if (currentItem.itemTexture == null) {
						spriteHolder [1].gameObject.SetActive (false);
				} else if (currentItem.itemTexture != null) {
						spriteHolder [1].gameObject.SetActive (true);
						spriteHolder [1].sprite = currentItem.itemTexture;
				}
		}
		public void Press ()
		{
				currentItem.activate = true;
		}
		public void Select ()
		{
				if (sprites.color != Color.grey && currentPress == -1) {
						sprites.color = Color.grey;
						currentPress = id;
						InventoryManager.inventoryInstance.presses.Add (id);
				} else {
						InventoryManager.inventoryInstance.presses.RemoveAt (currentPress);
						Remove ();
				}
		}
		public void Remove ()
		{
				if (currentPress > -1) {
						sprites.color = Color.white;
						currentPress = -1;
				}
		}

		public void Select2 ()
		{

				if (sprites.color != Color.grey) {
						sprites.color = Color.grey;
				} else {
						sprites.color = Color.white;
				}
		}
		public void Sell ()
		{

				if (sprites.color == Color.grey) {
						ShopDatabase.shopInstance.SellItem (currentItem);
				}
				sprites.color = Color.white;
		}
}