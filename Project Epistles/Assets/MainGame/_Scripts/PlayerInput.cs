using UnityEngine;
using System;
using System.Collections;
using Gamekit3D;
using Cinemachine;

public class PlayerInput : MonoBehaviour
{

    [Header("Controls")]
    public Joystick leftStick;
    public float leftStickHorizontalSensitivity;
    public float leftStickVerticalSensitivity;
    public Joystick rightStick;
    public float rightStickHorizontalSensitivity;
    public float rightStickVerticalSensitivity;

    [Header("Camera")]
    //public float speed = 7.5f;
    //public float jumpSpeed = 8.0f;
    //public float gravity = 20.0f;
    //public Transform playerCameraParent;
    //public float lookSpeed = 2.0f;
    //public float lookXLimit = 60.0f;
    //private float XRotation = 0.0f;
    //Vector2 rotation = Vector2.zero;
    public CinemachineFreeLook freeLookCam;
    CinemachineOrbitalTransposer xAxis;

    [Header("ButtonObjects")]
    public GameObject miniMap;
    public GameObject inventoryCanvas;

    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;

    public Vector2 MoveInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera;
        }
    }

    public bool JumpInput
    {
        get { return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Attack
    {
        get { return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Pause
    {
        get { return m_Pause; }
    }

    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;
    const float k_AttackInputDuration = 0.03f;


    WaitForSeconds m_JumpInputWait;
    Coroutine m_JumpWaitCoroutine;
    const float k_JumpInputDuration = 0.5f;

    void Awake()
    {

        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);
        m_JumpInputWait = new WaitForSeconds(k_JumpInputDuration);

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
    }


    void Update()
    {
        //m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //m_Jump = Input.GetButton("Jump");

        // movement
        float x = leftStick.Horizontal;
        float z = leftStick.Vertical;
        //Vector3 move = transform.right * x + transform.forward * z;
        m_Movement.Set(x, z);
        //controller.Move(move * maxSpeed * Time.deltaTime);

        // camera
        float mouseX = rightStick.Horizontal * rightStickHorizontalSensitivity;
        float mouseY = rightStick.Vertical * rightStickVerticalSensitivity;
        //m_Camera.Set(mouseX, mouseY);
        freeLookCam.m_XAxis.Value += mouseX * 3f;
        freeLookCam.m_YAxis.Value -= mouseY * 0.1f;
        //Camera.main.transform.Rotate(m_Camera);
        //rotation.y += rightStick.Horizontal * lookSpeed;
        //rotation.x += -rightStick.Vertical * lookSpeed;
        //rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        //playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        //transform.eulerAngles = new Vector2(0, rotation.y);


        // jump
        //m_Jump = Input.GetButton("Jump");

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (m_AttackWaitCoroutine != null)
        //        StopCoroutine(m_AttackWaitCoroutine);

        //    m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        //}

        m_Pause = Input.GetButtonDown ("Pause");
    }


    public void DoJump()
    {
 
        if (m_JumpWaitCoroutine != null)
            StopCoroutine(m_JumpWaitCoroutine);

        m_JumpWaitCoroutine = StartCoroutine(JumpWait());
    }

    IEnumerator JumpWait()
    {
        m_Jump = true;

        yield return m_AttackInputWait;

        m_Jump = false;
    }

    public void ToggleMinimap()
    {
        // toggle the MiniMap on/off
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }

    public void ToggleInventory()
    {
        // toggle the inventory panel on/off
        inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
    }

    public void DoAttack()
    {
        if (m_AttackWaitCoroutine != null)
            StopCoroutine(m_AttackWaitCoroutine);

        m_AttackWaitCoroutine = StartCoroutine(AttackWait());
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;

        yield return m_AttackInputWait;

        m_Attack = false;
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }
}
