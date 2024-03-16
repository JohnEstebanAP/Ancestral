using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float Xmove;
    private float Ymove;
    public float yRotationCamera = 0f;
    [SerializeField] private Transform PlayerBody;

    public Transform cameraTransForm;
    public FixedJoystick joystickRotate;
    public bool isMouseActive;
    public Vector2 LookAxis;
    public float Sensivity = 40f;
    public float shoulderHeight = -0.5f; // La altura del hombro del personaje
    public float shoulderDistance = 0.5f; // La distancia desde el hombro al centro del personaje
    public float defaultCameraHeight = 5f; // La altura de la cámara por defecto
    public float defaultCameraDistance = -9f; // La distancia de la cámara por defecto

    void Update()
    {
        onMoveView();
    }

    private void onMoveView()
    {
        if (isMouseActive)
        {
            Xmove = Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
            Ymove = Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;
        }
        else if (joystickRotate.isActiveAndEnabled && LookAxis == Vector2.zero)
        {
            Xmove = joystickRotate.Horizontal * Time.deltaTime * Sensivity;
            Ymove = joystickRotate.Vertical * Time.deltaTime * Sensivity;
        }
        else
        {
            Xmove = LookAxis.x * Sensivity * Time.deltaTime;
            Ymove = LookAxis.y * Sensivity * Time.deltaTime;
        }

        yRotationCamera -= Ymove;
        yRotationCamera = Mathf.Clamp(yRotationCamera, -90f, 90f);

        // Ajusta la posición de la cámara basándote en el ángulo de rotación vertical
        float cameraY = Mathf.Lerp(defaultCameraHeight, shoulderHeight, yRotationCamera / -90f) * Mathf.Sin(Mathf.Deg2Rad * yRotationCamera);
        float cameraZ = 0.0f;
        if (yRotationCamera < 0)
        {
          cameraZ =  Mathf.Lerp(defaultCameraDistance, shoulderDistance, yRotationCamera / -90f);// * Mathf.Cos(Mathf.Deg2Rad * yRotationCamera);
        }
        else
        {
            cameraZ = Mathf.Lerp(defaultCameraDistance, shoulderDistance, yRotationCamera / 90f);// * Mathf.Cos(Mathf.Deg2Rad * yRotationCamera);
        }
         
        cameraTransForm.localPosition = new Vector3(0, cameraY, cameraZ);

        cameraTransForm.localRotation = Quaternion.Euler(yRotationCamera, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * Xmove);
    }
}
