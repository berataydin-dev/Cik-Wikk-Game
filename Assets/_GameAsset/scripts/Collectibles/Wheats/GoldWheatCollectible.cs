using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour, Icollectible
{
   [SerializeField] private WheatDesignSO _wheatDesignSO;
   [SerializeField] private PlayerController _playerConrtoller;
   [SerializeField] private PlayerStateUI _playerStateUI;

   private RectTransform _playerBoosterTransform;
   private Image _playerBoosterImage;
   private void Awake(){
      _playerBoosterTransform=_playerStateUI.GetBoosterSpeedTransform;
      _playerBoosterImage=_playerBoosterTransform.GetComponent<Image>();
   }
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
    _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetGoldBoosterWheatImage,_wheatDesignSO.ActiveSprite,_wheatDesignSO.pasiveSprite,_wheatDesignSO.ActiveWheatSprite,_wheatDesignSO.PasiveWheatSprite,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
   }
}
