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
	
	public bool canStaminaRegenerate;
	public bool IsStunned;
	protected int Stamina;
	protected int MaxStamina;
	protected int StaminaRegeneration;
	
	public bool CanRevive;
	
	public int getStamina()
	{
		return this.Stamina;
	}
	
	public void setStamina(int s)
	{
		//When Stunned, stamina does not matter anymore
		if (!this.IsStunned)
		{
			//If stamina is lowering, cancel regenerating stamina at end of turn
			if (s < this.Stamina)
			{
				canStaminaRegenerate = false;
			}
			this.Stamina = Mathf.Max(s,0);
			if (this.Stamina <= 0)
			{
				this.GetStunned();
			}
		}
	}
	
	public int getMaxStamina()
	{
		return this.MaxStamina;
	}
	
	public void setMaxStamina(int s)
	{
		this.MaxStamina = s;
	}
	
	public int getStaminaRegeneration()
	{
		return this.StaminaRegeneration;
	}
	
	public abstract void GenerateMoves();

	
	public Stack<EnemyMove> getCurrentMoves()
	{
		return Moves;
	}
	
	public void GetStunned()
	{
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
	
	public virtual void Respawn()
	{
		return;
	}
}

}
