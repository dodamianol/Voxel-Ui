using VoxelUiDemo.ui.elements.basic;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public Vector2I ChildSize { get; init; } = new(100, 100);
    public int Spacing { get; init; } = 10;
    
    protected override void ArrangeChildren() {
        var cursor = 0;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var curTransform = child.Layout.Transform;
            var nextTransform = (Orientation == Orientation.Horizontal) 
                ? curTransform.WithOffset(new Vector2I(cursor, 0)).WithSize(ChildSize) 
                : curTransform.WithOffset(new Vector2I(0, cursor)).WithSize(ChildSize);
            child.AssignTransform(nextTransform, AnchorType.TopLeft);
            
            cursor += (Orientation == Orientation.Horizontal ? ChildSize.X : ChildSize.Y) + Spacing;
        }
    }
}
