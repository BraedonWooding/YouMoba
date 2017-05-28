using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[System.Serializable]
public class Item {

    public Sprite itemTexture;
    public int itemID;
    public string name;
    public Stats.basicStats itemStats;
    public bool isActive;
    public bool oldActive;
    public int inventoryId;
    public KeyCode activeKey = KeyCode.None;
    public float abilityCooldown, abilityMaxCooldown;
    public float recipePrice;
    public bool bought;
	public float fullPrice;
	public float finalPrice;
	[System.NonSerialized]
	public Item[] buildInto;
    public string toolTip;
    public bool activate;
	public bool build;
	public string[] buildIntoNames;
	public string[] requirements;
	public bool reqiure;
    public string itemName = "Item";

	public Item () {
		name = "";
	}

    public virtual void Activate()
    {
    }
}