using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private void Start()
    {
        if (m_Target == null)
            m_Target = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(m_Target.position.x, transform.position.y, m_Target.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -m_Target.eulerAngles.y, 0);
    }
}