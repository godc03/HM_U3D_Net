using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {

	public int Port = 2016;

	// Use this for initialization
	void Start () {
	
	}

	//GUI
	void OnGUI(){
		switch (Network.peerType) {
		
		case NetworkPeerType.Disconnected:
			StartServer();
			break;
		case NetworkPeerType.Server:
			OnServer();
			break;
		default:	//Client Connecting
			break;
		}
	}

	void StartServer(){
		if(GUILayout.Button("Start Server"))
		{
			NetworkConnectionError err = Network.InitializeServer(10,Port,false);
			Debug.Log ("Error Msg:" + err);
		}
	}

	void OnServer(){
		if (GUILayout.Button ("Stop Server")) {
			Network.Disconnect();
		}
		GUILayout.Label ("Player List:");
		for (int i = 0; i < Network.connections.Length; ++i) {
			GUILayout.Label(Network.connections[i].ipAddress+":"+Network.connections[i].port);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
