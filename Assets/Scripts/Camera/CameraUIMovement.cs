using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class CameraUIMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum Axis { Horizontal, Vertical, Vertical2D }
    public Axis m_Axis;

    [Range(-1, 1)] [SerializeField] private float m_Value = 0;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        CrossPlatformInputManager.SetAxis(m_Axis.ToString(), m_Value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CrossPlatformInputManager.SetAxis(m_Axis.ToString(), 0);
    }
}