using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements {
    public class InteractableComponent {
        public bool BlocksInput { get; set; } = true;
        public bool IsHovered { get; internal set; }
        public bool IsPressed { get; internal set; }

        public Action? OnClick;
        public Action? OnHoverEnter;
        public Action? OnHoverExit;

        public bool ContainsPoint(Vector2 point, Rectangle bounds) {
            return Raylib.CheckCollisionPointRec(point, bounds);
        }
    }
}