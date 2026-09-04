using System.Numerics;

namespace VoxelUiDemo.ui.elements;

public interface Draggable {
    void OnDragStart(Vector2 mousePos);
    void OnDrag(Vector2 mousePos);
    void OnDragEnd();
};