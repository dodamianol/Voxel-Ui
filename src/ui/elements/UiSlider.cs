using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.components;
using VoxelUiDemo.ui.elements.basic;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.elements {
    public class UiSlider : UiElement {
        public float Value { get; private set; }
        public Action<float>? OnValueChanged { get; set; }
        
        public Color BarColor { get; set; } = new(63, 63, 70, 255);
        public Color HandleColor { get; set; } = new(212, 212, 216, 255);
        public Color HandleHoverColor { get; set; } = new(244, 244, 245, 255);
        public Color HandlePressedColor { get; set; } = new(161, 161, 170, 255);
        public Color ProgressColor {
            get => _barHighlighter.BackgroundColor;
            set => _barHighlighter.BackgroundColor = value;
        } 
        
        private readonly InteractableComponent _interactable = new();
        
        private float _xOffsetToHandlePos;
        private float _minX, _maxX;
        
        private readonly UiRect _bar = null!;
        private readonly UiRect _handle = null!;
        private readonly UiText _text = null!;
        private readonly UiRect _barHighlighter = null!;

        public UiRect Bar {
            get => _bar;
            init {
                Children.Remove(_bar);
                _bar = value;
                AddChild(_bar);
            }
        }

        public UiRect Handle {
            get => _handle;
            init {
                Children.Remove(_handle);
                _handle = value;
                AddChild(_handle);
            }
        }
        
        public UiText Text {
            get => _text;
            init {
                Children.Remove(_text);
                _text = value;
                AddChild(_text);
            }
        }
        
        public UiRect BarHighlighter {
            get => _barHighlighter;
            init {
                Children.Remove(_barHighlighter);
                _barHighlighter = value;
                AddChild(_barHighlighter);
            }
        }
        
        public UiSlider() {
            Interactable = _interactable;
            _interactable.OnPress += OnPress;
            _interactable.OnDragStart += OnDragStart;
            _interactable.OnDrag += OnDrag;
            _interactable.OnDragEnd += OnDragEnd;
            
            var currentLayout = Layout;
            currentLayout.Transform = new Transform{ Size = new Vector2I(500, 50) };
            Layout = currentLayout;

            Bar = new UiRect { BackgroundColor = BarColor };
            Handle = new UiRect { BackgroundColor = HandleColor };
            Text = new UiText {
                Layout = new LayoutComponent { 
                    Anchor = AnchorType.MiddleRight, 
                    Transform = new Transform{ Offset = new Vector2I(-15, 0) } 
                },
                TextColor = Color.White, Text = "0,00", AlignH = TextAlignmentHorizontal.Right
            };
            BarHighlighter = new UiRect { BackgroundColor = new Color(59, 130, 246, 255) };
            
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

        private void SetValue(float value) {
            value = Math.Clamp(value, 0f, 1f);

            if (MathF.Abs(Value - value) <= 0.001f) return;

            Value = value;
            OnValueChanged?.Invoke(Value);
            _text.Text = Value.ToString("0.00");
        }
        
        private float CalculateValueFromMousePosition(Vector2 mousePos, float handleOffset = 0f) {
            var localHandleX = 
                (mousePos.X - handleOffset - Layout.FinalTransform.Offset.X)
                / Layout.FinalTransform.Scale.X;

            var clampedHandleX = Math.Clamp(localHandleX, _minX, _maxX);

            if (_maxX <= _minX) return 0f;

            return (clampedHandleX - _minX) / (_maxX - _minX);
        }
        
        private void OnPress(Vector2 mousePos) {
            var offset = Handle.Layout.FinalTransform.Size.X / 2;
            SetValue(CalculateValueFromMousePosition(mousePos, offset));
        }
        
        private void OnDragStart(Vector2 mousePos) {
            if (Raylib.CheckCollisionPointRec(mousePos, Handle.Layout.FinalTransform.ToRectangle())) {
                _xOffsetToHandlePos = mousePos.X - Handle.Layout.FinalTransform.Offset.X;
            } 
            else {
                _xOffsetToHandlePos = Handle.Layout.FinalTransform.Size.X / 2f;
            }
        }

        private void OnDrag(Vector2 mousePos) {
            SetValue(CalculateValueFromMousePosition(mousePos, _xOffsetToHandlePos));
        }

        private void OnDragEnd() {
            _xOffsetToHandlePos = 0;
        }
        
        public override void Update(Vector2 mousePos) {
            base.Update(mousePos);

            if (_interactable.IsPressed) {
                _handle.BackgroundColor = HandlePressedColor;
            } else if (_handle.ContainsPoint(mousePos)) {
                _handle.BackgroundColor = HandleHoverColor;
            } else {
                _handle.BackgroundColor = HandleColor;
            }
        }
    }
}
