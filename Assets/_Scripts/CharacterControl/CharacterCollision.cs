using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Tilemaps.Tile;

public class CharacterCollision : MonoBehaviour
{
    private CapsuleCollider2D characterCollider;
    public float DodaiTiaQuetUp;
    public float DodaiTiaQuetDown;
    public float DodaiTiaQuetLeftRight;
    public string  GroundQuet;
    LayerMask layerMask;
    public GameObject characterGameobject;
    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider;
    private bool groundUnder; // dưới chân
    private bool groundAbove; // trên dầu
    private void Awake()
    {
        layerMask = LayerMask.GetMask(GroundQuet);
        characterCollider = GameObject.Find("GameObject (Assassin)").GetComponent<CapsuleCollider2D>();
        characterGameobject = GameObject.Find("Character");
        tilemap = GameObject.Find("GroundMap"). GetComponent<Tilemap>();
        tilemapCollider =GameObject.Find("GroundMap"). GetComponent<TilemapCollider2D>();
    }

    private void Update()
    { 
        // Kiểm tra va chạm từ phía dưới
        CheckCollisionBelow();

        // Kiểm tra va chạm từ phía trên
       CheckCollisionAbove();


        // Kiểm tra va chạm từ bên trái
       // CheckCollisionLeft();

        // Kiểm tra va chạm từ bên phải
       // CheckCollisionRight();
    }
    private void FixedUpdate()
    {
        if (groundUnder && !groundAbove)
        {
            characterCollider.enabled = true;
        }
        else if (groundUnder && groundAbove)
        {
            characterCollider.enabled = false;
        }
        else if (!groundUnder && groundAbove)
        {
            characterCollider.enabled = false;
        }
        else if (!groundUnder && !groundAbove)
        {
            characterCollider.enabled = true;
        }
        else
        {
            characterCollider.enabled = false;
        }
    }

    private void CheckCollisionAbove()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * DodaiTiaQuetUp, Color.yellow);
        // Vector2 raycastOrigin = characterCollider.bounds.center - Vector3.up * characterCollider.bounds.extents.x;

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection( Vector2.up), Mathf.Infinity, LayerMask.GetMask("GroundTile")); // Bắn tia  max
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection( Vector2.up), DodaiTiaQuetUp, layerMask); // Bắn dài 4f
        if (hit.collider != null)
        {
            groundAbove = true;
        }
        else
        {
            groundAbove = false;
        }
        // Ở đay có đoạn mã cũ 
        #region
        //RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection( Vector2.up), DodaiTiaQuet, layerMask); // Bắn dài 4f
        //foreach (RaycastHit2D hit in hits)
        //{
        //    if (hit.collider != null)
        //    {

        //        Debug.Log("Tim thay tren" + hit.collider.name + "dai " + hit.distance);
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hit.distance, Color.red);
        //        Vector3Int tilePosition = tilemap.WorldToCell(hit.point);
        //        TileBase tileBase = tilemap.GetTile(tilePosition);
        //        TileBase tileBase1 = tilemap.GetTile(tilePosition + new Vector3Int(0, 1, 0));
        //        Debug.Log("toa do tim tile" + tilePosition); // tọa độ position
        //        Debug.Log("toa do tim tile1" + (tilePosition + new Vector3Int(0, 1, 0))); // tọa độ position
        //        if (tileBase != null) // Nếu tile này không có thì tìm tile bên trên
        //        {

        //            // Chuyển đổi tile từ TileBase sang Tile
        //            Tile tile = tileBase as Tile;
        //            Debug.Log("Tim toa do tile" + tilePosition);
        //            // Kiểm tra nếu tile có Collider2D
        //            // Kiểm tra nếu tile không null và có colliderType là Sprite
        //            if (tile != null && tile.colliderType == ColliderType.Sprite)
        //            {
        //                tile.colliderType = ColliderType.None;

        //                // Cập nhật lại tile trên tilemap
        //                //  tilemap.SetTile(tilePosition, tile);

        //                tilemap.SetTileFlags(tilePosition, TileFlags.None);
        //                tilemap.RefreshTile(tilePosition);
        //            }
        //            #region
        //            //TileBase topTile = tilemap.GetTile(tilePosition + new Vector3Int(0, 1, 0)); // Tile phía trên
        //            //if (topTile == null)
        //            //{
        //            //    return;
        //            //}
        //            //else
        //            //{
        //            //    // Chuyển đổi tile từ TileBase sang Tile
        //            //    Tile tile = topTile as Tile;
        //            //    Debug.Log("Tim toa do top tile" + tilePosition);
        //            //    // Kiểm tra nếu tile có Collider2D
        //            //    // Kiểm tra nếu tile không null và có colliderType là Sprite
        //            //    if (tile != null && tile.colliderType == ColliderType.Sprite)
        //            //    {
        //            //        tile.colliderType = ColliderType.None;

        //            //        // Cập nhật lại tile trên tilemap
        //            //        //  tilemap.SetTile(tilePosition, tile);
        //            //        tilemap.RefreshTile(tilePosition);
        //            //        tilemap.SetTileFlags(tilePosition, TileFlags.None);
        //            //    }
        //            //}
        //            #endregion
        //        }

        //        if (tileBase1 != null)
        //        {
        //            // Chuyển đổi tile từ TileBase sang Tile
        //            Tile tile1 = tileBase1 as Tile;
        //            Debug.Log("toa do tim tile1" + (tilePosition + new Vector3Int(0, 1, 0))); // tọa độ position
        //            // Kiểm tra nếu tile có Collider2D
        //            // Kiểm tra nếu tile không null và có colliderType là Sprite
        //            if (tile1 != null && tile1.colliderType == ColliderType.Sprite)
        //            {
        //                tile1.colliderType = ColliderType.None;

        //                // Cập nhật lại tile trên tilemap
        //                //  tilemap.SetTile(tilePosition, tile);
        //                tilemap.SetTileFlags(tilePosition + new Vector3Int(0, 1, 0), TileFlags.None);
        //                tilemap.RefreshTile(tilePosition + new Vector3Int(0, 1, 0));
        //            }
        //        }

        //    }
        //}
        #endregion 

    }

    private void CheckCollisionBelow()
    {
        // Vector2 raycastOrigin = characterCollider.bounds.center - Vector3.up * characterCollider.bounds.extents.x;

       // RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector2.down), DodaiTiaQuetUpDown, layerMask); // Bắn dài 4f
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * DodaiTiaQuetDown, Color.yellow);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), DodaiTiaQuetDown, layerMask); // Bắn dài f
        if (hit.collider !=null)
        {
            groundUnder = true;
        }
        else
        {
            groundUnder = false;
        }
        // ở đây còn mã cũ
        #region
        //foreach (RaycastHit2D hit in hits)
        //{
        //    if (hit.collider != null)
        //    {

        //        Debug.Log("Tim thay tren" + hit.collider.name + "dai " + hit.distance);
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * hit.distance, Color.red);
        //        Vector3Int tilePosition = tilemap.WorldToCell(hit.point);
        //        TileBase tileBase = tilemap.GetTile(tilePosition);
        //        TileBase tileBase1 = tilemap.GetTile(tilePosition + new Vector3Int(0, -1, 0));
        //        Debug.Log("toa do tim tile" + tilePosition); // tọa độ position
        //        Debug.Log("toa do tim tile1" + (tilePosition + new Vector3Int(0, -1, 0))); // tọa độ position
        //        if (tileBase != null) // Nếu tile này không có thì tìm tile bên trên
        //        {

        //            // Chuyển đổi tile từ TileBase sang Tile
        //            Tile tile = tileBase as Tile;
        //            Debug.Log("Tim toa do tile" + tilePosition);
        //            // Kiểm tra nếu tile có Collider2D
        //            // Kiểm tra nếu tile không null và có colliderType là Sprite
        //            if (tile != null && tile.colliderType == ColliderType.None)
        //            {
        //                tile.colliderType = ColliderType.Sprite;

        //                // Cập nhật lại tile trên tilemap
        //                //  tilemap.SetTile(tilePosition, tile);
        //                tilemap.SetTileFlags(tilePosition, TileFlags.None);
        //                tilemap.RefreshTile(tilePosition);
        //            }

        //        }

        //        if (tileBase1 != null)
        //        {
        //            // Chuyển đổi tile từ TileBase sang Tile
        //            Tile tile1 = tileBase1 as Tile;
        //            Debug.Log("toa do tim tile1" + (tilePosition + new Vector3Int(0, -1, 0))); // tọa độ position
        //            // Kiểm tra nếu tile có Collider2D
        //            // Kiểm tra nếu tile không null và có colliderType là Sprite
        //            if (tile1 != null && tile1.colliderType == ColliderType.None)
        //            {
        //                tile1.colliderType = ColliderType.Sprite;

        //                // Cập nhật lại tile trên tilemap
        //                //  tilemap.SetTile(tilePosition, tile);
        //                tilemap.SetTileFlags(tilePosition + new Vector3Int(0, -1, 0), TileFlags.None);
        //                tilemap.RefreshTile(tilePosition + new Vector3Int(0, -1, 0));
        //            }
        //        }

        //    }
        //}
        #endregion
    }

    private void CheckCollisionLeft()
    {
        float raycastDistance = 0.1f;
        Vector2 raycastOrigin = characterCollider.bounds.center - Vector3.right * characterCollider.bounds.extents.x;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.left, raycastDistance, LayerMask.GetMask("GroundTile"));
        if (hit.collider != null)
        {
            // Nhân vật đang va chạm với Tilemap Collider từ phía bên trái
            // Việc xử lý va chạm từ bên trái có thể được thực hiện ở đây
            Debug.Log("Va chạm từ bên trái");
        }
    }

    private void CheckCollisionRight()
    {
        float raycastDistance = 0.1f;
        Vector2 raycastOrigin = characterCollider.bounds.center + Vector3.right * characterCollider.bounds.extents.x;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.right, raycastDistance, LayerMask.GetMask("GroundTile"));
        if (hit.collider != null)
        {
            // Nhân vật đang va chạm với Tilemap Collider từ phía bên phải
            // Việc xử lý va chạm từ bên phải có thể được thực hiện ở đây
            Debug.Log("Va chạm từ bên phải");
        }
    }
}