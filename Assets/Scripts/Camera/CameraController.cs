using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private float m_RotationDegree = 10;

    private CameraState m_TargetCameraState = new CameraState();
    private CameraState m_InterpolatingCameraState = new CameraState();

    [Header("Movement Settings")]
    [SerializeField] private float m_Boost = 3.5f;

    [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
    public float m_PositionLerpTime = 0.2f;

    [Header("Compass Options")]
    public Transform m_Compass;
    public Vector3 m_CompassPositionOffset = new Vector3(1, 1, 1);

    [Header("Rotation Settings")]
    [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
    public AnimationCurve m_MouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    public float m_RotationLerpTime = 0.01f;

    [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
    public bool m_InvertY = false;

    #region MonoBehaviour Methods
    private void Awake()
    {
        Instance = this;
        transform.parent = null;    // for better controll on transform.position
    }

    void OnEnable()
    {
        m_TargetCameraState.SetFromTransform(transform);
        m_InterpolatingCameraState.SetFromTransform(transform);
    }

    void Update()
    {
        // Hide and lock cursor when right mouse button pressed
        if (Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        // Unlock and show cursor when right mouse button released
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Rotation
        if (Input.GetMouseButton(1))
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (m_InvertY ? 1 : -1));

            float mouseSensitivityFactor = m_MouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
        }

        // Translation
        Vector3 translation = GetInputTranslationDirection() * Time.deltaTime;

        // Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
        translation *= m_Boost;

        m_TargetCameraState.Translate(translation);

        // Framerate-independent interpolation
        // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
        float positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / m_PositionLerpTime) * Time.deltaTime);
        float rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / m_RotationLerpTime) * Time.deltaTime);
        m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

        m_InterpolatingCameraState.UpdateTransform(transform);
        
        m_Compass.position = transform.position + m_CompassPositionOffset;
        m_Compass.localEulerAngles = -transform.eulerAngles;
    }
    #endregion

    private Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();

        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
            direction += Vector3.forward;

        if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
            direction += Vector3.back;

        if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
            direction += Vector3.right;

        if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
            direction += Vector3.left;

        if (CrossPlatformInputManager.GetAxis("Vertical2D") > 0)
            direction += Vector3.up;

        if (CrossPlatformInputManager.GetAxis("Vertical2D") < 0)
            direction += Vector3.down;

        return direction;
    }

    public void ResetTransform(Vector3 position, Vector3 eulerAngles, float time)
    {
        iTween.Stop(gameObject);
        iTween.Stop(m_Compass.gameObject);

        iTween.RotateTo(gameObject, iTween.Hash("position", position, "rotation", eulerAngles, "time", time, "oncomplete", "OnResetFinished"));
        iTween.RotateTo(m_Compass.gameObject, -eulerAngles, time);
    }

    private void OnResetFinished()
    {
        m_InterpolatingCameraState.SetFromTransform(transform);
        m_TargetCameraState.SetFromTransform(transform);
    }

    public void ChangeMovementBoostSpeed(float speed)
    {
        m_Boost = speed;
    }

    public float GetMovementBoostSpeed()
    {
        return m_Boost;
    }

    public void ChangeRotationDegree(float degree)
    {
        m_RotationDegree = degree;
    }

    public float GetRotationDegree()
    {
        return m_RotationDegree;
    }
}