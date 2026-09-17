using Raylib_cs;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiGridPanel :  UiElement {
    
    //TODO: make uipanel as background 
    
    public Orientation PrimaryOrientation = Orientation.Horizontal;
    public int SpacingX { get; init; } = 10;
    public int SpacingY { get; init; } = 10;
    public int ElementsPerPrimarayDimension { get; init; } = 3;

    protected override void ArrangeChildren() {
        var countInCurrentRow = 0;
        var xPos = 0;
        var yPos = 0;
        
        foreach (var child in Children) {
            if (!child.IsVisible) continue;
            
            var nextTransform = new Transform { Offset = new Vector2I(xPos, yPos), Size = child.Layout.Transform.Size };
            child.AssignTransform(nextTransform);
            
            if (PrimaryOrientation == Orientation.Horizontal) xPos += SpacingX;
            else yPos += SpacingY;
            
            countInCurrentRow++;

            if (countInCurrentRow < ElementsPerPrimarayDimension) continue;

            if (PrimaryOrientation == Orientation.Horizontal) {
                xPos = Layout.Transform.Offset.X;    
                yPos += SpacingY;
            }
            else {
                xPos += SpacingX;
                yPos = Layout.Transform.Offset.Y;
            }
            countInCurrentRow = 0;
        }
    }
}
