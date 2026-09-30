using System.Numerics;

namespace VoxelUiDemo.ui.elements;

public struct LayoutComponent {
    public Transform Transform;
    public Transform FinalTransform;
    public AnchorType Anchor;

    public void Calculate(Transform? parentFinalTransform, int screenWidth, int screenHeight) {
        var parentPos = parentFinalTransform?.Offset ?? Vector2.Zero;
        var parentSize = parentFinalTransform?.Size ?? new Vector2(screenWidth, screenHeight);
        var parentScale = parentFinalTransform?.Scale ?? Vector2.One;

        var finalScale = parentScale * Transform.Scale;        
        var scaledSize = Vector2.Round(Transform.Size * finalScale);
        var scaledOffset = Vector2.Round(Transform.Offset * parentScale);
        
        var factor = Anchor.GetFactor();
        var anchorPoint = parentPos + Vector2.Round((parentSize - scaledSize) * factor);
        var finalPos = anchorPoint + scaledOffset;
        
        FinalTransform = new Transform { 
            Offset = RoundVectorBy(finalPos, Program.uiPixelSize), 
            Size = RoundVectorBy(scaledSize, Program.uiPixelSize), 
            Scale = finalScale 
        };
    }
    
    private static Vector2 RoundVectorBy(Vector2 vec, int by) {
        return new Vector2(RoundBy(vec.X, by), RoundBy(vec.Y, by));
    }
    
    private static int RoundBy(float value, int by) {
        return (int)MathF.Round(value / by) * by;
    }
}