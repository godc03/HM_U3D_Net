﻿using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public float MoveInterval = 0.7f;	//0.7s per Cell
	public float Map1BeginX = -3.085f;
	public float Map1BeginY = -3.065f;
	public float CellSize =  0.317f;	
	public float Map2BeginX = 0.169f;
	public float Map2BeginY = -3.065f;
	public int MaxX = 9;
	public int MaxY = 19;

	private static GameLogic instance;
	public static GameLogic getInstance{
		get{
			if(instance==null){
				instance=new GameLogic();
			}
			return instance;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
