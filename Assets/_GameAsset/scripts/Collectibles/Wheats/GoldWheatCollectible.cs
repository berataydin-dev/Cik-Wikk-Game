using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, Icollectible
{
   [SerializeField] private WheatDesignSO _wheatDesignSO;
   [SerializeField] private PlayerController _playerConrtoller;
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
   }
}
