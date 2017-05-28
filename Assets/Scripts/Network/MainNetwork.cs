using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class MainNetwork : MonoBehaviour
{

		public NetworkView nView;
		string ipAddress;
		private List<string> ID = new List<string> ();
		const int numberOfPlayersMax = 4;
		public string playerID;
		static bool inGame;
		public InputField input_Password;
		public InputField input_IP;
		GameObject canvas_ChampSelect;
		public string playerUsername;
		public GameObject prefab_PlayerYellow;
		public GameObject prefab_PlayerGreen;
		public GameObject panel_GreenSide;
		public GameObject panel_YellowSide;
		public List<CharacterBase> playableCharacters = new List<CharacterBase> ();
		public CharacterBase myCharacter;
		List<CharacterBase> characters = new List<CharacterBase> ();
		public int myPlayerSpot;
		public GameObject prefab_Character;
		public GameObject panel_CharacterHolder;
		public GameObject panel_SkinHolder;
		public GameObject prefab_Skin;
		public GameObject errorMessage;
		public List<string> usernames = new List<string> ();
		static bool freeze = false;

		void Start ()
		{
				Application.runInBackground = true;
				ipAddress = Network.player.ipAddress;
				nView = GetComponent<NetworkView> ();
				input_IP = GameObject.Find ("Input_Ip").GetComponent<InputField> ();
				input_Password = GameObject.Find ("Input_Password").GetComponent<InputField> ();
				input_IP.ActivateInputField ();
				input_IP.text = ipAddress;
				input_IP.DeactivateInputField ();
				canvas_ChampSelect = GameObject.Find ("Canvas_ChampSelect");
				panel_YellowSide = GameObject.Find ("Side_0");
				panel_GreenSide = GameObject.Find ("Side_1");
				canvas_ChampSelect.SetActive (false);
				playerUsername = Username.username;
				myCharacter = new CharacterBase ();
				RunPlayableCharacters ();
		}

		public void LaunchServer ()
		{
				Network.incomingPassword = input_Password.text;
				bool useNat = !Network.HavePublicAddress ();
				Network.InitializeServer (numberOfPlayersMax, 25000, useNat);
				playerID = Network.player.guid;
				ID.Add (Network.player.guid);
				ChampionSelection ();
		}
	
		public void JoinServer ()
		{
				Network.Connect (input_IP.text, 25000, input_Password.text);
				playerID = Network.player.guid;
		}
		
		void OnPlayerConnected (NetworkPlayer player)
		{
				if (ID.Contains (player.guid)) {
						//Load Player Back In
						nView.RPC ("LoadPlayersIn", player);
				} else {
						if (freeze) {
								Network.CloseConnection (player, true);
						} else {
								ID.Add (player.guid);
								ChampionSelection ();
								nView.RPC ("RecieveUsername", player, playerUsername, myPlayerSpot);
						}
				}
		}

		void OnFailedToConnect (NetworkConnectionError error)
		{
				errorMessage.SetActive (true);
				errorMessage.GetComponent<Text> ().text = "Failed to Connect: " + error;
		}
	
		void OnPlayerDisconnected (NetworkPlayer player)
		{
				if (Application.loadedLevel < 2) {
						Network.Disconnect ();
				}
		}
		
		void OnDisconnectedFromServer ()
		{
				Application.LoadLevel (0);
		}
	
		void OnApplicationQuit ()
		{
				Network.Disconnect ();
		}

		public void Quit ()
		{
				Application.Quit ();
		}

		public void StartGame ()
		{
				if (Network.peerType == NetworkPeerType.Server) {
						nView.RPC ("LoadPlayersIn", RPCMode.AllBuffered);
				}
		}

		void RunPlayableCharacters ()
		{
				//Add Characters
				playableCharacters.Add (new BrainBile ());
				playableCharacters.Add (new BrainBile_Skin01 ());

				//Load Characters in
				for (int i = 0; i < playableCharacters.Count; i++) {
						if (playableCharacters [i].characterType == CharacterBase.CharacterType.character) {
								GameObject go = (GameObject)Instantiate (prefab_Character);
								go.name = playableCharacters [i].characterName;
								go.GetComponent<CharacterSpriteHolder> ().character = playableCharacters [i];
								go.GetComponent<CharacterSpriteHolder> ().copyOfMainNetwork = this;
								go.GetComponent<CharacterSpriteHolder> ().Setup ();
								go.transform.SetParent (panel_CharacterHolder.transform);
						}
				}
		}

		public void CallChangeCharacter (string newSpriteName, string newCharacterScriptName, string GUID, int spot)
		{
				nView.RPC ("ChangeCharacter", RPCMode.AllBuffered, newSpriteName, newCharacterScriptName, GUID, spot);
		}

		[RPC]

		void LoadPlayersIn ()
		{
				GameObject usernameHolder = GameObject.Find ("UsernameHolder");
				Username usernameScript = usernameHolder.GetComponent<Username> ();
				usernameScript.charactersToSpawn = characters;
				DontDestroyOnLoad (this);
				Application.LoadLevel (2);
				nView.RPC ("FreezeChange", RPCMode.AllBuffered, true);
		}

		[RPC]

		void SpawnObjectServer (string path, Vector3 location, Quaternion rotation)
		{
				if (Network.peerType == NetworkPeerType.Server) {
						nView.RPC ("SpawnObject", RPCMode.All, path, location, rotation); //Not Buffered since if you leave and rejoin you don't want every object to be respawned
				}
		}

		[RPC]

		void SpawnObject (string path, Vector3 location, Quaternion rotation)
		{
				GameObject goToSpawn = Resources.Load (path) as GameObject;
				Instantiate (goToSpawn, location, rotation);
		}

		[RPC]

		void FreezeChange (bool newValue)
		{
				freeze = newValue;
		}

		[RPC]

		void ChampionSelection ()
		{
				if (Network.peerType == NetworkPeerType.Server) {
						nView.RPC ("TurnObjectOnOff", RPCMode.AllBuffered, true);
						for (int i = 0; i < ID.Count; i++) {
								if (i % 2 == 0) {
										nView.RPC ("SpawnChampSelectPlayer", RPCMode.AllBuffered, true, panel_GreenSide.name, ID [i], i);
								} else {
										nView.RPC ("SpawnChampSelectPlayer", RPCMode.AllBuffered, false, panel_YellowSide.name, ID [i], i);
								}		
						}
				}
		}
	
		[RPC]
	
		void ChangeCharacter (string spriteName, string characterBaseScriptName, string ID, int spotToChange)
		{
				CharacterBase tempCharacter = System.Activator.CreateInstance (System.Type.GetType (characterBaseScriptName)) as CharacterBase;
				if (characters.Count > 0 && characters.Count > spotToChange) {
						Debug.Log ("SkinChange" + characters.Count + "/" + spotToChange);
						characters [spotToChange] = tempCharacter; //SkinChange
				} else {
						characters.Add (tempCharacter);
				}
				if (Network.player.guid == ID) {
						if (tempCharacter.characterID != myCharacter.characterID) {
								foreach (Transform child in panel_SkinHolder.transform) {
										Destroy (child.gameObject);
								}
								for (int i = 0; i < playableCharacters.Count; i++) {
										if (playableCharacters [i].characterType == CharacterBase.CharacterType.skin) {
												GameObject skin = (GameObject)Instantiate (prefab_Skin);
												skin.name = playableCharacters [i].characterName;
												skin.GetComponent<CharacterSpriteHolder> ().character = playableCharacters [i];
												skin.GetComponent<CharacterSpriteHolder> ().copyOfMainNetwork = this;
												skin.GetComponent<CharacterSpriteHolder> ().Setup ();
												skin.transform.SetParent (panel_SkinHolder.transform);
										}
								}	
						}
						myCharacter = System.Activator.CreateInstance (System.Type.GetType (characterBaseScriptName)) as CharacterBase;
						GameObject.Find ("UsernameHolder").GetComponent<Username> ().personalCharacter = myCharacter;
				}
				GameObject go = GameObject.Find ("" + spotToChange);
				Image[] images = go.GetComponentsInChildren<Image> ();
				Text[] texts = go.GetComponentsInChildren<Text> ();
				images [1].sprite = tempCharacter.portrait;
				texts [1].text = tempCharacter.characterName;
		}

		[RPC]

		void RecieveUsername (string username, int spot)
		{
				GameObject go = GameObject.Find ("" + spot);
				Text[] texts = go.GetComponentsInChildren<Text> ();
				texts [0].text = username;
				usernames.Add (username);
		}
		
		[RPC]

		void SpawnChampSelectPlayer (bool greenOrYellow, string location, string ID, int spot)
		{
				GameObject go;
				go = GameObject.Find ("" + spot);
				if (go == null) {
						if (greenOrYellow) {
								go = (GameObject)Instantiate (prefab_PlayerGreen);
						} else {
								go = (GameObject)Instantiate (prefab_PlayerYellow);
						}
						go.transform.SetParent (GameObject.Find (location).transform);
						go.name = "" + spot;
						if (ID == Network.player.guid) {
								myPlayerSpot = spot;
								nView.RPC ("RecieveUsername", RPCMode.AllBuffered, playerUsername, myPlayerSpot);
						}
				}
		}

		[RPC]

		void ApplyDamage (string gameObjectName, float damage)
		{

		}

		[RPC]

		void TurnObjectOnOff (bool newState)
		{
				canvas_ChampSelect.SetActive (newState);
		}
}