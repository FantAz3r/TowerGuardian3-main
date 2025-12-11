using System;

public interface IDamageAbility 
{
    event Action<float> DialedDamage;
    void OnHit(int damage);
}
