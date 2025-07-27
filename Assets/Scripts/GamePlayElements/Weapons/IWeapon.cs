using UnityEngine;

public interface IWeapon
{
    void Init(Transform attackPoint, AttackZone attackZone);
    void Attack();
    void TakeOff();
}