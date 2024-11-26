using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerStealthState : IPlayerState
{
    [SerializeField] private DynamicMoveProvider moveProvider;
    [SerializeField] float beforePlayerSpeed;
    [SerializeField] float StealthSpeed = 0.5f;

    public void EnterState(PlayerStateManager manager)
    {
        Debug.Log("Entering Stealth State");

        moveProvider = manager.moveProvider;

        moveProvider.GetComponent<DynamicMoveProvider>();

        beforePlayerSpeed = moveProvider.moveSpeed;
        moveProvider.moveSpeed = StealthSpeed;
    }

    public void UpdateState(PlayerStateManager manager)
    {
       /* if (manager.MoveInput.magnitude <= 0.1f)
        {
            manager.SwitchState(new PlayerIdleState());
        }*/
        if (!manager.IsStealthMode)
        {
            manager.SwitchState(new PlayerIdleState());
        }
    }

    public void ExitState(PlayerStateManager manager)
    {
        // 스텔스 해제 
        Debug.Log("Exiting Stealth State");
        moveProvider.moveSpeed = beforePlayerSpeed;
    }
}