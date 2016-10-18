using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Client : MonoBehaviour {
	
	public GameObject playerPrefabs;

	string IP = "127.0.0.1";
	int Port = 20168; //for c# server port
	public GameObject SelfPlayer;
    private bool bStart = false;

    NetworkClient client = new NetworkClient(); //ClientScene.ConnectLocalServer(); for local

    public class MyMsgType
    {
        public static short Move = MsgType.Highest + 1;
    };
    public class MoveMessage : MessageBase
    {
        public int row;
        public int col;
    }

    private void ClientSetup()
    {
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MyMsgType.Move, OnPlayerMove);   // NetworkServer.RegisterHandler on server
        client.Connect(IP, Port);
        Debug.Log("Connect。。。。。");
    }

    private void ServerSetup()
    {
        NetworkServer.Listen(20168);
        bStart = true;
    }

    private void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Client connect to server！");
    }

    private void OnPlayerMove(NetworkMessage msg)
    {
        MoveMessage MoveMsg = msg.ReadMessage<MoveMessage>();
        Debug.Log("Player move to " + MoveMsg.row + ":" + MoveMsg.col);
    }

	// Use this for initialization
	void Start () {

	}

	void OnGUI(){
        if (!bStart)
        {
			StartConnect();
        }
        else
        {
			InGame();
        }
		
    }

	void StartConnect()
	{
		if (GUILayout.Button ("Server")) {
            ServerSetup();
		}
        else if(GUILayout.Button("Connect"))
        {
            ClientSetup();
        }
	}
	
	void InGame(){
        if(NetworkServer.active)
        {
            if (GUILayout.Button ("Test Move"))
            {
                MoveMessage msg = new MoveMessage();
                msg.row = 3;
                msg.col = 5;

                NetworkServer.SendToAll(MyMsgType.Move, msg);
            }
        }
		else if (GUILayout.Button ("Quit"))
        {

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
