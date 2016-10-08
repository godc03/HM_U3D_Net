using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {
	
	public GameObject playerPrefabs;

	string IP = "127.0.0.1";
	int Port = 2016;

	bool testFlag = false;
	GameObject[] PlayerList;
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

			//GameObject player = Instantiate(playerPrefabs);

		}
	}

	void OnConnectedToServer()
	{
		GameObject player = (GameObject)Network.Instantiate(playerPrefabs,Vector3.zero,Quaternion.identity,0);
		PlayerControl2 playerScript =  player.GetComponent<PlayerControl2>();
		Debug.Log("playerScritp = "+playerScript);
	}



	void OnDisconnectedFromServer(NetworkDisconnection info )
	{
		Debug.Log("Clean up a bit after server quit");  
		//Network.DestroyPlayerObjects(Network.player);  
		//Network.RemoveRPCs(Network.player);  
	}
	
	void InGame(){
		if (GUILayout.Button ("Quit")) {
			Network.Disconnect();
		}
		if (!testFlag) {

			//playerScript.SetPos(5,13);
			testFlag = true;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
