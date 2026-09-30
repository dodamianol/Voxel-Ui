using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class RectRenderer : RenderComponent {
    public Color Color { get; set; } = new(35, 35, 35, 255);

    public RectRenderer() { }

    public RectRenderer(Color color) {
        Color = color;
    }

    public void CollectRenderCommands(List<UiRenderCommand> commands, LayoutComponent layout) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = layout.FinalTransform.Offset,
            Size = layout.FinalTransform.Size,
            Color = Color
        });
    }
}