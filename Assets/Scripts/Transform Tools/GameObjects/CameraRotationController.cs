using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraRotationController : MonoBehaviour
{
    public enum Direction { Left, Front, Right, Top, Bottom, Back }
    public Direction m_Direction = Direction.Left;

    public float m_AnimationTime = 0.5f;

    private void OnMouseUp()
    {
        Vector3 rotation;
        
        switch(m_Direction)
        {
            case Direction.Left:
                rotation = new Vector3(0.0f, 90.0f, 0.0f);
                break;
            case Direction.Front:
                rotation = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case Direction.Right:
                rotation = new Vector3(0.0f, -90.0f, 0.0f);
                break;
            case Direction.Top:
                rotation = new Vector3(90.0f, 0.0f, 0.0f);
                break;
            case Direction.Bottom:
                rotation = new Vector3(-90.0f, 0.0f, 0.0f);
                break;
            case Direction.Back:
                rotation = new Vector3(0.0f, 180.0f, 0.0f);
                break;
            default:
                rotation = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }

        CameraController.Instance.ResetTransform(CameraController.Instance.transform.position, rotation, m_AnimationTime);
    }
}