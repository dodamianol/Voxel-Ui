using Raylib_cs;
using System.Numerics;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiImage : UiElement {
    public Texture2D Texture { get; set; }

    public Color Tint { get; set; } = Color.White;

    protected override void RenderSelf(List<UiRenderCommand> commands) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawTexture,
            Texture = Texture,
            Position = Layout.FinalTransform.Offset,
            Size = Layout.FinalTransform.Size,
            Color = Tint
        });
    }
}
