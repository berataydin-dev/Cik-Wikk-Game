using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, Icollectible
{
   [SerializeField] private PlayerController _playerConrtoller;
   [SerializeField] private float _increasespeed;
   [SerializeField] private float _resetBoostDuration;
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_increasespeed,_resetBoostDuration);
    Destroy(gameObject);
   }
}
