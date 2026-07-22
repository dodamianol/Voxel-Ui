using System.Net.Mime;
using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements {
    public class UiSlider : UiElement, Draggable {
        public float Value { get; private set; } = 0.5f;
        public Color NormalColor { get; set; } = Color.Gray;
        public Color HighlightColor { get; set; } = Color.LightGray;
        public Color PressedColor { get; set; } = Color.DarkGreen;
        
        private float _barWidth;
        private float _barHeight;
        private Vector2 _barPos;

        private float _handleWidth;
        private float _handleHeight;
        private float _handleY;
        private Vector2 _handlePos;
        
        private Rectangle _handleBounds;
        
        private bool _isBeingDragged = false;

        public override void CalculateLayout() {
            base.CalculateLayout();

            _barWidth = AbsoluteBounds.Width * 0.9f;
            _barHeight = AbsoluteBounds.Height * 0.1f;
            
            _barPos = new Vector2(
                AbsoluteBounds.X + (AbsoluteBounds.Width - _barWidth) / 2f, 
                AbsoluteBounds.Y + (AbsoluteBounds.Height - _barHeight) / 2f
            );
            
            _handleWidth = _handleHeight = AbsoluteBounds.Height * 0.8f;
            var handleX = _barPos.X + (_barWidth * Value) - (_handleWidth / 2f);
            _handleY = AbsoluteBounds.Y + (AbsoluteBounds.Height - _handleHeight) / 2f;
            _handlePos = new Vector2(handleX, _handleY);
        
            _handleBounds = new Rectangle(handleX, _handleY, _handleWidth, _handleHeight);
        }
        
        public override bool ContainsPoint(Vector2 point) {
            return Raylib.CheckCollisionPointRec(point, _handleBounds);
        }

        public void OnDragStart(Vector2 mousePos) {
            _isBeingDragged = true;
        }

        public void OnDrag(Vector2 mousePos) {

            var newX = mousePos.X;
            newX = Math.Clamp(newX, _barPos.X, _barPos.X + _barWidth);
            
            _handleBounds = new Rectangle(newX, _handleY, _handleWidth, _handleHeight);
        }

        public void OnDragEnd() {
            _isBeingDragged = false;
        }
        
        protected override void RenderSelf(List<UiRenderCommand> commands) {
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawRect,
                Position = _barPos,
                Size = new Vector2(_barWidth, _barHeight),
                Color = Color.DarkGray
            });

            var color = NormalColor;
            if (_isBeingDragged) color = PressedColor;
            else if (IsHovered) color  = HighlightColor;
            
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawRect,
                Position = new Vector2(_handleBounds.X, _handleBounds.Y),
                Size = new Vector2(_handleBounds.Width, _handleBounds.Height),
                Color = color
            });
        }
    }
}