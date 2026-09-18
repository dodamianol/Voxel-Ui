using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    
    //TODO: make uipanel as background 
    
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public Vector2I CellSize { get; init; } = new(100, 100);
    public int SpacingX { get; init; } = 10;
    public int SpacingY { get; init; } = 10;
    public int ElementsPerPrimarayDimension { get; init; } = 3;

    protected override void ArrangeChildren() {
        var countInCurrentRow = 0;
        var xPos = 0;
        var yPos = 0;
        
        foreach (var child in Children) {
            if (!child.IsVisible) continue;
            
            var curTransform = child.Layout.Transform;
            var nextTransform = curTransform
                .WithOffset(new Vector2I(xPos, yPos))
                .WithSize(CellSize);
            child.AssignTransform(nextTransform, AnchorType.TopLeft);

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos += CellSize.X + SpacingX;
            }
            else {
                yPos += CellSize.Y + SpacingY;
            }
            
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerPrimarayDimension) continue;

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
