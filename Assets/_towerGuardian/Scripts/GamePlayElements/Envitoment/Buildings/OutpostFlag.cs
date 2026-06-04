using UnityEngine;

[RequireComponent(typeof(Outpost))]
public class OutpostFlag : MonoBehaviour
{
    [SerializeField] private GameObject _flag;
    [SerializeField] private Renderer _flagRenderer;

    [SerializeField] private Vector3 _flagUpPosition = new Vector3(0, 1, 0);
    [SerializeField] private Vector3 _flagDownPosition = new Vector3(0, 0, 0);

    [SerializeField] private Outpost _outpost;    

    private void Awake()
    {
        _outpost.TimerUpdated += UpdateFlag;
        _outpost.Complited += OnComplele;

        _flag.transform.localPosition = _flagUpPosition;
        SetFlagEmissionColor(Color.red);
    }

    private void UpdateFlag(float currentTime, float targetTime)
    {
        float halfTime = targetTime / 2f;

        if (currentTime >= halfTime)
        {
            float t = (currentTime - halfTime) / halfTime;
            _flag.transform.localPosition = Vector3.Lerp(_flagDownPosition, _flagUpPosition, t);
            SetFlagEmissionColor(Color.Lerp(Color.green, Color.green, t)); 
        }
        else
        {
            float t = currentTime / halfTime;
            _flag.transform.localPosition = Vector3.Lerp(_flagUpPosition, _flagDownPosition, t);
            SetFlagEmissionColor(Color.Lerp(Color.red, Color.green, t));
        }
    }

    private void OnComplele()
    {
        _outpost.Complited -= OnComplele;
        _outpost.TimerUpdated -= UpdateFlag;
    }

    private void SetFlagEmissionColor(Color color)
    {
        if (_flagRenderer != null)
        {
            Material material = _flagRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", color);
        }
    }
}
