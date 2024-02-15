using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public float initialSpeed = 50f; // Tốc độ ban đầu
    public float speedMultiplier = 3f; // Hệ số tăng tốc độ
    public float delayBeforeStart = 0.5f; // Độ trễ trước khi bắt đầu di chuyển
    public float destroyDelay = 4f; // Độ trễ trước khi hủy đối tượng

    private RectTransform rectTransform;
    private float currentSpeed;
    private float elapsedTime;

    private void Start()
    {
        rectTransform = GameManager.Instance.FindObjectInChildren(transform, "ThongBaoText").GetComponent<RectTransform>();
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= delayBeforeStart)
        {
            // Lấy vị trí hiện tại của RectTransform
            Vector3 currentPosition = rectTransform.localPosition;

            // Tính toán vị trí mới bằng cách trừ giá trị speed từ vị trí hiện tại
            Vector3 newPosition = new Vector3(currentPosition.x - (currentSpeed * Time.deltaTime), currentPosition.y, currentPosition.z);

            // Cập nhật vị trí mới cho RectTransform
            rectTransform.localPosition = newPosition;

            // Tăng tốc độ sau 3s
            if (elapsedTime >= delayBeforeStart + 3f && currentSpeed == initialSpeed)
            {
                currentSpeed *= speedMultiplier;
            }

            // Hủy đối tượng sau 4s
            if (elapsedTime >= delayBeforeStart + destroyDelay)
            {
                Destroy(gameObject);
            }
        }
    }
}
