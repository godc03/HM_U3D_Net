using UnityEngine;
using System.Collections;

public class ServerPlaceholder : MonoBehaviour {
	
	private Client clientScritp;
	// Use this for initialization
	void Start () {
		clientScritp = GameObject.Find ("Main Camera").GetComponent<Client> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	[RPC]
	public void SetMap(int id)
	{
		Debug.Log ("ServerPlaceholder SetMap:" + id);
		clientScritp.SelfPlayer.SendMessage ("OnSetMap", id);
	}
	
	[RPC]
	public void StartGame()
	{
		Debug.Log ("ServerPlaceholder StartGame");
		clientScritp.SelfPlayer.SendMessage ("OnStartGame");
	}
}
