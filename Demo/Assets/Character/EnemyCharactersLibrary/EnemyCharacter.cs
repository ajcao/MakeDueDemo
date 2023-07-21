using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMoveUtil;


namespace CharacterUtil
{
    
//Enemy Character Class
public abstract class EnemyCharacter : Character
{
	//Move pool for display variable
	protected Queue<EnemyMove> Moves;
	
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
	
	public abstract void GenerateMoves();

	
	public Queue<EnemyMove> getCurrentMoves()
	{
		return Moves;
	}
	
	public void EnemyCastMoves()
	{
		EnemyMove EM = Moves.Dequeue();
		EM.onCast();
		EM.DeleteMove();
	}
	
	public void doNothing()
	{
		
	}
}

}
