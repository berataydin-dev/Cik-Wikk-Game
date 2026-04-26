using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour,Icollectible
{
    [SerializeField] private PlayerController _playerConrtoller;
   [SerializeField] private float _increaseforce;
   [SerializeField] private float _resetBoostDuration;
   public void collect(){
    _playerConrtoller.SetJumpForce(_increaseforce,_resetBoostDuration);
    Destroy(gameObject);
   }
}
