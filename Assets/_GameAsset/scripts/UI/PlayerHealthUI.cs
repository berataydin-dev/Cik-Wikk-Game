using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // HATA: DOTween kullanmak için bu kütüphane eklenmeliydi.

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image[] _playerHealthImage;
    [SerializeField] private Sprite _playerHealthSprite;
    [SerializeField] private Sprite _playerUnhealthSprite;
    [SerializeField] private float _scaleDuration;
    private RectTransform[] _playerHealthTransforms;

    private void Awake()
    {
        _playerHealthTransforms = new RectTransform[_playerHealthImage.Length];
        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            // HATA: virgül (,) yerine nokta (.) kullanılmalıydı. 
            // Ayrıca 'gameObject' takısına gerek yok, direkt Image üzerinden erişilebilir.
            _playerHealthTransforms[i] = _playerHealthImage[i].rectTransform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
        }
    }

    private void AnimateDamageSprite(Image activeimage, RectTransform activeImageTrasform)
    {
        activeImageTrasform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeimage.sprite = _playerUnhealthSprite;
            activeImageTrasform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            if (_playerHealthImage[i].sprite == _playerHealthSprite)
            {
                AnimateDamageSprite(_playerHealthImage[i], _playerHealthTransforms[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImage[i], _playerHealthTransforms[i]);
        }
    }
}