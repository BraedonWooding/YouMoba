using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour
{

	
		public void ClickedLoginButton ()
		{
				Username.username = GameObject.Find ("Input_Username").GetComponent<UnityEngine.UI.InputField> ().text;
				Application.LoadLevel (1);
		}
}
