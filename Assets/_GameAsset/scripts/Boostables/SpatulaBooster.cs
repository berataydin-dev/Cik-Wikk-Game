using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [SerializeField] private Animator _spatulaAnimator;
    [SerializeField] private float _jumpForce;

    private bool _isactive;
    public void Boost(PlayerController playerController){
        if(_isactive){return;}
    PlayBoostAnimation();

    Rigidbody playerRigidbody=playerController.GetPlayerRigidbody();

    playerRigidbody.linearVelocity=new Vector3(playerRigidbody.linearVelocity.x,0,playerRigidbody.linearVelocity.z);
  playerRigidbody.AddForce(transform.forward*_jumpForce,ForceMode.Impulse);
  _isactive=true;
  Invoke(nameof(ResetActivation),0.2f);
   }

   private void PlayBoostAnimation(){
    _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
   }
   private void ResetActivation(){
    _isactive=false;
   }
}
