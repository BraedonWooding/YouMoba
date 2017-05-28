using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class Default : MonoBehaviour
{
		ShopSet[] shopSets;
		
		public virtual void Awake ()
		{
				ShopDatabase.shopInstance.AddShopItem (new TestItem ());
				ShopDatabase.shopInstance.AddShopItem (new MOOITEM ());
				shopSets = new ShopSet[2];
				shopSets [0] = new ShopSet ("Starting", new Item[2] {
						ShopDatabase.shopInstance.items ["MOO Item"],
						ShopDatabase.shopInstance.items ["MOO Item"]
				});
				shopSets [1] = new ShopSet ("RUSH", new Item[1] {
				ShopDatabase.shopInstance.items ["Test Item"]});
				ShopDatabase.shopInstance.recommended = shopSets;
		}
}