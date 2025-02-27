using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Muove il giocatore in base all'input
            float moveHorizontal = Input.GetAxis("Horizontal"); // "A" e "D" o frecce sinistra e destra
            float moveVertical = Input.GetAxis("Vertical"); // "W" e "S" o frecce su e giù

            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Applica la gravità
        moveDirection.y -= gravity * Time.deltaTime;

        // Muove il controller del personaggio
        controller.Move(moveDirection * Time.deltaTime);
    }
}
