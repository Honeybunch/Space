﻿using UnityEngine;
using System.Collections;

public class ObjectiveEvent : MonoBehaviour {

	Contract objectiveContract;

	public Contract ObjectiveContract {
		get { return objectiveContract; }
		set { objectiveContract = value; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if( col.tag == "Ship" )
		{
			objectiveContract.completed = true;//Make boolean an array for multi-mission contracts
			GameObject.Destroy(this.gameObject);
		}
	}
}