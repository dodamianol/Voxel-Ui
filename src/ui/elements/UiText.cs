using Raylib_cs;
using System.Numerics;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiText : UiElement {
    public string Text { get; set; } = "";
    public int FontSize { get; set; } = 20;
    public Color TextColor { get; set; } = Color.White;
    
    public TextAlignmentHorizontal AlignH { get; set; } = TextAlignmentHorizontal.Center;
    public TextAlignmentVertical AlignV { get; set; } = TextAlignmentVertical.Center;

    public UiText() {
        BackgroundColor = Color.Blank;
        BlocksInput = false;
    }

    protected override void RenderSelf(List<UiRenderCommand> commands) {
        if (string.IsNullOrEmpty(Text)) return;

        var textWidth = Raylib.MeasureText(Text, FontSize);
        var textHeight = FontSize;

        var drawX = getXPos(textWidth);
        var drawY = getYPos(textHeight);
        
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawText,
            Text = Text,
            Position = Vector2I.Round(new Vector2(drawX, drawY)),
            FontSize = FontSize,
            Color = TextColor
        });
    }

    private float getXPos(int textWidth) {
        float drawX = FinalTransform.Offset.X;

        if (AlignH == TextAlignmentHorizontal.Center) {
            drawX += (FinalTransform.Size.X - textWidth) / 2f;
        }
        else if (AlignH == TextAlignmentHorizontal.Right) {
            drawX += FinalTransform.Size.X - textWidth;
        }

        return drawX;
    }

    private float getYPos(int textHeight) {
        float drawY = FinalTransform.Offset.Y;

        if (AlignV == TextAlignmentVertical.Center) {
            drawY += (FinalTransform.Size.Y - textHeight) / 2f;
        } 
        else if (AlignV == TextAlignmentVertical.Bottom) {
            drawY += FinalTransform.Size.Y - textHeight;
        }

        return drawY;
    }
}
