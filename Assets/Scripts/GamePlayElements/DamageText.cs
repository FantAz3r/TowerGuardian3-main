using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;
    private Vector3 _offset= new Vector3(0, 1.5f, -1.5f);
    private IDemageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _damageable.HealthLost += Show;
    }

    private void OnDisable()
    {
        _damageable.HealthLost -= Show;
    }

    private void Show(float damage)
    {
        GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position + _offset, Quaternion.identity);
        TMP_Text tmpText = damageTextObject.GetComponentInChildren<TMP_Text>();
        FaceCamera(damageTextObject);
        tmpText.text = damage.ToString();
        tmpText.fontSharedMaterial.renderQueue = 4000;

        Animator animator = damageTextObject.GetComponentInChildren<Animator>();
        Destroy(damageTextObject, GetAnimationDuration(animator));
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

    private void FaceCamera(GameObject damageTextObject)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            damageTextObject.transform.LookAt(mainCamera.transform);
            damageTextObject.transform.Rotate(0, 180, 0);
        }
    }
}