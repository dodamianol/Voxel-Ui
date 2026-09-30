using System.Numerics;
using VoxelUiDemo.ui.elements.basic;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel : UiElement {
    
    //TODO: make uiPanel as background 
    
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public Vector2 CellSize { get; init; } = new(100, 100);
    public int SpacingX { get; init; } = 10;
    public int SpacingY { get; init; } = 10;
    public int ElementsPerPrimaryDimension { get; init; } = 3;

    protected override void ArrangeChildren() {
        var countInCurrentRow = 0;
        var xPos = 0f;
        var yPos = 0f;
        
        foreach (var child in Children) {
            if (!child.IsVisible) continue;
            
            var curTransform = child.Layout.Transform;
            var nextTransform = curTransform
                .WithOffset(new Vector2(xPos, yPos))
                .WithSize(CellSize);
            child.AssignTransform(nextTransform, AnchorType.TopLeft);

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos += CellSize.X + SpacingX;
            }
            else {
                yPos += CellSize.Y + SpacingY;
            }
            
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerPrimaryDimension) continue;

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos = 0;
                yPos += CellSize.Y + SpacingY;
            }
            else {
                xPos += CellSize.X + SpacingX;
                yPos = 0;
            }
            countInCurrentRow = 0;
        }
    }
}
