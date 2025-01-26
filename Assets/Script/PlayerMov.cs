using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed = 5f;
    public float multiplicadorSprint = 2f;
    public float speedLateral = 3f;

    private Rigidbody rb;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= multiplicadorSprint;
        }

        float strafe = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            strafe = -speedLateral;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            strafe = speedLateral;
        }

        Vector3 move = (transform.forward * currentSpeed) + (transform.right * strafe);
        
        controller.Move(move * Time.deltaTime);
    }
}
