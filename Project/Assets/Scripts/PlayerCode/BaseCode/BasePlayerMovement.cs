using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCode.BaseCode {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    public abstract class BasePlayerController : MonoBehaviour {
        #region Variables

        //default values that can be changed later
        public virtual float walkSpeed => 3f;
        public virtual float jumpHeight => 2f;
        public LayerMask groundMask = 3; //change default to ground layer

        //stores the player inputs into variables
        private Vector2 _moveInput;
        private bool _jumpButtonDown;
        private bool _attackKeyDown;

        private bool _hasPunched;
        //references
        protected Rigidbody rb;

        #endregion

        #region Unity Methods

        private void Start() {
            rb = GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate() {
            HandleMovement();
            HandleJump();
            CombatManager();
        }

        private void OnDrawGizmos() {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.2f);
        }

        #endregion

        #region Input Methods

        public void MoveInput(InputAction.CallbackContext c) {
            _moveInput = c.ReadValue<Vector2>();
        }

        public void HandleJumpInput(InputAction.CallbackContext c) {
            _jumpButtonDown = c.performed;
        }

        #endregion

        #region Movement Methods

        protected virtual void HandleMovement() {
            //single movement, maybe add velocity later
            rb.linearVelocity = new Vector3(_moveInput.x * walkSpeed, rb.linearVelocity.y, 0);
        }

        protected virtual void HandleJump() {
            if (!(_jumpButtonDown && isGrounded)) return;

            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        #endregion

        #region Combat Methods

        private void CombatManager() {
            
        }

        protected virtual void LightAttack() { }

        protected virtual void HeavyAttack() { }

        protected virtual void ArialAttack() { }

        protected virtual void UpwardsAttack() { }

        protected virtual void DownwardsAttack() { }

        protected virtual void Block() { }
        
        protected virtual void Ability() { }
        
        protected virtual void Ultimate() { }

        #endregion

        #region Check Methods
        protected bool isGrounded => Physics.Raycast(transform.position, Vector3.down, 1.2f, groundMask);

        protected bool shouldPunch => !_hasPunched;
        #endregion
    }
}