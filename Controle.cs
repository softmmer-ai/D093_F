using UnityEngine;

public class Controle : MonoBehaviour
{
    [SerializeField] private float flySpeed = 300f;
    [SerializeField] private float yawAmount = 120f;
    [SerializeField] private float pitchAmount = 45f;
    [SerializeField] private float rollAmount = 60f;

    private float yaw;
    private float pitch;
    private float roll;

    // Update is called once per frame
    void Update()
    {
        transform.position += flySpeed * Time.deltaTime * transform.forward;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        yaw += yawAmount * horizontalInput * Time.deltaTime;
        pitch = Mathf.Lerp(0, -pitchAmount, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        roll = Mathf.Lerp(0, rollAmount, Mathf.Abs(horizontalInput)) * Mathf.Sign(horizontalInput);

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.back * roll);
    }
}
