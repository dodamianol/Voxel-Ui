using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.elements.basic;

namespace VoxelUiDemo.ui;

public class UiInputSystem {
    private UiElement? _hoveredElement;
    private UiElement? _pressedElement;
    private UiElement? _draggingElement;

    private Vector2 _pressPosition;
    private bool _isDragging;

    private const float DragThreshold = 3.0f;

    public void Update(UiElement root) {
        var mousePos = Raylib.GetMousePosition();

        UpdateHover(root, mousePos);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) BeginPress(mousePos);
        if (Raylib.IsMouseButtonDown(MouseButton.Left)) UpdatePress(mousePos);
        if (Raylib.IsMouseButtonReleased(MouseButton.Left)) EndPress(mousePos);
    }

    private void UpdateHover(UiElement root, Vector2 mousePos) {
        var newHoveredElement = FindInteractable(root, mousePos);

        if (_hoveredElement == newHoveredElement) return;

        _hoveredElement?.Interactable?.IsHovered = false;
        _hoveredElement = newHoveredElement;
        _hoveredElement?.Interactable?.IsHovered = true;
    }

    private void BeginPress(Vector2 mousePos) {
        if (_hoveredElement?.Interactable == null) return;

        _pressedElement = _hoveredElement;
        _pressPosition = mousePos;
        _draggingElement = null;
        _isDragging = false;

        _pressedElement.Interactable.IsPressed = true;
        _pressedElement.Interactable.OnPress?.Invoke(mousePos);
    }
    
    
    private void UpdatePress(Vector2 mousePos) {
        if (_pressedElement?.Interactable == null) return;

        if (!_isDragging) {
            var distance = Vector2.Distance(_pressPosition, mousePos);

            if (distance >= DragThreshold) {
                _isDragging = true;
                _draggingElement = _pressedElement;

                _draggingElement.Interactable.OnDragStart?.Invoke(_pressPosition);
            }
        }

        if (_isDragging) {
            _draggingElement?.Interactable?.OnDrag?.Invoke(mousePos);
        }
    }

    private void EndPress(Vector2 mousePos) {
        if (_pressedElement?.Interactable == null) return;

        _pressedElement.Interactable.IsPressed = false;

        if (_isDragging) _draggingElement?.Interactable?.OnDragEnd?.Invoke();
        else if (_hoveredElement == _pressedElement) _pressedElement.Interactable.OnClick?.Invoke(mousePos);

        _pressedElement = null;
        _draggingElement = null;
        _isDragging = false;
    }

    private UiElement? FindInteractable(UiElement element, Vector2 mousePos) {
        if (!element.IsVisible) {
            return null;
        }

        for (var i = element.Children.Count - 1; i >= 0; i--) {
            var child = element.Children[i];

            var result = FindInteractable(child, mousePos);

            if (result != null) {
                return result;
            }
        }

        if (element.Interactable == null) return null;
        return !element.ContainsPoint(mousePos) ? null : element;
    }
}