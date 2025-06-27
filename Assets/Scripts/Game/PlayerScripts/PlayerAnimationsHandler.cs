using Game.Services;
using UnityEngine;

namespace Game.PlayerScripts
{
    public class PlayerAnimationsHandler : MonoBehaviour, IService
    { 
        private Animator _animator;

        public void Init()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void RunAnimation(float speed) => _animator.SetFloat("Run_speed", speed);
        public void JumpAnimation() => _animator.SetTrigger("OnJump");
        public void IsGrounded(bool isGrounded) => _animator.SetBool("IsGrounded", isGrounded);
        public void ShootAnimation(bool isShoot) => _animator.SetBool("IsShoot", isShoot);
        public void HurtAnimation() => _animator.SetTrigger("OnHurt");
        public void DeadAnimation() => _animator.SetTrigger("OnDead");
    }
}