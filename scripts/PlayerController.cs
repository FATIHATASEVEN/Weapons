using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed=10f;
    public int PlayerHealth = 100;
    public float gravity = -14f;
    private Vector3 gravitVector;
    public Transform GroundCheckPoint;
    public float groundCheckRadius = 0.35f;
    public LayerMask groundLayer;
    public bool isGrounded = false;
    public float jumpspeed = 5f;
    public Slider healthSlider;
    public Text healthText;
    private GameManager gameManager;

    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        MovePlayer();
        GroundCheck();
        JumpandGravity();
    }
    public void MovePlayer()
    {
        Vector3 moveVector = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        characterController.Move(moveVector * speed * Time.deltaTime);
    }
    public void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(GroundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    public void JumpandGravity()
    {
        gravitVector.y += gravity * Time.deltaTime;
        characterController.Move(gravitVector * Time.deltaTime);

        if (isGrounded && gravitVector.y < 0)
        {
            gravitVector.y = -3f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravitVector.y = Mathf.Sqrt(jumpspeed * -2f * gravity);
        }
    }
   public void PlayerTakeDamage(int DamageAmount)
    {
        PlayerHealth -= DamageAmount;
        healthSlider.value -= DamageAmount;
        HealthTextUpdate();

        if(PlayerHealth<=0 )
        {
            PlayerDeath();
            HealthTextUpdate();
            healthSlider.value = 0;
        }
    }
    public void PlayerDeath()
    {
        gameManager.RestartGame();
    }
    void HealthTextUpdate()
    {
        healthText.text = PlayerHealth.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndTigger"))
        gameManager.WinLevel();
    }
}
