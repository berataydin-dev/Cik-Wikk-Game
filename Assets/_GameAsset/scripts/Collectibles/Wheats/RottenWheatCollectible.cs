using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerConrtoller;
   [SerializeField] private float _deccreasespeed;
   [SerializeField] private float _resetBoostDuration;
   public void collect(){
    _playerConrtoller.SetMovementSpeed(_deccreasespeed,_resetBoostDuration);
    Destroy(gameObject);
   }
}
