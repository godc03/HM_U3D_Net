using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {

	public int Port = 2016;
	
	public GameObject playerPrefabs;
	private ArrayList playerList = new ArrayList();

	private bool gameStarted = false;
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
			playerList = new ArrayList();
		}
		GUILayout.Label ("Player List:");
		for (int i = 0; i < Network.connections.Length; ++i) {
			GUILayout.Label(Network.connections[i].ipAddress+":"+Network.connections[i].port);
		}

		if (!gameStarted && Network.connections.Length >= 2) {
			AllReady();
			gameStarted = true;
		}
	}
	void OnPlayerConnected(NetworkPlayer player)
	{
		playerList.Add(player);

	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Debug.Log("Clean up after player " + player);  
		Network.RemoveRPCs(player);  
		Network.DestroyPlayerObjects(player);  

		//TODO delete from playerList
	}

	// Update is called once per frame
	void Update () {
	
	}

	void AllReady()
	{
		//for (int i = 0; i < Network.connections.Length; ++i) {

		//}

		for (int i = 0; i<playerList.Count; ++i) {
			//NetworkView  networkView = playerList[i].networkView;
			//networkView.RPC("StartGame",RPCMode.All);
			GetComponent<NetworkView>().RPC("SetMap",(NetworkPlayer)playerList[i],i);
			Debug.Log ("SetMap:"+i);
		}
		GetComponent<NetworkView>().RPC("StartGame",RPCMode.Others);
		Debug.Log ("RPC StartGame");
	}

	[RPC]
	public void StartGame()
	{

	}
	[RPC]
	public void SetMap(int id)
	{

	}
}
