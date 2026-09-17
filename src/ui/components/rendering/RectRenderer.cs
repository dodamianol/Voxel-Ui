using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class RectRenderer : RenderComponent {
    public Color Color { get; set; } = Color.Gray;
    
    public void CollectRenderCommands(List<UiRenderCommand> commands, LayoutComponent layout) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = layout.FinalTransform.Offset,
            Size = layout.FinalTransform.Size,
            Color = Color
        });
    }
}