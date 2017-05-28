using UnityEngine;
using System.Collections;
using System;
using AdvancedInspector;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
//just a complex small class to store player variables
[AdvancedInspector]
[Serializable]
public class PlayersBoard {

		public string name;
		public int playerId;
		public GameObject prefab;
		public static int numberOfPlayers = 0;
		public BoardManager.TeamColour playerTeam;
		[Inspect]
		public scoreDic stats = new scoreDic();
		
		public PlayersBoard (string Name, BoardManager.TeamColour team)
		{
			//this just sets all the player variables to values
			name = Name;
			playerId = numberOfPlayers;
			playerTeam = team;
			numberOfPlayers ++;
			stats.Clear();
			stats.Add ("Kills", 0);
			stats.Add ("Deaths", 0);
			stats.Add ("Assists", 0);
			stats.Add ("Minions", 0);
			stats.Add ("Score", 0);
			stats.Add ("Buildings", 0);
			switch(playerTeam) {
			case BoardManager.TeamColour.blue : {
				prefab = (GameObject)GameObject.Instantiate (BoardManager.scores.prefabB);
				prefab.name = name;
				prefab.transform.SetParent (BoardManager.scores.parentB);
				break;
			}
			case BoardManager.TeamColour.red : {
				prefab = (GameObject)GameObject.Instantiate (BoardManager.scores.prefabR);
				prefab.name = name;
				prefab.transform.SetParent (BoardManager.scores.parentR);
				break;
			}
			}
			/* 
			For ^^ above just add more team colors in the format of this
			case BoardManager.TeamColour.[your team colour] : { 
			prefab = (GameObject)GameObject.Instantiate (scores.prefab[Colour initial]);
			prefab.name = name;
			prefab.transform.SetParent (scores.parent[Colour initial);
			} 
			 */
		}
	public PlayersBoard () {

	}
	}
[Serializable]
public class scoreDic : UDictionary<string, int> {}