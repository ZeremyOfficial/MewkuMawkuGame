using UnityEngine;
public class PlayerSwordController : MonoBehaviour
{
    public SwordAttack swordAttack;

    public void EnableSwordAttack(string direction)
    {
        swordAttack.EnableAttack(direction);
    }

    public void DisableSwordAttack()
    {
        swordAttack.DisableAttack();
    }
}