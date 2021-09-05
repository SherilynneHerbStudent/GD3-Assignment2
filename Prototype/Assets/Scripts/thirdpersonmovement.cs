using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thirdpersonmovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;
    public float speed = 2f;
    public float defaultWalkSpeed = 2f;
    public float runmultiplier;
    public float turnSmoothTime = 0.1f;
    public int stamina;
    public int maxStamina;
    public int staminaDrainPerSecond;
    public int staminaRegenPerSecond;
    public GameObject foxPlayer;

    public float Gravity = -9.8f;

    float turnSmoothVelocity;
    public Animator my_Animator;
    Vector3 vel;
    bool isGrounded;
    bool sprinting;
    public bool inBurrow;
    bool onBurrow;

    public Slider StaminaBar;
    public static thirdpersonmovement instance;
    public GameObject FoxHappyUI;
    public GameObject FoxTiredUI;
    public GameObject FoxHidingUI;

    public Transform groundCheck;
    LayerMask groundMask;

    private WaitForSeconds staminaDepletion = new WaitForSeconds(1f);
    private WaitForSeconds staminaRegen = new WaitForSeconds(2f);

    void Start()
    {
        my_Animator = GetComponent<Animator>();
        defaultWalkSpeed = speed;
        stamina = maxStamina;
        sprinting = false;
        inBurrow = false;
        onBurrow = false;
        StaminaBar.maxValue = maxStamina;
        StaminaBar.value = maxStamina;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        my_Animator.SetBool("isMoving", isWalking);
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isGrounded == false)
        {
            vel.y += Gravity * Time.deltaTime;
            controller.Move(vel * Time.deltaTime);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
            {
                sprinting = true;
                speed = defaultWalkSpeed * 2;
                my_Animator.SetBool("isMoving", false);
                my_Animator.SetBool("isRunning", isWalking);
                StartCoroutine(StaminaSystem());
            }
            
            else if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0) 
            {
               sprinting = false;
               my_Animator.SetBool("isRunning", false);
                FoxHappyUI.SetActive(false);
                FoxTiredUI.SetActive(true);
               speed = defaultWalkSpeed;

            }

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            
        }

        if (onBurrow == true && Input.GetKeyDown(KeyCode.E))
        {
            foxPlayer.SetActive(false);
            inBurrow = true;
        }

        if (inBurrow == true)
        {
            StartCoroutine(StaminaRegeneration());

            if (Input.GetKeyUp(KeyCode.E))
            {
                foxPlayer.SetActive(true);
                inBurrow = false;
                FoxHidingUI.SetActive(false);
                FoxHappyUI.SetActive(true);
            }

            else
            {
                foxPlayer.SetActive(false);
                inBurrow = true;
                FoxTiredUI.SetActive(false);
                FoxHidingUI.SetActive(true);
                FoxHappyUI.SetActive(false);
            }
        }

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Burrow"))
        {
            onBurrow = true;
        }

        else
        {
            onBurrow = false;
        }
    }

    IEnumerator StaminaSystem()
    {
        while (sprinting == true)
        {
            stamina -= staminaDrainPerSecond;
            StaminaBar.value = stamina;
            yield return staminaDepletion;
        }
    }

    IEnumerator StaminaRegeneration()
    {
        yield return new WaitForSeconds(2);

        while (inBurrow == true)
        {
            stamina += staminaRegenPerSecond;
            StaminaBar.value = stamina;
            yield return staminaRegen;
        }
    }


    
}
