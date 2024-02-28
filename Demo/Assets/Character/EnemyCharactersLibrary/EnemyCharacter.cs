using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMoveUtil;
using BuffUtil;


namespace CharacterUtil
{
    
//Enemy Character Class
public abstract class EnemyCharacter : Character
{
	//Move pool for display variable
	protected Stack<EnemyMove> Moves;
	
	public bool canPoiseRegenerate;
	public bool IsStunned;
	protected int Poise;
	protected int MaxPoise;
	protected int PoiseRegeneration;
	
	public bool MultipleLives = false;
	
	public string Forms = "Default";
	
	public int getPoise()
	{
		return this.Poise;
	}
	
	public void setPoise(int s)
	{
		//When Stunned, poise does not matter anymore
		if (!this.IsStunned)
		{
			//If poise is lowering, cancel regenerating poise at end of turn
			if (s < this.Poise)
			{
				canPoiseRegenerate = false;
			}
			this.Poise = Mathf.Max(s,0);
			if (this.Poise <= 0)
			{
				this.GetStunned();
			}
		}
	}
	
	public int getMaxPoise()
	{
		return this.MaxPoise;
	}
	
	public void setMaxPoise(int s)
	{
		this.MaxPoise = s;
	}
	
	public int getPoiseRegeneration()
	{
		return this.PoiseRegeneration;
	}
	
	public abstract void GenerateMoves();

	
	public Stack<EnemyMove> getCurrentMoves()
	{
		return Moves;
	}
	
	public void DeleteMoves()
	{
		//Delete movepool of Enemy
		Stack<EnemyMove> Moves = this.getCurrentMoves();
		while (Moves.Count > 0)
		{
			EnemyMove EM = this.getCurrentMoves().Pop();
			EM.DeleteMoveIndicator();
		}
	}
	
	public void GetStunned()
	{
		if (!this.isAlive())
		{
			return;
		}
		
		//If the enemy already has a move plan, delete it 
		//and replace the move with the Stunned moved
		if (Moves.Count > 0)
		{
			EnemyMove EM = Moves.Pop();
			EM.DeleteMoveIndicator();
		}		
		Moves.Push(new EnemyStunnedMove());
		this.IsStunned = true;
		
		
		//Additioanl debuff is placed on enemy turn
		//since duration will count down before players next turn
		EnemyMoveHandler EM_Handler = GameObject.Find("EnemyMoveHandlerGameObject").GetComponent<EnemyMoveHandler>();
		StunnedBuff B;
		if (EM_Handler.EnemyisMoving)
		{
			B = new StunnedBuff(this, this, null, 2);
		}
		else
		{
			B = new StunnedBuff(this, this, null, 1);
		}
        BattleLogicHandler.OnBuffApply(B);
		
	}
	
	public void doNothing()
	{
		
	}
	
	public virtual void InitialBuffs()
	{
		return;
	}
	
	public virtual void EnterNextLife()
	{
		return;
	}
	
	public virtual void EnterNewForm(string s)
	{
		return;
	}
}

}
