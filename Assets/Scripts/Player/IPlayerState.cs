using UnityEngine;

public interface IPlayerState
{
    void Enter(PlayerMovement player);
    void Update(PlayerMovement player);
    void Exit(PlayerMovement player);
}