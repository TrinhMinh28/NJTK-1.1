using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    // Getter cho instance
    public static EnemyManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<EnemyManager>();
                    singletonObject.name = "EnemyManager (Generated)";
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Đảm bảo chỉ có một instance duy nhất
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Các phương thức và thuộc tính của EnemyManager
    // ...
}
