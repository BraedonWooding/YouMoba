 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class ShopDatabase : MonoBehaviour
{

		public static ShopDatabase shopInstance;
		public Dictionary<string, Item> items = new Dictionary<string, Item> ();
		public InventoryManager playerInventory;
		public Transform parent;
		public GameObject prefab;
		public GameObject Window;
		public List<string> itemNames = new List<string> ();
		public InputField searchBar;
		public Transform parentSearch;
		public string cachedText;
		public GameObject tooltip;
		public Stats playerStats;
		public Transform[] copies;
		public Transform nextTransform;
		public GameObject prefabNext;
		public ShopSet[] recommended;
		public Dictionary<string, string> otherName = new Dictionary<string, string> ();
		public bool mode { get; set; }
		public Transform parent2;
		static int panelCount;
		public GameObject prefabPanel;
		Transform[] parents;
		bool cachedStatus = false;
		public enum undoOptions
		{
				bought,
				sold
		}
		public List<UndoSet> UndoSets = new List<UndoSet> ();
		
		
		void Awake ()
		{
				shopInstance = this;
				mode = true;
				#region Variable Setting
				//Get variables (Just so we don't need to set these up)
				copies = new Transform[6];
				for (int i = 0; i < copies.Length; i++) {
						copies [i] = GameObject.Find (i + "Copy").transform;
				}
				Window = GameObject.Find ("ShopMain");
				#endregion

				cachedText = "";
				tooltip = GameObject.Find ("ToolTip");
		}

		void Start ()
		{
				//NOTE REPLACE LATER
				playerInventory = GameObject.Find ("_SCRIPTS_").GetComponent<InventoryManager> ();

				#region FullPrice + Build Into

				//Going through each item and just setting up which items build into which things (for our build into window).
				foreach (var key in items) {
						key.Value.bought = false;
						key.Value.fullPrice = key.Value.recipePrice;
						key.Value.inventoryId = -1;
						if (key.Value.build) {
								for (int i = 0; i < key.Value.buildInto.Length; i++) {
										key.Value.buildInto [i] = items [key.Value.buildIntoNames [i]];
								}
						}
				}

				foreach (var key in items) {

						if (key.Value.build) {
								for (int i = 0; i < key.Value.buildInto.Length; i++) {
										key.Value.buildInto [i].fullPrice += key.Value.fullPrice;
								}
						}
				}
				#endregion

				UpdateDatabase ();
				RefreshShopWindow ();
				Window.SetActive (false);
				foreach (var key in items) {
						itemNames.Add (key.Value.name);
						otherName.Add (key.Value.itemName, key.Value.name);
				}
		}

		void Update ()
		{
				if (Window.activeSelf) {
						if (mode) {
								parent2.parent.gameObject.SetActive (false);
						} else {	
								parent2.parent.gameObject.SetActive (true);
						}
						//If you actually have changed the searchbar text then delete every child of the search bar
						if (searchBar.text != cachedText) {
								foreach (Transform child in parentSearch) {
										Destroy (child.gameObject);
								}
								//Then if what you wrote contained anything related to any item (including spacebars *Fix the spacebar thing if we don't want them)
								for (int i = 0; i < itemNames.Count; i++) {
										if (itemNames [i].ToLower ().Contains (searchBar.text.ToLower ())) {
												BuildItemInShop (itemNames [i], parentSearch);
										}
								}
								//Just to prevent infinite loops
								cachedText = searchBar.text;
						}
						//If you're search bar text isn't empty then set the gameobject to true else set it to false
						if (searchBar.text == "") {
								parent.gameObject.SetActive (true);
								parentSearch.gameObject.SetActive (false);
						} else {
								parent.gameObject.SetActive (false);
								parentSearch.gameObject.SetActive (true);
						}
						//Setting the copies to inventory
						for (int i = 0; i < copies.Length; i++) {
								copies [i].GetComponent<ItemData> ().currentItem = playerInventory.ourInventory [i];
						}
				}
				if (Window.activeInHierarchy != cachedStatus) {
						tooltip.SetActive (false);
						cachedStatus = Window.activeInHierarchy;
				}
		}

		public void Undo ()
		{
				if (UndoSets.Count > 0) {
						switch (UndoSets [0].choice) {
					
						case undoOptions.bought:
								UndoSet copy = UndoSets [0];
								playerInventory.RemoveStringItem (UndoSets [0].itemChanged.name);
								playerStats.IncrementStat ("Gold", UndoSets [0].itemChanged.fullPrice);

								if (UndoSets [0].priorItems.Count > 0) {
										for (int i = 0; i < UndoSets[0].priorItems.Count; i++) {
												BuyItem (UndoSets [0].priorItems [i].itemName, true);
										}
								}
								UndoSets.Remove (copy);
								UpdateDatabase ();
								break;

						case undoOptions.sold:
								if (playerStats.finalStatsInspector ["Gold"] >= items [otherName [UndoSets [0].itemChanged.itemName]].finalPrice) {
										playerInventory.AddItem (System.Activator.CreateInstance (System.Type.GetType (UndoSets [0].itemChanged.itemName)) as Item);
										playerStats.IncrementStat ("Gold", -items [otherName [UndoSets [0].itemChanged.itemName]].finalPrice);
										UndoSets.RemoveAt (0);
								}
								UpdateDatabase ();
								break;
						}
				}
		}

		void AddNewUndo (Item item, undoOptions option, List<Item> previousItems)
		{
				UndoSets.Insert (0, new UndoSet (option, item, previousItems));
		}

		public void AddShopItem (Item item)
		{
				//Super simple just for simplicity since you need to also supply the name (good for later if we want cases)
				items.Add (item.name, item);
		}

		public void RefreshShopWindow ()
		{
				#region Normal Section
				//RefreshShopWindow is just rebuilding the items in the shop (deleting then replacing)

				foreach (Transform child in parent) {
						Destroy (child.gameObject);
				}

				//Just rebuildings items
				foreach (var key in items) {
						BuildItemInShop (key.Value.name, parent);
				}

				#endregion

				#region Recommended Section
		
				foreach (Transform child in parent2) {
						Destroy (child.gameObject);
				}
		
				panelCount = 0;
		
				parents = new Transform[recommended.Length];
		
				for (int i = 0; i < recommended.Length; i++) {
						parents [i] = GetNextPanel (recommended [i].name);						
				}

				for (int i = 0; i < recommended.Length; i++) {
						for (int j = 0; j < recommended[i].setItems.Length; j++) {
								BuildItemInShop (recommended [i].setItems [j].name, parents [i]);
						}
				}
				#endregion
		}

		//Same but just for the buildinto window

		public void RefreshNextWindow (string id)
		{
				foreach (Transform child in nextTransform) {
						Destroy (child.gameObject);
				}
				//If the item that you give 'me' has other items that it builds into then display those items
				if (items [id].build) {
						for (int i = 0; i < items[id].buildInto.Length; i++) {
								//This is just setting variables and so on
								GameObject go = (GameObject)Instantiate (prefabNext);
								go.transform.SetParent (nextTransform);
								go.GetComponent<ShopData> ().currentItem = items [id].buildInto [i];
								go.GetComponent<DetectMouseOver> ().toolTip = tooltip;
								go.GetComponent<ShopData> ().Setup ();
							
						}
				}
		}

		//Buying items
		public void BuyItem (string itemName, bool addSets = true)
		{
				
				//Just checking if you have enough spots for items
				if (playerInventory.numberOfItems < InventoryManager.inventoryInstance.itemPanels.Length || HavePreviousItems (itemName)) {
						//If you have enough gold for the items
						if (playerStats.finalStatsInspector ["Gold"] >= items [otherName [itemName]].finalPrice) {
								bool added = false;
								bool hadPrevItems = HavePreviousItems (itemName);
								//Add the item and remove the gold
								List<Item> itemPrivate = new List<Item> ();
								if (playerInventory.numberOfItems < InventoryManager.inventoryInstance.itemPanels.Length && !hadPrevItems) {
										playerInventory.AddItem (System.Activator.CreateInstance (System.Type.GetType (itemName)) as Item);
										playerStats.IncrementStat ("Gold", -items [otherName [itemName]].finalPrice);
										added = true;
								}
								//If you require other items then remove those items (the price is effected before hand, and yeh i know i could do it here but its better before since then you can display a proper price)
								if (items [otherName [itemName]].reqiure) {
										for (int i = 0; i < items[otherName[itemName]].requirements.Length; i++) {
												if (items [items [otherName [itemName]].requirements [i]].bought) {
														playerInventory.RemoveStringItem (items [items [otherName [itemName]].requirements [i]].name);
														itemPrivate.Add (items [items [otherName [itemName]].requirements [i]]);
														for (int j = 0; j < UndoSets.Count; j++) {
																if (UndoSets [j].itemChanged.itemTexture == items [items [otherName [itemName]].requirements [i]].itemTexture) {
																		UndoSets.RemoveAt (j);
																		break;
																}
														}
												}
										}
								}
								if (!added && hadPrevItems && playerInventory.numberOfItems < InventoryManager.inventoryInstance.itemPanels.Length) {
										playerInventory.AddItem (System.Activator.CreateInstance (System.Type.GetType (itemName)) as Item);
										playerStats.IncrementStat ("Gold", -items [otherName [itemName]].finalPrice);
								}
								if (addSets) {
										AddNewUndo (items [otherName [itemName]], undoOptions.bought, itemPrivate);
								}
						} else {
								//We can replace these later but just simple debugs.
								Debug.Log ("Not Enough Gold");
						}
				} else {
						Debug.Log ("Out Of Spaces");
						return;
				}
				UpdateDatabase ();
		}

		void UpdateDatabase ()
		{
				//We are just resetting all the values and build intos (we don't need the build intos) *REMOVE THESE!!!!
				foreach (var key in items) {
						key.Value.bought = false;
						key.Value.finalPrice = key.Value.recipePrice;
						if (key.Value.build) {
								for (int i = 0; i < key.Value.buildInto.Length; i++) {
										key.Value.buildInto [i] = items [key.Value.buildIntoNames [i]];
								}
						}
				}
				//Just going through your items and saying you bought them to the shop database
				for (int i = 0; i < playerInventory.numberOfItems; i++) {
						playerInventory.ourInventory [i].bought = true;
						if (items.ContainsKey (playerInventory.ourInventory [i].name)) {
								items [playerInventory.ourInventory [i].name].bought = true;
						}
				}
				//Then Doing the final price thing
				foreach (var key in items) {

						if (key.Value.build) {
								if (!key.Value.bought) {
										key.Value.inventoryId = -1;
										for (int i = 0; i < key.Value.buildInto.Length; i++) {

												key.Value.buildInto [i].finalPrice += key.Value.finalPrice;
										}
								}
						}
				}
		}

		public void SellItem (Item item)
		{
				//If the item exists (you can't do item != null since item is too arbitary) then you remove the item and gain the gold for the full price.
				if (item.itemTexture != null) {
						playerInventory.RemoveItem (item.inventoryId);
						playerStats.IncrementStat ("Gold", items [item.name].fullPrice);
						AddNewUndo (item, undoOptions.sold, null);
				}
				UpdateDatabase ();
		}

		public void BuildItemInShop (string id, Transform pa)
		{
				//Create the prefab then set variables and the parent
				GameObject go = (GameObject)Instantiate (prefab);
				go.transform.SetParent (pa);
				go.GetComponent<ShopData> ().currentItem = items [id];
				go.GetComponent<DetectMouseOver> ().toolTip = tooltip;
				go.GetComponent<ShopData> ().Setup ();
		}
		public Transform GetNextPanel (string panelText)
		{
				GameObject Go = (GameObject)Instantiate (prefabPanel);
				Go.transform.SetParent (parent2);
				Go.name = "Panel" + panelCount;
				panelCount++;
				Go.GetComponentInChildren<Text> ().text = panelText;
				
				return Go.transform;
		}

		bool HavePreviousItems (string itemName2)
		{
				if (items [otherName [itemName2]].reqiure) {
						int numberOfBoughtItems = 0;
						for (int i = 0; i < items[otherName[itemName2]].requirements.Length; i++) {
								if (items [items [otherName [itemName2]].requirements [i]].bought) {
										numberOfBoughtItems++;
								}
						}
						if (numberOfBoughtItems > 0) {
								return true;
						} else {
								return false;
						}
				} else
						return false;
		}
}
		
[System.Serializable]
public class UndoSet
{
		public ShopDatabase.undoOptions choice;
		public Item itemChanged;
		public List<Item> priorItems = new List<Item> ();

		public UndoSet (ShopDatabase.undoOptions option, Item item, List<Item> prevItems)
		{

				choice = option;
				itemChanged = item;
				priorItems = prevItems;
		}
}