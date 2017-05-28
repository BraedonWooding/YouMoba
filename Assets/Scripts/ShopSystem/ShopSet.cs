using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[System.Serializable]
public class ShopSet
{
		public Item[] setItems;
		public string name;
		
		public ShopSet (string Name, Item[] items)
		{
				setItems = items;
				name = Name;
		}
}
