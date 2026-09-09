using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui;

public struct UiRenderCommand {
    public CommandType Type;
    public Vector2I Position;
    public Vector2I Size;
    public Color Color;
    public string Text;
    public int FontSize;
    public Texture2D? Texture { get; init; }
}