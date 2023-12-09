using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public PlayerSwordController swordController;

    public void EnableSwordAttackFromAnimation(string direction)
    {
        if (swordController != null)
        {
            swordController.EnableSwordAttack(direction);
        }
    }

    public void DisableSwordAttackFromAnimation()
    {
        if (swordController != null)
        {
            swordController.DisableSwordAttack();
        }
    }
}