using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public void PlayPlayerAnimation(string animationTrigger)
    {
        playerAnimator.SetTrigger(animationTrigger);
    }

    public void PlayEnemyAnimation(string animationTrigger)
    {
        enemyAnimator.SetTrigger(animationTrigger);
    }
}
