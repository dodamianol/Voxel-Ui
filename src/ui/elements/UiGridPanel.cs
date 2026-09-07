using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public float SpacingX { get; init; } = 10;
    public float SpacingY { get; init; } = 10;
    public int ElementsPerPrimarayDimension { get; init; } = 3;

    protected override void CalculateOwnLayout() {
        base.CalculateOwnLayout();
        
        var countInCurrentRow = 0;
        var xPos = FinalBounds.X;
        var yPos = FinalBounds.Y;
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
                xPos = FinalBounds.X;    
                yPos += SpacingY;
            }
            else {
                xPos += + SpacingX;
                yPos =  FinalBounds.Y;
            }
            countInCurrentRow = 0;
        }
    }
}
