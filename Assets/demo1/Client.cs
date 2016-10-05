using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	string IP = "127.0.0.1";
	int Port = 2016;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI(){
		switch (Network.peerType) {
		case NetworkPeerType.Disconnected:
			StartConnect();
			break;
		case NetworkPeerType.Client:
			InGame();
			break;
		default:
			break;
		}
	}

	void StartConnect()
	{
		if (GUILayout.Button ("Login")) {
			NetworkConnectionError err = Network.Connect(IP,Port);
			Debug.Log ("Error Msg:"+err);
		}
	}

	void InGame(){
		if (GUILayout.Button ("Quit")) {
			Network.Disconnect();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
