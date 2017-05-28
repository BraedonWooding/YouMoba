using UnityEngine;
using System.Collections;

//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class StatDisplayer : MonoBehaviour
{

    public Stats.stat[] statsToDisplay;
    public UnityEngine.UI.Text[] outputText;
    public string[] toSay;


    void Update()
    {
        for (int i = 0; i < statsToDisplay.Length; i++)
        {
            outputText[i].text = toSay[i] + Stats.statInstance.finalStatsInspector[statsToDisplay[i].ToString()] + "";
        }
    }
}