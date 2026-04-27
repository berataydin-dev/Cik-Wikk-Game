using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class PlayerStateUI : MonoBehaviour
{
   [Header("references")]
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private RectTransform _playerWalkingTransform;
   [SerializeField] private RectTransform _playerSlidingTransform;
   [SerializeField] private RectTransform _boosterSpeedTransform;
   [SerializeField] private RectTransform _boosterJumpTransform;
   [SerializeField] private RectTransform _boosterSlowdTransform;
   [Header("Images")]
   [SerializeField] private Image _goldBoosterWheatImage;
   [SerializeField] private Image _holyBoosterWheatImage;
   [SerializeField] private Image _rottenBoosterWheatImage;
   [Header("sprites")]
   [SerializeField] private Sprite _playerWalkingActiveSprite;
   [SerializeField] private Sprite _playerWalkingPasiveSprite;
   [SerializeField] private Sprite _playerSlidingActiveSprite;
   [SerializeField] private Sprite _playerSlidingPasiveSprite;
   [Header("Settings")]
   [SerializeField] private float _moveDuration;
   [SerializeField] private Ease _moveEase;
   public RectTransform GetBoosterSpeedTransform=>_boosterSpeedTransform;
   public RectTransform GetBoosterSlowTransform=>_boosterSlowdTransform;
   public RectTransform GetBoosterJumpTransform=>_boosterJumpTransform;

   public Image GetGoldBoosterWheatImage=>_goldBoosterWheatImage;
   public Image GetHolyBoosterWheatImage=>_holyBoosterWheatImage;
   public Image GetRottenBoosterWheatImage=>_rottenBoosterWheatImage;
   private Image _playerWalkingImage;
   private Image _playerSlidingImage;
   private void Awake(){
    _playerWalkingImage=_playerWalkingTransform.GetComponent<Image>();
    _playerSlidingImage=_playerSlidingTransform.GetComponent<Image>();
   }
   private void Start(){
    _playerController.OnPlayerStateChanged+=PlayerController_OnPlayerStateChanged;
    SetStateUserInterFace(_playerWalkingActiveSprite,_playerSlidingPasiveSprite,_playerWalkingTransform,_playerSlidingTransform);
     }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerstate){
   switch(playerstate){
       case PlayerState.Idle:
       case PlayerState.Move:
       SetStateUserInterFace(_playerWalkingActiveSprite,_playerSlidingPasiveSprite,_playerWalkingTransform,_playerSlidingTransform);
       break;
       case PlayerState.SlideIdle:
       case PlayerState.Slide:
       SetStateUserInterFace(_playerWalkingPasiveSprite,_playerSlidingActiveSprite,_playerSlidingTransform,_playerWalkingTransform);
       break;}
   }
   private void SetStateUserInterFace(Sprite playerWalkingSprite, Sprite playerSlidingSprite,RectTransform activeTransform ,RectTransform pasiveTransform){
     _playerWalkingImage.sprite=playerWalkingSprite;
     _playerSlidingImage.sprite=playerSlidingSprite;

     activeTransform.DOAnchorPosX(-25f,_moveDuration).SetEase(_moveEase);
     pasiveTransform.DOAnchorPosX(-90f,_moveDuration).SetEase(_moveEase);
   }
   private IEnumerator SetBoosterUserInterFace(RectTransform activeTransform,Image boosterImage, Image wheatImage,Sprite activeSprite,Sprite pasiveSprite,Sprite activeWheatSprite,Sprite pasiveWheatSprite,float duration){
    boosterImage.sprite=activeSprite;
    wheatImage.sprite=activeWheatSprite;
    activeTransform.DOAnchorPosX(25f,_moveDuration).SetEase(_moveEase);
    yield return new WaitForSeconds(duration);
    boosterImage.sprite=pasiveSprite;
    wheatImage.sprite=pasiveWheatSprite;
    activeTransform.DOAnchorPosX(90f,_moveDuration).SetEase(_moveEase);
   }
   public void PlayBoosterUIAnimations(RectTransform activeTransform,Image boosterImage, Image wheatImage,Sprite activeSprite,Sprite pasiveSprite,Sprite activeWheatSprite,Sprite pasiveWheatSprite,float duration){
    StartCoroutine(SetBoosterUserInterFace(activeTransform,boosterImage,wheatImage,activeSprite,pasiveSprite,activeWheatSprite,pasiveWheatSprite,duration));
   }
}
