using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements {
    public class TextRenderer : RenderComponent {
        public string Text { get; set; } = "";
        public int FontSize { get; set; } = 20;
        public Color TextColor { get; set; } = Color.White;
        public TextAlignmentHorizontal AlignH { get; set; } = TextAlignmentHorizontal.Center;
        public TextAlignmentVertical AlignV { get; set; } = TextAlignmentVertical.Center;

        public void CollectRenderCommands(List<UiRenderCommand> commands, LayoutComponent layout) {
            if (string.IsNullOrEmpty(Text)) return;

            var scale = layout.FinalTransform.Scale.Y;
            var scaledFontSize = Math.Max(1, (int)MathF.Round(FontSize * scale));
            
            var textWidth = Raylib.MeasureText(Text, scaledFontSize);
            var textHeight = scaledFontSize;

            float drawX = layout.FinalTransform.Offset.X;
            if (AlignH == TextAlignmentHorizontal.Center) drawX += (layout.FinalTransform.Size.X - textWidth) / 2f;
            else if (AlignH == TextAlignmentHorizontal.Right) drawX += layout.FinalTransform.Size.X - textWidth;

            float drawY = layout.FinalTransform.Offset.Y;
            if (AlignV == TextAlignmentVertical.Center) drawY += (layout.FinalTransform.Size.Y - textHeight) / 2f;
            else if (AlignV == TextAlignmentVertical.Bottom) drawY += layout.FinalTransform.Size.Y - textHeight;

            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawText,
                Text = Text,
                Position = Vector2I.Round(new Vector2(drawX, drawY)),
                FontSize = scaledFontSize,
                Color = TextColor
            });
        }
    }
}