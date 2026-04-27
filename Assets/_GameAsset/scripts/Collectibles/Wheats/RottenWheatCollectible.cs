using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, Icollectible
{
    [SerializeField] private PlayerController _playerConrtoller;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerStateUI _playerStateUI;
   private RectTransform _playerBoosterTransform;
   private Image _playerBoosterImage;
   private void Awake(){
      _playerBoosterTransform=_playerStateUI.GetBoosterSlowTransform;
      _playerBoosterImage=_playerBoosterTransform.GetComponent<Image>();
   }
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
   _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetRottenBoosterWheatImage,_wheatDesignSO.ActiveSprite,_wheatDesignSO.pasiveSprite,_wheatDesignSO.ActiveWheatSprite,_wheatDesignSO.PasiveWheatSprite,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
   }
}
