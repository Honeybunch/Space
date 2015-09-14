﻿using UnityEngine;

[RequireComponent (typeof ( Rigidbody2D ) ) ]
[RequireComponent ( typeof ( ThrustScript ) ) ]

// I'm sailing away...
public class ShipScript : MonoBehaviour
{
	protected ThrustScript m_thrust;
	protected WeaponScript[] m_weapons;
	
	// Basically the Start method of the script,
	// since the Start of a base class script will not be called
	protected void InitShip()
	{
		m_thrust = GetComponent<ThrustScript>();
	}
	
	protected void FireWeapons()
	{
		for( int i = 0; i < m_weapons.Length; i++ )
		{			
			m_weapons[ i ].Fire();
		}
	}
	
	protected void ReleaseFire()
	{
		for( int i = 0; i < m_weapons.Length; i++ )
		{
			m_weapons[ i ].OnRelease();
		}
	}
}