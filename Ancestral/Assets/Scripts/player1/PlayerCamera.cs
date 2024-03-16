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

    public float defaultCameraHeight = 3; // La altura de la cámara por defecto
    public float shoulderHeight = 4f; // La altura del hombro del personaje
    private float rangeHeigh;

    public float shoulderDistance = 0.7f; // La distancia desde el hombro al centro del personaje
    public float defaultCameraDistance = -12f; // La distancia de la cámara por defecto


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
        yRotationCamera = Mathf.Clamp(yRotationCamera, -80f, 80f);

        rangeHeigh = Mathf.Lerp(defaultCameraHeight, shoulderHeight, yRotationCamera / -90f);

        // Ajusta la posición de la cámara basándote en el ángulo de rotación vertical
        float cameraY = 0.0f;
        float cameraZ = 0.0f;
        if (yRotationCamera < 0)
        {
            cameraY = rangeHeigh * (1.5f * Mathf.Sin(yRotationCamera / 30) ) + 2;
            cameraZ = Mathf.Lerp(defaultCameraDistance, shoulderDistance, yRotationCamera / -90f);
        }
        else
        {  cameraY = rangeHeigh * (1.5f * Mathf.Sin(yRotationCamera /30) ) + 2;
            cameraZ = Mathf.Lerp(defaultCameraDistance, shoulderDistance, yRotationCamera / 90f);
        }

        cameraTransForm.localPosition = new Vector3(0, cameraY, cameraZ);

        cameraTransForm.localRotation = Quaternion.Euler(yRotationCamera, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * Xmove);
    }
}
