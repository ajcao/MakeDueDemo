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
	protected Stack<EnemyMove> Moves;
	
	public bool canStaminaRegenerate;
	public bool IsStunned;
	protected int Stamina;
	protected int MaxStamina;
	protected int StaminaRegeneration;
	
	public int getStamina()
	{
		return this.Stamina;
	}
	
	public void setStamina(int s)
	{
		//When Stunned, stamina does not matter anymore
		if (!this.IsStunned)
		{
			//If stamina is lowering, cancel ability to regenerate stamina
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
	
	public void EnemyCastMoves()
	{
		EnemyMove EM = Moves.Pop();
		EM.onCastWrapper();
		EM.DeleteMove();
	}
	
	public void GetStunned()
	{
		EnemyMove EM = Moves.Peek();
		//Pop the move onto the stack without removing
		if (!EM.IsSpecial())
		{
			EM = Moves.Pop();
			EM.DeleteMove();
		}			
		Moves.Push(new EnemyStunnedMove());
		this.IsStunned = true;
		GameObject.Find("EnemyMoveHandlerGameObject").GetComponent<EnemyMoveHandler>().DrawMoves(this);
		
	}
	
	public void doNothing()
	{
		
	}
}

}
