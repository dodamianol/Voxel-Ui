using Raylib_cs;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public int SpacingX { get; init; } = 10;
    public int SpacingY { get; init; } = 10;
    public int ElementsPerPrimarayDimension { get; init; } = 3;

    protected override void CalculateOwnLayout() {
        base.CalculateOwnLayout();
        
        var countInCurrentRow = 0;
        var xPos = FinalTransform.Offset.X;
        var yPos = FinalTransform.Offset.Y;
        foreach (var child in Children) {
            if (!child.IsVisible) continue;
            
            var nextTransform = new Transform {
                Offset = new Vector2I(xPos, yPos), Size = child.Transform.Size
            };
            child.AssignTransform(nextTransform);
            
            child.CalculateLayout();

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos += SpacingX;
            }
            else {
                yPos += SpacingY;
            }
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerPrimarayDimension) continue;

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos = FinalTransform.Offset.X;    
                yPos += SpacingY;
            }
            else {
                xPos += SpacingX;
                yPos = FinalTransform.Offset.Y;
            }
            countInCurrentRow = 0;
        }
    }
}
