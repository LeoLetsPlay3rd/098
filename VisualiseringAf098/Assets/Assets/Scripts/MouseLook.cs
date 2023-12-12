using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player or camera based on the mouse input
        // Adjust sensitivity to your preference
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around its local axes
        float newRotationX = transform.localEulerAngles.x - mouseY;
        newRotationX = Mathf.Clamp(newRotationX, 0f, 30f); // Adjust the values as needed
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0f);
    }
}
