using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    // public Orientation PrimaryOrientation = Orientation.Horizontal;
    public float SpacingX { get; init; } = 10;
    public float SpacingY { get; init; } = 10;
    public int ElementsPerRow { get; init; } = 3;
    
    public override void CalculateLayout() {
        base.CalculateLayout();
        
        var countInCurrentRow = 0;
        var xPos = AbsoluteBounds.X;
        var yPos = AbsoluteBounds.Y;
        foreach (var child in Children) {
            if (!child.IsVisible) continue;
            
            var nextBounds = new Rectangle(xPos, yPos, child.Size.X, child.Size.Y);
            child.AssignLayoutBounds(nextBounds);
            
            child.CalculateLayout();
            
            xPos += child.Size.X + SpacingX;
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerRow) continue;
            
            yPos += child.Size.Y + SpacingY;
            xPos = AbsoluteBounds.X;
            countInCurrentRow = 0;
        }
    }
}