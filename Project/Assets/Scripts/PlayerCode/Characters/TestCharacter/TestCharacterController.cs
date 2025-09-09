using PlayerCode.BaseCode;

namespace PlayerCode.Characters.TestCharacter {
    public class TestCharacterController : BasePlayerMovement {
        protected override void HandleMovement() {
            base.HandleMovement();
            print("Movement :D");
        }
    }
}