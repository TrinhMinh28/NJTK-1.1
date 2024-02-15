using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing; // độ mượt 
    Vector3 offset;
    float lowY;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position; // lấy khoảng cách từ Cam - nhân vật
        lowY = transform.position.y;
    }
    private void Update()
    {
        Vector3 cam2Position = target.position + offset; // vị trí cam 2 mới sẽ bằng vị trí nhân vật thứ 2 + khoảng cách 
        transform.position = Vector3.Lerp(transform.position, cam2Position, smoothing); // thay vị trí của cam thành vị trí mới
        if (transform.position.y < lowY)
        {
            transform.position =  new Vector3 (transform.position.x, lowY, transform.position.z); 
        } if (transform.position.y > lowY)
        {
            transform.position =  new Vector3 (transform.position.x, lowY, transform.position.z); 
        }
    }

}
