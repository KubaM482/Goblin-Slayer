using UnityEngine;

public class AttackingState : IPlayerState
{
    public void Enter(PlayerMovement player)
    {
        player.anim.SetTrigger("Attacking");
        player.AttackResetCooldown();
    }

    public void Update(PlayerMovement player)
    {
        if (!player.isAttackOnCooldown)
        {
            player.changeState(player.moveInput == Vector2.zero ? new IdleState() : new RunningState());
         }


    }

    public void Exit(PlayerMovement player)
    {
        
    }
    
}