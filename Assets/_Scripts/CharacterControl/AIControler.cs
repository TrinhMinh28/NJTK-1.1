using Spine;
using UnityEngine;
using UnityEngine.XR;

public class AIControler : MonoBehaviour
{
    private InputAnimation inPutAnim;
    private AnimationControl Animcontrol;
    private Vector3 lastPosition; // vị trí cuối của nhân vật
    Rigidbody2D rigidbody2;
    private bool checkFacing;// Trạng thái hướng mặt nhân vật. 
    private bool isJumping = false;
    // ********* Các biến để bắn 
    public Transform gunShot;
    public GameObject bullet;
    float fierate = 0.5f;
    float nextfire = 0f;
    bool holdRun = false;
    float holdRunTime = 0f;
    float pressDuration = 0f; // time nhấn phím
    public float jumpForce = 12f; // lực nhảy 
    public float maxSpeed = 10f; // Tốc độ 
    bool isWaitingForSecondTap = false;
    bool ontheGround; // nằm trên mặt đất
    int jumIndex;
    public int ideIndex;
    float indexcheck;

    private void Start()
    {
        inPutAnim = GetComponent<InputAnimation>();
        lastPosition = transform.position; // vị trí của nhân vật lúc bắt đầu game
        rigidbody2 = GetComponent<Rigidbody2D>();
        Animcontrol = GetComponent<AnimationControl>();
        indexcheck = 0;
        ideIndex = 1;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)) // Chuột trái
        {
            Animcontrol.Invoke("AnimLeftMouse", 0f);
        }
        if (Input.GetMouseButtonDown(1)) // Chuột phải
        {
            Animcontrol.Invoke("AnimRightMouse", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (isWaitingForSecondTap)
            {
                jumIndex += 1;
               // Debug.Log("Nhấn hai lần!");
                isWaitingForSecondTap = false;
                Jump();
                Animcontrol.Invoke("AnimJump3", 0.1f);
            }
            else
            {
                if (!isJumping)
                {
                    isWaitingForSecondTap = true;
                    Jump();
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            holdRun = true;
            holdRunTime = Time.time;

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            holdRun = true;
            holdRunTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            holdRun = false;
            pressDuration = Time.time - holdRunTime; // Tính thời gian đã nhấn phím
                                                     // Debug.Log("KeyBoard pressDuration : " + pressDuration.ToString());
        }
        //  Debug.Log("indexAnimPlayer là : " + indexAnimPlayer);

    }
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2.velocity = new Vector2(move * maxSpeed, rigidbody2.velocity.y);
        //transform.position = transform.position + new Vector3(move*maxSpeed*Time.deltaTime, 0, 0); // Đây cũng là lẹnh di chuyển.
        if (move < 0 && !checkFacing)
        {
            flip();

        }
        if (move > 0 && checkFacing)
        {
            flip();

        }
        // bẵn từ bàn phím
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            fireBulet(); // Gọi bắn.
        }
        checkTrans();
    }
    public void flip()
    {
        checkFacing = !checkFacing;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }
    void fireBulet() // chức năng bắn 
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + fierate;
            if (checkFacing)
            {
                Instantiate(bullet, gunShot.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
            else if (!checkFacing)
            {
                Instantiate(bullet, gunShot.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }
    private void checkTrans()
    {
        Vector3 currentPosition = transform.position; // vị trí hiện tại của nhân vật 

        // Kiểm tra xem vị trí đã thay đổi hay chưa
        if (currentPosition != lastPosition) // nếu vị trí hiện tại của nhân vật khác với vị trí lúc trước thì .
        {
            // Gọi phương thức PlayAnimation để kích hoạt animation dựa trên thay đổi vị trí
            if (currentPosition.x != lastPosition.x)
            {
                // Debug.Log("Thay đổi vị trí theo trục x");
                float difference = Mathf.Abs(currentPosition.x - lastPosition.x); // tính toán tốc độ 
                if (difference <= 0.1f)
                {
                    if (ontheGround == true)
                    {
                        // Goi đi bộ
                        Animcontrol.Invoke("AnimWalk", 0f);
                        // Debug.Log("Tốc độ đang chậm");
                        if (!holdRun && pressDuration >= 0.7f)
                        {
                            //  Debug.Log("Thoả mãn gọi skid");
                            Animcontrol.Invoke("AnimSkid", 0f);
                        }
                    }
                }
                else
                {
                    // Debug.Log("Thoả mãn gọi run");
                    // Gọi phương thức PlayAnimation để kích hoạt animation tương ứng với thay đổi vị trí theo trục x
                    if (ontheGround == true)
                    {
                        Animcontrol.Invoke("AnimRun", 0f);
                    }
                }
            }
            else if (currentPosition.y != lastPosition.y)
            {
                //Debug.Log("Thay đổi vị trí theo trục y");
                if (currentPosition.y > lastPosition.y)
                {
                    if (Mathf.Abs(currentPosition.y - lastPosition.y) >= 0.2f) // nhảy mạnh
                    {
                        // Debug.Log("Vị trí theo trục y tăng");
                        // Gọi phương thức PlayAnimation để kích hoạt animation tương ứng với việc tăng vị trí theo trục y
                        Animcontrol.Invoke("AnimJump", 0f);
                    }
                }
                else if (Mathf.Abs(currentPosition.y - lastPosition.y) >= 0.1f)
                { 
                    if (jumIndex == 1)
                    {
                        jumIndex += 1;
                        Animcontrol.Invoke("AnimJump2", 0f);
                    }
                }
            }
        }
        else if (currentPosition.x == lastPosition.x && currentPosition.y == lastPosition.y && ontheGround == true && Animcontrol.getindex() == 1)
        {
              //  Animcontrol.setindex(0) ;
            if (ideIndex ==1 )
            {
                ideIndex += 1;
                Animcontrol.Invoke("AnimIdle", 1f);
                Debug.Log("ide dc goi");
            }
                
        }
        lastPosition = currentPosition;
    }
    private void Jump()
    {
        rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, jumpForce);
        isJumping = true;
        ontheGround = false;
    }
 
    private void OnCollisionStay2D(Collision2D collision) // Thay bang OnCollisionStay2D(Collision2D x ) de nhay 2 llan
    {
        // Kiểm tra nếu nhân vật đang trong trạng thái nhảy
        // và va chạm với một đối tượng khác (đất, platform, v.v.)
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //
        {
            isJumping = false;
            ontheGround = true;
        }
        Debug.Log("Đang trên đất ");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animcontrol.Invoke("AnimDefault", 0f);
        Animcontrol.Invoke("AnimLand", 0f);
        Animcontrol.Invoke("AnimDefault", 0.3f);
        Debug.Log("Vừa chạm đất ");
        // animationManager.PlayAnimation("land".ToString(), false, false);// cham đất gọi animation land
        isWaitingForSecondTap = false;
        jumIndex = 1;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        ontheGround = false;
        isJumping = true;
        Debug.Log(" Đã rời mặt đất ");
    }

}