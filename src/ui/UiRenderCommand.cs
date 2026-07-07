using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui;

public struct UiRenderCommand {
    public CommandType Type;
    public Vector2 Position;
    public Vector2 Size;
    public Color Color;
    public string Text;
    public Texture2D? Texture { get; set; }
}