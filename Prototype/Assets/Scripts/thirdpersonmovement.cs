using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator Anim;
    public float walkSpeed = 6f;
    bool isWalking;
    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        isWalking = false;
    }

    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            isWalking = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * walkSpeed * Time.deltaTime);
        }
    }
}
