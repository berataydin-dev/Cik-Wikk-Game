using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _BlackBackgroundObject;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _MainMenuButton;
    private Image _blackBackGroundImage;
    [SerializeField] private float _animationDuration;

    private void Awake()
    {
        _blackBackGroundImage=_BlackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale=Vector3.zero;
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }
    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        _BlackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);
        _blackBackGroundImage.DOFade(0.8f,_animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f,_animationDuration).SetEase(Ease.OutBack);
    }
    private void OnResumeButtonClicked()
    {
        
        _blackBackGroundImage.DOFade(0f,_animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(0f,_animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
             _BlackBackgroundObject.SetActive(false);
        _settingsPopupObject.SetActive(false);
        });
        
    }

}
