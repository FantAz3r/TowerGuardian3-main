using System.Collections;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // Скорость вращения камеры (градусы в секунду)
    [SerializeField] private float zoomSpeed = 5f; // Скорость приближения/отдаления
    [SerializeField] private float minDistance = 5f; // Минимальная дистанция камеры от игрока
    [SerializeField] private float maxDistance = 15f; // Максимальная дистанция камеры от игрока

    private Transform player; // Игрок, вокруг которого вращаемся
    private float currentDistance;
    private bool isZooming = false;

    private int rotationDirection = 0;
    // 0 - без вращения, 
    // 1 - вращаем вправо,
    // -1 - вращаем влево

    // Угол текущего вращения (в градусах) камеры вокруг игрока по горизонтали
    private float currentAngle = 0f;

    private void Start()
    {
        player = ServiceLocator.Get<IGameFactory>().Player.transform;
        // Проинициализируем начальное расстояние камеры от игрока по горизонтальной плоскости
        Vector3 offset = transform.position - player.position;
        currentDistance = new Vector2(offset.x, offset.z).magnitude;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Инициализируем угол взгляда камеры относительно игрока
        currentAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        HandleRotationInput();

        if (!isZooming && rotationDirection != 0)
        {
            // Обновляем угол вращения на основе направления и скорости
            currentAngle += rotationSpeed * rotationDirection * Time.deltaTime;
            currentAngle = currentAngle % 360f;
        }

        // Вычисляем позицию камеры на основе текущего угла и расстояния от игрока
        float rad = currentAngle * Mathf.Deg2Rad;

        // Позиция камеры по горизонтали
        float camX = player.position.x + Mathf.Cos(rad) * currentDistance;
        float camZ = player.position.z + Mathf.Sin(rad) * currentDistance;

        // Высота камеры фиксирована относительно позиции игрока + 5 по Y
        float camY = player.position.y + 5f;

        transform.position = new Vector3(camX, camY, camZ);

        // Камера смотрит на игрока, но с учетом того, что камера выше на 5 по Y
        Vector3 lookTarget = new Vector3(player.position.x, player.position.y + 1.5f, player.position.z);
        transform.LookAt(lookTarget);

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DoZoom());
        }
    }

    private void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (rotationDirection == 1)
            {
                rotationDirection = 0;
            }
            else if (rotationDirection == 0)
            {
                rotationDirection = -1;
            }
            else if (rotationDirection == -1)
            {
                rotationDirection = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (rotationDirection == -1)
            {
                rotationDirection = 0;
            }
            else if (rotationDirection == 0)
            {
                rotationDirection = 1;
            }
            else if (rotationDirection == 1)
            {
                rotationDirection = 0;
            }
        }
    }

    private IEnumerator DoZoom()
    {
        isZooming = true;

        float targetDistance = Random.value > 0.5f ? maxDistance : minDistance;

        while (Mathf.Abs(currentDistance - targetDistance) > 0.1f)
        {
            currentDistance = Mathf.MoveTowards(currentDistance, targetDistance, zoomSpeed * Time.deltaTime);

            float rad = currentAngle * Mathf.Deg2Rad;
            float camX = player.position.x + Mathf.Cos(rad) * currentDistance;
            float camZ = player.position.z + Mathf.Sin(rad) * currentDistance;
            float camY = player.position.y + 5f;
            transform.position = new Vector3(camX, camY, camZ);

            Vector3 lookTarget = new Vector3(player.position.x, player.position.y + 1.5f, player.position.z);
            transform.LookAt(lookTarget);

            yield return null;
        }

        isZooming = false;
    }
}

