using System.Numerics;

namespace VoxelUiDemo.ui.components;

public class InteractableComponent {
    public bool BlocksInput { get; set; } = true;
    
    public bool IsHovered { get; internal set; }
    public bool IsPressed { get; internal set; }

    public Action<Vector2>? OnPress;
    public Action<Vector2>? OnClick;
    
    public Action<Vector2>? OnDragStart;
    public Action<Vector2>? OnDrag;
    public Action? OnDragEnd;
}