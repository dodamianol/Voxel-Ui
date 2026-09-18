using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements {
    public class InteractableComponent {
        public bool BlocksInput { get; set; } = true;
        
        public bool IsHovered { get; internal set; }
        public bool IsPressed { get; internal set; }

        public Action? OnClick;
        
        public Action<Vector2>? OnDragStart;
        public Action<Vector2>? OnDrag;
        public Action? OnDragEnd;
    }
}