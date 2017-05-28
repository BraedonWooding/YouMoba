using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class InventoryManager : MonoBehaviour
{
		public Transform[] itemPanels = new Transform[6];
		public Item[] ourInventory = new Item[6];
		public static InventoryManager inventoryInstance;
		public int numberOfItems = 0;
		public List<int> notFilled = new List<int> ();
		public List<int> presses = new List<int> ();

		void Awake ()
		{
				inventoryInstance = this;
				for (int i = 0; i < itemPanels.Length; i++) {
						itemPanels [i] = GameObject.Find ("ItemPanel" + i).transform;
				}
				for (int i = 0; i < ourInventory.Length; i++) {
						if (ourInventory [i].itemTexture == null) {
								notFilled.Add (i);
						}
				}
		}

		public void AddItem (Item item)
		{
				if (numberOfItems < ourInventory.Length) {
						ourInventory [notFilled [0]] = item;
						item.inventoryId = notFilled [0];
						numberOfItems++;
						notFilled.RemoveAt (0);
						OrganizeFilled ();
				}
		}

		public void RemoveItem (int itemID)
		{
				ourInventory [itemID].isActive = false;
				ourInventory [itemID].Activate ();
				ourInventory [itemID] = new Item ();
				numberOfItems -= 1;
				notFilled.Add (itemID);
				OrganizeFilled ();
		}

		public void RemoveStringItem (string itemName)
		{
				int actuallyExists = -1;
				for (int i = 0; i < ourInventory.Length; i++) {
						if (ourInventory [i].name == itemName) {
								actuallyExists = ourInventory [i].inventoryId;
								break;
						}
				}
				if (actuallyExists > -1) {
						RemoveItem (actuallyExists);
				}
		}

		public void ChangeItem (int oldItemID, int newItemID)
		{
				Item interchange = ourInventory [oldItemID];
				ourInventory [oldItemID] = ourInventory [newItemID];
				ourInventory [oldItemID].inventoryId = oldItemID;
				ourInventory [newItemID] = interchange;
				ourInventory [newItemID].inventoryId = newItemID;
				if (ourInventory [oldItemID].itemTexture == null) {
						notFilled.Add (oldItemID);
				} else {
						notFilled.Remove (oldItemID);
				}
				if (ourInventory [newItemID].itemTexture == null) {
						notFilled.Add (newItemID);
				} else {
						notFilled.Remove (newItemID);
				}
				OrganizeFilled ();
		}

		void Update ()
		{
				for (int i = 0; i < ourInventory.Length; i++) {
						if (ourInventory [i].itemTexture != null) {
								ourInventory [i].isActive = true;
								ourInventory [i].Activate ();
						}
				}

				for (int i = 0; i < itemPanels.Length; i++) {
						itemPanels [i].GetComponent<ItemData> ().currentItem = ourInventory [i];
				}

				if (presses.Count >= 2) {
						ChangeItem (presses [0], presses [1]);
						presses.Clear ();
						for (int i = 0; i < itemPanels.Length; i++) {
								itemPanels [i].GetComponent<ItemData> ().Remove ();
						}
				}
		}
		void OrganizeFilled ()
		{
				notFilled.Sort ();
		}
}