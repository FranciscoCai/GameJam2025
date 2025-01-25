using UnityEngine;

public class RotacionCamara : MonoBehaviour
{
    public Vector3 rotationA;
    public Vector3 rotationD;
    public Vector3 rotationDefault;
    public float rotationSpeed;

    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler(rotationDefault);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(rotationA);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(rotationD);
        }
        else
        {
            targetRotation = Quaternion.Euler(rotationDefault);
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

