using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject m_MovementPad;

    public RectTransform m_SettingsPanel;

    [Header("Settings Button")]
    public Image m_SettingButton;
    public Sprite m_CloseSettingsSprite;
    public Sprite m_ShowSettingsSprite;

    [Range(0.001f, 0.1f)] public float m_AnimationFrameTime = 0.017f;

    #region Backup Holders
    private float m_SettingsPanelStartXPos;
    private float m_SettingsPanelWidth;
    private bool m_IsSettingHidden;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_SettingsPanelStartXPos = m_SettingsPanel.transform.position.x;
        m_SettingsPanelWidth = m_SettingsPanel.rect.width;

        if (m_SettingsPanel.anchoredPosition.x < 0)
        {
            m_IsSettingHidden = true;
            m_SettingButton.sprite = m_ShowSettingsSprite;
            m_SettingsPanel.anchoredPosition = new Vector2(-m_SettingsPanelWidth, 0);
        }
        else
        {
            m_IsSettingHidden = false;
            m_SettingButton.sprite = m_CloseSettingsSprite;
            m_SettingsPanel.anchoredPosition = Vector2.zero;
        }

#if !UNITY_ANDROID
        Destroy(m_MovementPad);
#endif
    }
    #endregion

    public void ShowSettingsPanel()
    {
        m_SettingButton.sprite = m_CloseSettingsSprite;
        m_IsSettingHidden = false;

        StopAllCoroutines();
        StartCoroutine(MoveSettingPanelTo(Vector2.zero, m_AnimationFrameTime));
    }

    public void HideSettingsPanel()
    {
        m_SettingButton.sprite = m_ShowSettingsSprite;
        m_IsSettingHidden = true;

        StopAllCoroutines();
        StartCoroutine(MoveSettingPanelTo(new Vector2(-m_SettingsPanelWidth, 0), m_AnimationFrameTime));
    }

    public void OnSettingsButtonClick()
    {
        if (m_IsSettingHidden)
            ShowSettingsPanel();
        else
            HideSettingsPanel();
    }

    private IEnumerator MoveSettingPanelTo(Vector2 endPosition, float waitTime)
    {
        for (; ; )
        {
            m_SettingsPanel.anchoredPosition = Vector2.Lerp(endPosition, m_SettingsPanel.anchoredPosition, 0.5f);

            if (m_SettingsPanel.anchoredPosition.x <= (-m_SettingsPanelWidth + 1))
            {
                m_SettingsPanel.anchoredPosition = new Vector2(-m_SettingsPanelWidth, 0);
                yield break;
            }
            if (m_SettingsPanel.anchoredPosition.x > -1)
            {
                m_SettingsPanel.anchoredPosition = Vector2.zero;
                yield break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}