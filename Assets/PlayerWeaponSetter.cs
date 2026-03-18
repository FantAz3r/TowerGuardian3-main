using UnityEngine;

public class PlayerWeaponSetter : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("HasWeapon", true);
    }
}
