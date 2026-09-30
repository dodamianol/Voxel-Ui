using System.Numerics;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui;

public struct Transform {
    public Transform() { }

    public Vector2 Offset = new(0, 0);
    public Vector2 Size = new(100, 100);
    public Vector2 Scale = Vector2.One;

    public Rectangle ToRectangle() {
        return new Rectangle(Offset.X, Offset.Y, Size.X, Size.Y);
    }
    
    public Transform WithOffset(Vector2 newOffset) => this with { Offset = newOffset };
    public Transform WithSize(Vector2 newSize) => this with { Size = newSize };
    public Transform WithScale(Vector2 newScale) => this with { Scale = newScale }; 
}
