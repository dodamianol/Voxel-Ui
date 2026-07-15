using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public float SpacingX { get; init; } = 10;
    public float SpacingY { get; init; } = 10;
    public int ElementsPerPrimarayDimension { get; init; } = 3;
    
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

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos += SpacingX;
            }
            else {
                yPos += SpacingY;
            }
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerPrimarayDimension) continue;

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos = AbsoluteBounds.X;    
                yPos += SpacingY;
            }
            else {
                xPos += + SpacingX;
                yPos =  AbsoluteBounds.Y;
            }
            countInCurrentRow = 0;
        }
    }
}