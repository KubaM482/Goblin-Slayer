using UnityEngine;

public class RunningState : IPlayerState
{

 public void Enter(PlayerMovement player)
    {
        
    }

    public void Update(PlayerMovement player)
    {
        

        if (player.moveInput == Vector2.zero || player.rb.linearVelocity.magnitude < 0.05f)
        {
            player.changeState(new IdleState());
        }

       
    }

    public void Exit(PlayerMovement player)
    {
        
    }

}