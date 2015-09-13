using UnityEngine;
using System.Collections.Generic;
using Utility;
using BladeCast;

public class GameController : MonoBehaviour 
{
	public GameObject[] playerDB;

	//private Dictionary<int, GameObject> players;

	// Use this for initialization
	void Start () {
		BCMessenger.Instance.RegisterListener ("connect", 0, this.gameObject, "HandleConnect");
		BCMessenger.Instance.RegisterListener ("disconnect", 0, this.gameObject, "HandleDisconnect");
	}

	private void HandleConnect(ControllerMessage msg) 
	{
		int controlID = msg.ControllerSource;

		if (controlID > playerDB.Length) 
		{
			return;
		}
		if (playerDB [controlID])
		{
			Instantiate (playerDB[controlID], new Vector3 (0f, 5f, 0f), Quaternion.identity);
		}
	}

	private void HandleDisconnect(ControllerMessage msg) 
	{
		if (this.gameObject) {
			int controlID = msg.ControllerSource;
			if (playerDB [controlID]) {
				//BCMessenger.DestroyObject (playerDB [controlID]);
				//Destroy (playerDB[controlID]);
				this.gameObject.SetActive(false);
			}
		}
	} 
}
