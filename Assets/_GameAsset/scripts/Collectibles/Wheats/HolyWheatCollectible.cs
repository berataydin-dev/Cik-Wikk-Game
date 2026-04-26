using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour,Icollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerConrtoller;
   public void collect(){
    _playerConrtoller.SetJumpForce(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
   }
}
