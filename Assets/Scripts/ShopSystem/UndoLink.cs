using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class UndoLink : MonoBehaviour
{
		private ShopDatabase copy;

		void Awake ()
		{
				copy = GameObject.Find ("Shop").GetComponent<ShopDatabase> ();
		}
		
		public void Undo ()
		{
				copy.Undo ();
		}
}