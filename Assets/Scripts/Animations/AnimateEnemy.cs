using UnityEngine;

public class AnimateEnemy : MonoBehaviour
{
    private Animator enemyAnimator;
    private WanderingAI wanderingAI;
    private AttackingAI attackingAI;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        wanderingAI = GetComponent<WanderingAI>();
        attackingAI = GetComponent<AttackingAI>();
    }

    public void EnemyAttackAnimation()
    {
        if (wanderingAI.IsInProximity)
        {
            enemyAnimator.SetBool("isWalking", false);

            //Debug.Log("Enemy is attacking");
            enemyAnimator.SetTrigger("isAttacking");
        }
        else
        {
            enemyAnimator.SetBool("isWalking", true);
        }
    }

    public void UpdateWalkingAnimation(bool isWalking)
    {
        enemyAnimator.SetBool("isWalking", isWalking);
    }
}
