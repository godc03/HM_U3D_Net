using UnityEngine;
using System.Collections;
//socket
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class Client : MonoBehaviour {
	
	public GameObject playerPrefabs;

	string IP = "127.0.0.1";
	int Port = 20168; //for c# server port
	public GameObject SelfPlayer;
    private bool bStart = false;

    Socket socket;
    IPEndPoint ipEnd;
    Thread netThread;
    byte[] recvBuf = new byte[1024];
    byte[] sendBuf = new byte[1024];

    private void ClientSetup()
    {

        IPAddress ip = IPAddress.Parse(IP);
        ipEnd = new IPEndPoint(ip, Port);

        if (socket != null)
            socket.Close();

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(ipEnd);
        netThread = new Thread(new ThreadStart(SocketReceive));
        netThread.Start();
        Debug.Log("Connect。。。。。");
        bStart = true;
    }

    void SocketReceive()
    {
        while(true)
        {
            int recvLen = socket.Receive(recvBuf);
            string recvStr = Encoding.ASCII.GetString(recvBuf, 0, recvLen);
            Debug.Log("Receive message:" + recvStr);
        }
    }

    public void SocketSend(string sendStr)
    {
        sendBuf = Encoding.ASCII.GetBytes(sendStr);
        socket.Send(sendBuf, sendBuf.Length, SocketFlags.None);
    }
 

	// Use this for initialization
	void Start () {

	}

	void OnGUI(){
        if (!bStart)
        {
            if(GUILayout.Button("Login"))
            {
                ClientSetup();
                SocketSend("hello world");
            }
        }
        else
        {
            if (GUILayout.Button("Quit"))
            {
                if (netThread != null)
                {
                    netThread.Interrupt();
                    netThread.Abort();
                    if (socket != null)
                    {
                        socket.Close();
                    }
                }
                bStart = false;
                Debug.Log("disconnect !");
            }
        }
		
    }

	// Update is called once per frame
	void Update () {
	
	}
}
