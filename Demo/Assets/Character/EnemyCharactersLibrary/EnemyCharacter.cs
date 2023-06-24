using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterUtil
{
    
//Enemy Character Class
public abstract class EnemyCharacter : Character
{
	//Move pool for display variable
	//protected Moves[] moves?
	
	protected bool canPoiseRegenerate;
	protected int Poise;
	//-1 is full regeneration
	protected int PoiseRegeneration;
	
	public int getPoise()
	{
		return this.Poise;
	}
	
	public void setPoise(int p)
	{
		this.Poise = p;
	}
	
	public void doNothing()
	{
		
	}
}

}
