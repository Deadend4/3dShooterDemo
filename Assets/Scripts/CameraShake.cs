using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    void Start()
    {

    }
    public void ShakeRotateCamera(float duration, float angleDeg, Quaternion direction, GameObject root)
    {
        //Запускаем корутину вращения камеры
        StartCoroutine(ShakeRotateCor(duration, angleDeg, direction, root));
    }


    private IEnumerator ShakeRotateCor(float duration, float angleDeg, Quaternion direction, GameObject root)
    {
        //Счетчик прошедшего времени
        float elapsed = 0f;
        //Запоминаем начальное вращение камеры по аналогии с вибрацией камеры
        Quaternion startRotation = root.transform.localRotation;

        //Для удобства добавляем переменную середину нашего таймера
        //Ибо сначала отклонение будет идти на увеличение, а затем на уменьшение
        float halfDuration = duration / 2;
        //Приводим направляющий вектор к единичному вектору, дабы не портить вычисления
        direction = direction.normalized;
        while (elapsed < duration)
        {
            //Сохраняем текущее направление ибо мы будем менять данный вектор
            Quaternion currentDirection = direction;
            //Подсчёт процентного коэффициента для функции Lerp[0..1]
            //До середины таймера процент увеличивается, затем уменьшается
            float t = elapsed < halfDuration ? elapsed / halfDuration : (duration - elapsed) / halfDuration;
            //Текущий угол отклонения
            float currentAngle = Mathf.Lerp(0f, angleDeg, t);
            //Вычисляем длину направляющего вектора из тангенса угла.
            //Недостатком данного решения будет являться то
            //Что угол отклонения должен находится в следующем диапазоне (0..90)
            // currentDirection *= Mathf.Tan(currentAngle * Mathf.Deg2Rad);
            //Сумма векторов - получаем направление взгляда на текущей итерации
            Vector3 resDirection = (currentDirection.eulerAngles + Vector3.forward).normalized;
            //С помощью Quaternion.FromToRotation получаем новое вращение
            //Изменяем локальное вращение, дабы во время вращения, если игрок будет управлять камерой
            //Все работало корректно
            root.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, resDirection);

            elapsed += Time.deltaTime;
            yield return null;
        }
        //Восстанавливаем вращение
        root.transform.localRotation = startRotation;
    }
}
