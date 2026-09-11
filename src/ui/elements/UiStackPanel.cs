using VoxelUiDemo.voxel_engine;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public int Spacing { get; set; } = 10;
    
    protected override void CalculateOwnLayout() {
        base.CalculateOwnLayout();

        var cursor = (Orientation == Orientation.Horizontal) ? Transform.Offset.X : Transform.Offset.Y;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var nextTransform = (Orientation == Orientation.Horizontal) ?
                new Transform{ Offset = new Vector2I(cursor, Transform.Offset.Y), Size = child.Transform.Size } : 
                new Transform{ Offset = new Vector2I(Transform.Offset.X, cursor), Size = child.Transform.Size };
            child.AssignTransform(nextTransform);
            
            // child.CalculateLayout();

            cursor += Spacing;
        }
    }
}
