using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Animator animator;

    [SerializeField]
    float movementSpeed = 5f;

    Vector2 movement;

    [Header("Stamina")]
    [SerializeField]
    float stamina = 0;
    [SerializeField]
    float maxStamina = 2f;
    [SerializeField]
    float speedMultipler = 1.0f;
    [SerializeField]
    float walkSpeedValue = 1.0f;
    [SerializeField]
    float runSpeedValue = 1.5f;
    [SerializeField]
    float staminaRegenDelay;
    [SerializeField]
    bool canRegenerateStamina;
    [Header("Sounds")]
    [SerializeField]
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stamina = maxStamina;
        speedMultipler = walkSpeedValue;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (stamina > maxStamina / 10)
            {
                speedMultipler = runSpeedValue;
                canRegenerateStamina = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMultipler = walkSpeedValue;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if(movement.sqrMagnitude > 0.1f && audioSource.isPlaying==false)
        {
            audioSource.volume = Random.Range(0.2f, .3f);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }
        CalculateStamina();
        if(canRegenerateStamina)
        {
            RegenerateStamina();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * speedMultipler * Time.fixedDeltaTime);
    }

    void RegenerateStamina()
    {
        if(stamina>=maxStamina)
        {
            stamina = maxStamina;
            return;
        }
        stamina += Time.deltaTime/3.0f;
    }

    void CalculateStamina()
    {
        if (speedMultipler == runSpeedValue)
        {
            staminaRegenDelay = 3f;
            stamina -= Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                speedMultipler = walkSpeedValue;
            }
        }
        if (staminaRegenDelay > 0)
        {
            staminaRegenDelay-=Time.deltaTime;
        }
        if(staminaRegenDelay<=0)
        {
            canRegenerateStamina = true;
        }
    }
}
