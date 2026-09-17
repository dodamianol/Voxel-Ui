using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.voxel_engine;

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
            var currentLayout = Layout;
            currentLayout.Transform = new Transform{ Size = new Vector2I(500, 50) };
            Layout = currentLayout;

            Bar = new UiRect { BackgroundColor = Color.LightGray, BlocksInput = false };
            Handle = new UiRect { BackgroundColor = NormalColor, BlocksInput = false };
            Text = new UiText {
                Layout = new LayoutComponent { 
                    Anchor = AnchorType.MiddleRight, 
                    Transform = new Transform{ Offset = new Vector2I(-15, 0) } 
                },
                BlocksInput =  false, TextColor = Color.White, Text = "0,00", AlignH = TextAlignmentHorizontal.Right
            };
            BarHighlighter = new UiRect { BackgroundColor = Color.DarkGreen, BlocksInput = false };
        }

        protected override void ArrangeChildren() {
            var sliderSize = Layout.Transform.Size;

            var barWidth = sliderSize.X * 0.8f;
            var barHeight = sliderSize.Y * 0.1f;
            var barX = 20f; 
            var barY = (sliderSize.Y - barHeight) / 2f;
    
            Bar.AssignTransform(new Transform {
                Offset = Vector2I.Round(new Vector2(barX, barY)), 
                Size = Vector2I.Round(new Vector2(barWidth, barHeight))
            });

            var handleWidth = (sliderSize.Y * 0.8f) / 2;
            var handleHeight = sliderSize.Y * 0.7f;
            var handleY = (sliderSize.Y - handleHeight) / 2f;
    
            _minX = barX;
            _maxX = barX + barWidth - handleWidth;
            var handleX = _minX + (_maxX - _minX) * Value;
    
            Handle.AssignTransform(new Transform {
                Offset = Vector2I.Round(new Vector2(handleX, handleY)), 
                Size = Vector2I.Round(new Vector2(handleWidth, handleHeight))
            });
    
            var barHighlighterWidth = handleX - barX;
            BarHighlighter.AssignTransform(new Transform {
                Offset = Vector2I.Round(new Vector2(barX, barY)), 
                Size = Vector2I.Round(new Vector2(barHighlighterWidth, barHeight))
            });
        }
        
        public override bool ContainsPoint(Vector2 point) {
            return Raylib.CheckCollisionPointRec(point, Layout.FinalTransform.ToRectangle());
        }

        public void OnDragStart(Vector2 mousePos) {
            _isBeingDragged = true;
            
            if (Raylib.CheckCollisionPointRec(mousePos, Handle.Layout.FinalTransform.ToRectangle())) {
                _xOffsetToHandlePos = mousePos.X - Handle.Layout.FinalTransform.Offset.X;
            } 
            else if (Raylib.CheckCollisionPointRec(mousePos, Bar.Layout.FinalTransform.ToRectangle())) {
                _xOffsetToHandlePos = Handle.Layout.FinalTransform.Size.X / 2f;
                OnDrag(mousePos);
            }
        }

        public void OnDrag(Vector2 mousePos) {
            var desiredGlobalHandleX = mousePos.X - _xOffsetToHandlePos;
            var globalDistance = desiredGlobalHandleX - Layout.FinalTransform.Offset.X;
            var scaleX = Layout.FinalTransform.Scale.X;
            var localNewX = globalDistance / scaleX; 
    
            localNewX = Math.Clamp(localNewX, _minX, _maxX);

            var newValue = 0f;
            if (_maxX > _minX) {
                newValue = (localNewX - _minX) / (_maxX - _minX);
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
    }
}
