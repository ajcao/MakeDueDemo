using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationHandler : MonoBehaviour
{
    //Quick Temporary method of animating by moving gameObject
    private Coroutine currentAnimation;
    
    public void StartAnimation(GameObject G, string A)
    {
        Debug.Log("Starting animation");
        switch (A)
        {
            case("Jump"):
                currentAnimation = StartCoroutine(JumpAnimation(G));
                break;
            case("EnemyAttack"):
                currentAnimation = StartCoroutine(EnemyAttackAnimation(G));
                break;
            case("Flash"):
                currentAnimation = StartCoroutine(FlashAnimation(G));
                break;
            case("Shake"):
                currentAnimation = StartCoroutine(ShakeAnimation(G));
                break;
            default:
                break;
        }
        
    }
    
    public bool isAnimationPlaying()
    {
        return (currentAnimation != null);
    }
    
    
    IEnumerator JumpAnimation(GameObject G)
    {
        for (int i = 8; i >= -8; i--)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position += new Vector3(0.0f, (float) i/40.0f, 0.0f);
            yield return new WaitForSeconds(0.05f);
        }
        currentAnimation = null;
    }
    
    IEnumerator EnemyAttackAnimation(GameObject G)
    {
        for (int i = 0; i < 8 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position -= new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }
        
        for (int i = 0; i < 16 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position += new Vector3(0.02f, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }
        
        currentAnimation = null;
    }
    
    IEnumerator FlashAnimation(GameObject G)
    {
        SpriteRenderer currentSprite = G.GetComponentInChildren<SpriteRenderer>();
        
        for (int i = 0; i < 40 ; i++)
        {
            
            currentSprite.color -= new Color(0.0f, 0.0f, 0.05f, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
        
        for (int i = 0; i < 40 ; i++)
        {
            
            currentSprite.color += new Color(0.0f, 0.0f, 0.05f, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < 40 ; i++)
        {
            
            
            currentSprite.color -= new Color(0.0f, 0.0f, 0.05f, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < 40 ; i++)
        {
            
            currentSprite.color += new Color(0.0f, 0.0f, 0.05f, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
        currentAnimation = null;
    }
    
    IEnumerator ShakeAnimation(GameObject G)
    {
        for (int i = 0; i < 4 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position -= new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (int i = 0; i < 8 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position += new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (int i = 0; i < 8 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position -= new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (int i = 0; i < 8 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position += new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (int i = 0; i < 4 ; i++)
        {
            if (G == null)
            {
                break;
            }
            
            G.transform.position += new Vector3(0.04f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        currentAnimation = null;
    }
}
