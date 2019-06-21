using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CameraSpeedController : MonoBehaviour
{
    private Slider m_SpeedBoostSlider;
    public Text m_SpeedBoostText;

    private void Start()
    {
        m_SpeedBoostSlider = GetComponent<Slider>();
        m_SpeedBoostSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); }); ;
        m_SpeedBoostSlider.value = CameraController.Instance.GetMovementBoostSpeed();
        m_SpeedBoostText.text = m_SpeedBoostSlider.value.ToString("0.00");
    }

    private void ValueChangeCheck()
    {
        CameraController.Instance.ChangeMovementBoostSpeed(m_SpeedBoostSlider.value);
        m_SpeedBoostText.text = m_SpeedBoostSlider.value.ToString("0.00");

        // remove focus from this slider (because of movement axis)
        EventSystem.current.SetSelectedGameObject(null);        
    }
}