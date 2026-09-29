using VoxelUiDemo.ui.elements.basic;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public int Spacing { get; init; } = 10;
    
    protected override void ArrangeChildren() {
        var cursor = (Orientation == Orientation.Horizontal) ? Layout.Transform.Offset.X : Layout.Transform.Offset.Y;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var nextTransform = (Orientation == Orientation.Horizontal) ?
                new Transform{ Offset = new Vector2I(cursor, Layout.Transform.Offset.Y), Size = child.Layout.Transform.Size } : 
                new Transform{ Offset = new Vector2I(Layout.Transform.Offset.X, cursor), Size = child.Layout.Transform.Size };
            child.AssignTransform(nextTransform);
            
            cursor += Spacing;
        }
    }
}
