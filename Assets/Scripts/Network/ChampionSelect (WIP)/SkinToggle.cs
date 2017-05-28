using UnityEngine;
using System.Collections;

public class SkinToggle : MonoBehaviour
{
		GameObject skinSelect;

		void Start ()
		{
				skinSelect = GameObject.Find ("SelectSkin");
				skinSelect.SetActive (false);
		}

		public void Toggle ()
		{
				skinSelect.SetActive (!skinSelect.activeInHierarchy);
		}
}