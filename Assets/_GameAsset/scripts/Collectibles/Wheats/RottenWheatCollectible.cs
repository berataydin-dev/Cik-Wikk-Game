using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, Icollectible
{
    [SerializeField] private PlayerController _playerConrtoller;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
   }
}
