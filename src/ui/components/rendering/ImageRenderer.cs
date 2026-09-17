using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class ImageRenderer : RenderComponent {
    public Texture2D Texture { get; set; }
    public Color Tint { get; set; } = Color.White;
    
    public void CollectRenderCommands(List<UiRenderCommand> commands, LayoutComponent layout) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawTexture,
            Texture = Texture,
            Position = layout.FinalTransform.Offset,
            Size = layout.FinalTransform.Size,
            Color = Tint
        });
    }
}