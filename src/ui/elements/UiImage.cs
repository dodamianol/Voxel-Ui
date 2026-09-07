using Raylib_cs;
using System.Numerics;

namespace VoxelUiDemo.ui.elements;

public class UiImage : UiElement {
    public Texture2D Texture { get; set; }

    public Color Tint { get; set; } = Color.White;

    protected override void RenderSelf(List<UiRenderCommand> commands) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawTexture,
            Texture = Texture,
            Position = new Vector2(FinalBounds.X, FinalBounds.Y),
            Size = new Vector2(FinalBounds.Width, FinalBounds.Height),
            Color = Tint
        });
    }
}
