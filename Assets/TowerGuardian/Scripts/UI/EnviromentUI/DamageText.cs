using System.Collections;
using UnityEngine;

namespace TowerGuardian.Scripts.UI.EnviromentUI
{
    public class DamageText : MonoBehaviour
    {
        private WaitForSeconds _sleep;

        private void Awake()
        {
            Animator animator = GetComponentInChildren<Animator>();
            _sleep = new WaitForSeconds(GetAnimationDuration(animator));
        }

        private void OnEnable()
        {
            FaceCamera(this);
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return _sleep;
            gameObject.SetActive(false);
        }

        private void FaceCamera(DamageText damageTextObject)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                damageTextObject.transform.LookAt(mainCamera.transform);
                damageTextObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private float GetAnimationDuration(Animator animator)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo.Length > 0)
            {
                return clipInfo[0].clip.length;
            }

            return 0f;
        }
    }
}
