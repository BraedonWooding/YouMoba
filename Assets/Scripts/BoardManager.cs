using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using AdvancedInspector;
using System;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
[AdvancedInspector(InspectDefaultItems = true)]
public class BoardManager : MonoBehaviour
{
		[Inspect, CreateDerived]
		public List<PlayersBoard>
				players = new List<PlayersBoard> ();
		[HideInInspector]
		public static BoardManager
				scores;
		//Add more of these below when making new colours
		public Transform  parentR, parentB;
		public GameObject prefabR, prefabB;
		public GameObject main;

		//Add more team colours and the system should support infinite colours as long as you give more transforms and prefabs
		public enum TeamColour
		{
				red,
				blue
		}
		void Awake ()
		{
				scores = this;
				AddPlayer ("Shadow", TeamColour.blue);
				AddPlayer ("Fang", TeamColour.red);
				SetStats ("Kills", 0, 10);
				IncrementStats ("Deaths", 1, 5);
				IncrementStats ("Deaths", 1, -2);
		}

		void Start ()
		{
				Initialize ();
		}

		public void Initialize ()
		{
				bool setActive;
				if (!main.activeSelf) {
						main.SetActive (true);
						setActive = false;
				} else {
						setActive = true;
				}
				//Just goes through each player and adds data to it as well as setting the data
				Text[] txt = new Text[4];
				PlayersBoard p;
				for (int i = 0; i < players.Count; i++) {
						p = players [i];
						if (!players [i].stats.ContainsKey ("Kills")) {
								p.stats.Add ("Kills", 0);
						}
						if (!p.stats.ContainsKey ("Deaths")) {
								p.stats.Add ("Deaths", 0);
						}
						if (!p.stats.ContainsKey ("Assists")) {
								p.stats.Add ("Assists", 0);
						}
						if (!p.stats.ContainsKey ("Minions")) {
								p.stats.Add ("Minions", 0);
						}
						if (!p.stats.ContainsKey ("Score")) {
								p.stats.Add ("Score", 0);
						}
						if (!p.stats.ContainsKey ("Buildings")) {
								p.stats.Add ("Buildings", 0);
						}
						if (p.prefab != null) {
								txt = p.prefab.GetComponentsInChildren<Text> ();
								txt [0].text = p.name;
								txt [1].text = p.stats ["Score"].ToString ();
								txt [2].text = p.stats ["Kills"].ToString ();
								txt [3].text = p.stats ["Deaths"].ToString ();
								txt [4].text = p.stats ["Assists"].ToString ();
								txt [5].text = p.stats ["Minions"].ToString ();
								txt [6].text = p.stats ["Buildings"].ToString ();
						}
				}
				main.SetActive (setActive);
		}
		//Just adds a new player
		public void AddPlayer (string name, TeamColour team)
		{
				players.Add (new PlayersBoard (name, team));
				Initialize ();
		}
		//For setting stats to x
		public void SetStats (string stat, int id, int amount)
		{
				players [id].stats [stat] = amount;
				Initialize ();
		}
		//for incrementing (adding/removing) stats by x
		public void IncrementStats (string stat, int id, int amount)
		{
				players [id].stats [stat] += amount;
				Initialize ();
		}
		public int GetPlayerID (string name)
		{
				for (int i = 0; i < players.Count; i++) {
						if (players [i].name == name) {
								return players [i].playerId;
						}
				}
				return -1;
		}
}