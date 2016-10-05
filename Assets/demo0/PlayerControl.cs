using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

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


	public GameLogic game;
	// Use this for initialization
	void Start () {
		//random set

		//init map 
		
		if (this.MapID == 0) {
			beginX = game.Map1BeginX;
			beginY = game.Map1BeginY;
		} else {
			beginX = game.Map2BeginX;
			beginY = game.Map2BeginY;
		}
	}
	
	// Update is called once per frame
	void Update () {
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
}
