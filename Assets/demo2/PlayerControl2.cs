using UnityEngine;
using System.Collections;

public class PlayerControl2 : MonoBehaviour {

	enum Direct
	{
		Up = 0,
		Right = 1,
		Down = 2,
		Left = 3,
	}
	private int[,] MoveOffset = new int[4,2]
	{
		{0,1},
		{1,0},
		{0,-1},
		{-1,0}
	};
	private int PosX = 3;
	private int PosY = 5;
	private Direct CurDirect = Direct.Down;
	private int MapID = 0;	//0: left map 1: RightMap
	private float beginX = 0.0f, beginY = 0.0f;
	private float PassTime = 0.0f;

	private GameLogic game;
	// Use this for initialization
	void Start () {
		game = GameObject.Find ("bg").GetComponent<GameLogic> ();
		Debug.Log ("game = " + game);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<NetworkView>().isMine) {		//!isLocalPlayer
			return;
		}
		//Input
		if (Input.GetKeyDown (KeyCode.W)) {
			CurDirect = Direct.Up;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			CurDirect = Direct.Down;
		} else if (Input.GetKeyDown (KeyCode.A)) {
			CurDirect = Direct.Left;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			CurDirect = Direct.Right;
		}

		PassTime += Time.deltaTime;
		if(PassTime >  game.MoveInterval)
		{
			PassTime -= game.MoveInterval;
			//update cell
			PosX += MoveOffset[(int)CurDirect,0];
			PosY += MoveOffset[(int)CurDirect,1];
			if(PosX < 0)
			{
				PosX = 0;
			}
			else if(PosX > game.MaxX)
			{
				PosX = game.MaxX;
			}
			if(PosY < 0)
			{
				PosY = 0;
			}
			else if(PosY > game.MaxY)
			{
				PosY = game.MaxY;
			}

			//update position
			this.transform.position = new Vector3(beginX + PosX*game.CellSize,beginY + PosY*game.CellSize);
		}
	}

	public void SetPos(int x,int y)
	{
		this.PosX = x;
		this.PosY = y;
	}

	public void OnSetMap(int id)
	{
		GetComponent<NetworkView> ().RPC ("SetMap",RPCMode.All,id);
	}

	public void OnStartGame()
	{
		GetComponent<NetworkView> ().RPC ("StartGame",RPCMode.All);
	}

	[RPC]
	public void SetMap(int id)
	{
		this.MapID = id;
		//init map 
		if (this.MapID == 0) {
			this.beginX = game.Map1BeginX;
			this.beginY = game.Map1BeginY;
		} else {
			this.beginX = game.Map2BeginX;
			this.beginY = game.Map2BeginY;
		}
	}

	[RPC]
	public void StartGame()
	{
		//TODO random set
		this.PosX = 4;
		this.PosY = 9;
	}
}
