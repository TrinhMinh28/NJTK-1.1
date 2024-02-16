using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdatePolygonCollider : MonoBehaviour
{
    public TilemapCollider2D tilemapCollider;
    public PolygonCollider2D polygonCollider;
    int Ran ;

    void Start()
    {
        Invoke("CreateCollider", 0.5f);
        //CreateCollider();
    }

    void CreateCollider()
    {
        if (Ran == 8)
        {
            // Kiểm tra xem Tilemap Collider và Polygon Collider đã được thêm vào
            if (tilemapCollider != null && polygonCollider != null)
            {
                // Lấy hình dạng collider từ Tilemap Collider
                CompositeCollider2D collider = tilemapCollider.GetComponent<CompositeCollider2D>();

                // Xóa các điểm cũ trong Polygon Collider
                polygonCollider.pathCount = 0;
                // Tạo một mảng chứa các đỉnh mới của Polygon Collider
                Vector2[] points = new Vector2[8];

                // Lấy viền bên trái và bên phải của Tilemap Collider
                points[0] = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
                points[1] = new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.min.y);
                points[2] = new Vector2(collider.bounds.max.x, collider.bounds.min.y);

                // Lấy viền bên dưới và bên trên của Tilemap Collider
                points[3] = new Vector2(collider.bounds.max.x, (collider.bounds.min.y + collider.bounds.max.y) / 2);
                points[4] = new Vector2(collider.bounds.max.x, collider.bounds.max.y);
                points[5] = new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.max.y);

                // Lấy viền bên phải và bên trái của Tilemap Collider
                points[6] = new Vector2(collider.bounds.min.x, collider.bounds.max.y);

                points[7] = new Vector2(collider.bounds.min.x, (collider.bounds.min.y + collider.bounds.max.y) / 2);

                // Thiết lập các đỉnh mới vào Polygon Collider
                polygonCollider.SetPath(0, points);

            }
            Ran = 7;
            Invoke("CreateCollider", 0.5f);

        }
        else
        {
            // Kiểm tra xem Tilemap Collider và Polygon Collider đã được thêm vào
            if (tilemapCollider != null && polygonCollider != null)
            {
                // Lấy hình dạng collider từ Tilemap Collider
                CompositeCollider2D collider = tilemapCollider.GetComponent<CompositeCollider2D>();

                // Xóa các điểm cũ trong Polygon Collider
                polygonCollider.pathCount = 0;
                // Tạo một mảng chứa các đỉnh mới của Polygon Collider
                Vector2[] points = new Vector2[7];

                // Lấy viền bên trái và bên phải của Tilemap Collider
                points[0] = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
                points[1] = new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.min.y);
                points[2] = new Vector2(collider.bounds.max.x, collider.bounds.min.y);

                // Lấy viền bên dưới và bên trên của Tilemap Collider
                points[3] = new Vector2(collider.bounds.max.x, (collider.bounds.min.y + collider.bounds.max.y) / 2);
                points[4] = new Vector2(collider.bounds.max.x, collider.bounds.max.y);
                points[5] = new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, collider.bounds.max.y);

                // Lấy viền bên phải và bên trái của Tilemap Collider
                points[6] = new Vector2(collider.bounds.min.x, collider.bounds.max.y);

              //  points[7] = new Vector2(collider.bounds.min.x, (collider.bounds.min.y + collider.bounds.max.y) / 2);

                // Thiết lập các đỉnh mới vào Polygon Collider
                polygonCollider.SetPath(0, points);

            }
            Ran = 8;
            Invoke("CreateCollider", 0.5f);

        }

    }
}
