using UnityEngine;

public class PlayerRunningState : IPlayerState
{
    public float Speed => GameManager.Instance.PlayerStatsData.runningSpeed;

    public void EnterState(PlayerStateManager manager)
    {
        Debug.Log("Entering Runing State");
        manager.MoveProvider.moveSpeed = Speed;
        AudioManager.Instance.Play("PlayerRun");
    }

    public void UpdateState(PlayerStateManager manager)
    {
        if (manager.IsStealthMode)
        {
            manager.SwitchState(new PlayerStealthState());
        }

        if (!manager.IsRunningMode || manager.MoveInput.magnitude <= 0.1f)
        {
            manager.SwitchState(new PlayerIdleState());
        }
    }

    public void ExitState(PlayerStateManager manager)
    {
        AudioManager.Instance.Stop("PlayerRun");
        manager.IsRunningMode = false;
        Debug.Log("Exiting Runing State");
    }
}
