﻿using UnityEngine;
using System.Collections;

public class MineWeaponScript : WeaponScript
{
	public float fireTime = 1.0f;
	public float mineLifeTime = 60.0f;
	public float projectileSpeed = 10.0f;

	private MineProjectileScript m_mineProj;
	private bool m_canFire = true;

	void Start ()
	{
		m_mineProj = projectilePrefab.GetComponent<MineProjectileScript>();
		Init();
	}

	public override void Fire()
	{
		if( !m_active || !m_canFire )
		{
			// Early return
			return;
		}
		
		FireMine();
		
		StartCoroutine( FireDelay() );
	}

	public override void OnRelease()
	{
	}

	protected override void ApplyModifier()
	{
		if( modifier == WeaponModifier.ModifierNames.DEFAULT )
		{
			// Early return
			return;
		}
		
		damage *= WeaponModifier.GetModifierValue( modifier, WeaponModifier.Stats.DAMAGE );
		projectileSpeed *= WeaponModifier.GetModifierValue( modifier, WeaponModifier.Stats.MINE_SPEED );
		fireTime /= WeaponModifier.GetModifierValue( modifier, WeaponModifier.Stats.FIRE_RATE );
	}

	private void FireMine()
	{	
		MineProjectileScript projectile = (MineProjectileScript)Instantiate( m_mineProj, 
		                                                					 transform.position, 
		                                               						 Quaternion.identity );
		projectile.Init( damage, projectileSpeed, mineLifeTime, transform.parent.gameObject );
		projectile.FireProj( transform.eulerAngles.z );
		
		m_soundSystem.PlayOneShot( fireSoundName );
	}
	
	private IEnumerator FireDelay()
	{
		m_canFire = false;
		yield return new WaitForSeconds( fireTime );
		m_canFire = true;
	}
}
