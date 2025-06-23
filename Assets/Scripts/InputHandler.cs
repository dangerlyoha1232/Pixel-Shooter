using Game.Services;
using UnityEngine;

public class InputHandler : IService
{
    public float HorizontalInput() => Input.GetAxisRaw("Horizontal");
    
    public bool JumpInput() => Input.GetButtonDown("Jump");
    
    public bool LeftMouseInput() => Input.GetMouseButtonDown(0);
}