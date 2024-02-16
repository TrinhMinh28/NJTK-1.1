using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float destroyDelay ; // Thời gian trễ trước khi xóa (đơn vị: giây) 

    private void Start()
    {
        if (destroyDelay ==0)
        {
            destroyDelay = 1;
        }
        // Gọi hàm DestroySelf sau một khoảng thời gian quyết định bởi người dùng
        Invoke("DestroySelf", destroyDelay); // phương thức gọi 1 phương thức khác sau 1 time.
    }

    private void DestroySelf()
    {
        // Xóa game object chứa script sau khi thời gian trễ đã qua
        Destroy(gameObject);
    }
}
