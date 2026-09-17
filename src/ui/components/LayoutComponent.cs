using System.Numerics;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public struct LayoutComponent {
    public Transform Transform;
    public Transform FinalTransform;
    public AnchorType Anchor;

    public void Calculate(Transform? parentFinalTransform, int screenWidth, int screenHeight) {
        var parentPos = parentFinalTransform?.Offset ?? Vector2I.Zero;
        var parentSize = parentFinalTransform?.Size ?? new Vector2I(screenWidth, screenHeight);
        var parentScale = parentFinalTransform?.Scale ?? Vector2.One;

        var finalScale = parentScale * Transform.Scale;        
        var scaledSize = Vector2I.Round(Transform.Size * finalScale);
        var scaledOffset = Vector2I.Round(Transform.Offset * parentScale);
        
        var factor = Anchor.GetFactor();
        var anchorPoint = parentPos + Vector2I.Round((parentSize - scaledSize) * factor);
        var finalPos = anchorPoint + scaledOffset;
        
        FinalTransform = new Transform { 
            Offset = finalPos, 
            Size = scaledSize, 
            Scale = finalScale 
        };
    }
}