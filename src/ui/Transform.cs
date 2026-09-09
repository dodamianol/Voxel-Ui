using System.Numerics;
using VoxelUiDemo.voxel_engine;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui;

public struct Transform {
    public Transform() { }

    public Vector2I Offset { get; set; } = new Vector2I(0, 0);
    public Vector2I Size { get; set; } = new  Vector2I(100, 100);
    public Vector2 Scale { get; set; } = Vector2.One;

    public Rectangle ToRectangle() {
        return new Rectangle(Offset.X, Offset.Y, Size.X, Size.Y);
    }
    
    public Transform WithOffset(Vector2I newOffset) => this with { Offset = newOffset };
    public Transform WithSize(Vector2I newSize) => this with { Size = newSize };
    public Transform WithScale(Vector2 newScale) => this with { Scale = newScale }; 
}
