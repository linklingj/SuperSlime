using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float Movespeed = 3.0f;
    public float airSpeed = 1;
    public float jumpForceFull = 18.0f;
    public float jumpForceShort = 12.0f;
    public float jumpSquatForce = 0.5f;
    public float turnSmoothTime;
    public bool ControlsOn = true;
    private Rigidbody rootRigidbody;
    private Rigidbody PlayerBody;
    private BoxCollider bottomCol;
    public GameObject joints;
    public GameObject[] points;
    [SerializeField] private Transform PlayerCamera;
    public GroundCheck groundCheck;

    private bool grounded = false;
    bool noJump;
    bool fullJump = true;
    private Vector3 PlayerMovementInput;
    private Vector3 velocity = Vector3.zero;
    private float turnSmoothVelocity;
    //public Rigidbody rootRigidBody;
    private void Start() {
        rootRigidbody = points[0].GetComponent<Rigidbody>();
        PlayerBody = gameObject.GetComponent<Rigidbody>();
        bottomCol = gameObject.GetComponent<BoxCollider>();
    }
    private void Update() {
        grounded = groundCheck.grounded;
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
        MovePlayer();
    }
    private void MovePlayer()
    {
        float speed = (grounded)? Movespeed : airSpeed;
        //바라봐야할 방향을 지정
        float targetAngle = Mathf.Atan2(PlayerMovementInput.x, PlayerMovementInput.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;
        //바라봐야할 방향까지 자연스럽게 변하도록 함
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //현재 바라보고 있는 방향을 기준으로 움직일 실질적 방향을 구함
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * Time.deltaTime;
        //이 게임오브젝트를 움직임
        PlayerBody.velocity = new Vector3(moveDir.x, rootRigidbody.velocity.y, moveDir.z);
        //루트노드가 이 게임오브젝트를 자연스럽게 따라가도록 함
        points[0].transform.position = Vector3.SmoothDamp(points[0].transform.position,transform.position,ref velocity,0.1f);
    }
    private void FixedUpdate()
    {
        if (ControlsOn == false)
            return;

        //this.transform.Translate(Vector3.forward * vert * Movespeed * Time.deltaTime);
        //points[0].transform.localRotation *= Quaternion.AngleAxis(PlayerMovementInput.x * Turnspeed * Time.deltaTime, Vector3.up);

        if (Input.GetButtonDown("Jump") && !noJump && grounded)
        {
            points[6].GetComponent<Rigidbody>().AddForce(-Vector3.up * 10f * jumpSquatForce, ForceMode.Impulse);
            noJump = true;
            fullJump = true;
            StartCoroutine("JumpPressed");
        }
        if(Input.GetButtonUp("Jump"))
        {
            fullJump = false;
        }
    }
    IEnumerator JumpPressed()
    {
        yield return new WaitForSeconds(0.3f);
        if(fullJump)
            rootRigidbody.AddForce(Vector3.up * 10f * jumpForceFull,ForceMode.Impulse);
        else
            rootRigidbody.AddForce(Vector3.up * 10f * jumpForceShort,ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        noJump = false;
    }
}

