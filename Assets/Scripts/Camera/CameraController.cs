using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    [SerializeField]
    private float m_MoveSpeed = 1.0f;

    void Update()
    {
        transform.Translate(new Vector3(
            1 * Input.GetAxis("Horizontal") * m_MoveSpeed * Time.deltaTime,
            0,
            1 * Input.GetAxis("Vertical") * m_MoveSpeed * Time.deltaTime
        ));

    }
}
