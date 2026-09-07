using System.Numerics;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui;

public struct Transform {
    public Transform() { }

    public required Vector2I Offset { get; set; }
    public required Vector2I Size { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
}
