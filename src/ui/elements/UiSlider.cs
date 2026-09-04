using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements {
    public class UiSlider : UiElement, Draggable {
        public float Value { get; private set; } = 0.5f;
        public Action<float>? OnValueChanged { get; set; }
        
        private UiElement _bar = null!;
        private UiElement _handle = null!;
        private UiText _text = null!;
        private UiElement _barHighlighter = null!;

        public UiElement Bar {
            get => _bar;
            init {
                if (_bar != null) Children.Remove(_bar);
                _bar = value;
                AddChild(_bar);
            }
        }

        public UiElement Handle {
            get => _handle;
            init {
                if (_handle != null) Children.Remove(_handle);
                _handle = value;
                AddChild(_handle);
            }
        }
        
        public UiText Text {
            get => _text;
            init {
                if (_text != null) Children.Remove(_text);
                _text = value;
                AddChild(_text);
            }
        }
        
        public UiElement BarHighlighter {
            get => _barHighlighter;
            init {
                if (_barHighlighter != null) Children.Remove(_barHighlighter);
                _barHighlighter = value;
                AddChild(_barHighlighter);
            }
        }
        
        public Color NormalColor { get; set; } = Color.Gray;
        public Color HighlightColor { get; set; } = Color.LightGray;
        public Color PressedColor { get; set; } = Color.DarkGreen;
        
        private bool _isBeingDragged = false;
        private float _xOffsetToHandlePos;
        private float _minX, _maxX;

        public UiSlider() {
            Size = new Vector2(500, 50);

            Bar = new UiPanel { BackgroundColor = Color.LightGray, BlocksInput = false };
            Handle = new UiPanel { BackgroundColor = NormalColor, BlocksInput = false };
            Text = new UiText {
                AnchorType = AnchorType.MiddleRight, BackgroundColor = new Color(0, 0, 0, 25), BlocksInput =  false,
                Text = "0,00", AlignH = TextAlignmentHorizontal.Right, Offset = new Vector2(-15, 0)
            };
            BarHighlighter = new UiPanel { BackgroundColor = Color.DarkGreen, BlocksInput = false };
        }

        protected override void CalculateOwnLayout() {
            base.CalculateOwnLayout();
            
            var barWidth = AbsoluteBounds.Width * 0.8f;
            var barHeight = AbsoluteBounds.Height * 0.1f;
            var barX = AbsoluteBounds.X + 20;//+ (AbsoluteBounds.Width - barWidth) / 2f;
            var barY = AbsoluteBounds.Y + (AbsoluteBounds.Height - barHeight) / 2f;
            Bar.AssignLayoutBounds(new Rectangle(barX, barY, barWidth, barHeight));

            if (_isBeingDragged) Handle.BackgroundColor = PressedColor;
            else if (Handle.IsHovered) Handle.BackgroundColor = HighlightColor;
            else if (!Handle.IsHovered) Handle.BackgroundColor = NormalColor;
            
            var handleWidth = (AbsoluteBounds.Height * 0.8f) / 2;
            var handleHeight = AbsoluteBounds.Height * 0.8f;
            var handleY = AbsoluteBounds.Y + (AbsoluteBounds.Height - handleHeight) / 2f;
            _minX = barX;
            _maxX = barX + barWidth - handleWidth;
            var handleX = _minX + (_maxX - _minX) * Value;
            Handle.AssignLayoutBounds(new Rectangle(handleX, handleY, handleWidth, handleHeight));

            var barHighlighterWidth = handleX - barX;
            BarHighlighter.AssignLayoutBounds(new Rectangle(barX, barY, barHighlighterWidth, barHeight));
        }
        
        public override bool ContainsPoint(Vector2 point) {
            return Raylib.CheckCollisionPointRec(point, AbsoluteBounds);
        }

        public void OnDragStart(Vector2 mousePos) {
            _isBeingDragged = true;
            if (Raylib.CheckCollisionPointRec(mousePos, Handle.AbsoluteBounds)) {
                _xOffsetToHandlePos = mousePos.X - Handle.AbsoluteBounds.X;
            } 
            else if (Raylib.CheckCollisionPointRec(mousePos, Bar.AbsoluteBounds)) {
                _xOffsetToHandlePos = Handle.AbsoluteBounds.Width / 2f;
                OnDrag(mousePos);
            }
        }

        public void OnDrag(Vector2 mousePos) {
            var newX = mousePos.X - _xOffsetToHandlePos;
            newX = Math.Clamp(newX, _minX, _maxX);
            
            // Handle.AssignLayoutBounds(Handle.AbsoluteBounds with { X = newX });
            
            var newValue = 0f;
            if (_maxX > _minX) {
                newValue = (newX - _minX) / (_maxX - _minX);
            }

            if (!(Math.Abs(Value - newValue) > 0.001f)) return;
            Value = newValue;
            OnValueChanged?.Invoke(Value);
            _text.Text = Value.ToString("0.00");
        }

        public void OnDragEnd() {
            _isBeingDragged = false;
            _xOffsetToHandlePos = 0;
        }
        
        /*
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
        }*/
    }
}