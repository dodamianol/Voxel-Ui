using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.elements;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui;
public static class UiTempContent {

    public static void AddTopLeft(UiElement rootCanvas) {
        var topLeft = new UiPanel() {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Size = new Vector2I(500, 500) }},
            BackgroundColor = Color.Gray,
        };

        rootCanvas.AddChild(topLeft);
        
        var testButton = new UiButton {
            Layout = new LayoutComponent{ Anchor = AnchorType.BottomRight,
            Transform = new Transform{ Size = new Vector2I(200, 50) }},
            Renderer = new RectRenderer{ Color = Color.Gold },
            OnClick = () => Console.WriteLine("Button was clicked!"),
            Text = new UiText {
                Layout = new LayoutComponent{ Anchor = AnchorType.Center,
                Transform = new Transform{ Size = new Vector2I(200, 50) }},
                Text = "Click me!",
                FontSize = 24,
                TextColor = Color.White,
                AlignH = TextAlignmentHorizontal.Center,
                AlignV = TextAlignmentVertical.Center
            }
        };
        
        topLeft.AddChild(testButton);

    }
    
    public static void AddTexture(UiElement rootCanvas) {
        var testTexture = new UiImage() {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(50, 50) }},
            Texture = Raylib.LoadTexture("assets/textures/grass0.png")
        };
        rootCanvas.AddChild(testTexture);

        var transparentPanel = new UiPanel() {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
            Transform = new Transform{ Offset = new Vector2I(100, 0), Size = new Vector2I(200, 200) }},
            BackgroundColor = new Color(0, 0, 0, 200),
        };
        rootCanvas.AddChild(transparentPanel);
    }

    public static void AddStackPanel(UiElement rootCanvas) {
        var stackPanel = new UiStackPanel() { 
            Layout = new LayoutComponent{ Anchor = AnchorType.BottomCenter,
            Transform = new Transform{ Offset = new Vector2I(0, -50), Size = new Vector2I(1020, 100) }},
            Spacing = 115,
            Orientation = Orientation.Horizontal,
            Renderer = new RectRenderer{ Color = Color.DarkGray }
        };
        rootCanvas.AddChild(stackPanel);
    
        for (var i = 0; i < 9; i++) {
            var stackPanelChild = new UiPanel() {
                Layout = new LayoutComponent{ Transform = new Transform{ Size = new Vector2I(100, 100) }}, 
                BackgroundColor = Color.Orange
            };
            stackPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddGridPanel(UiElement rootCanvas) {
        var gridPanel = new UiGridPanel() {
            Layout = new LayoutComponent{ Anchor = AnchorType.MiddleRight,
            Transform = new Transform{ Offset = new Vector2I(-50, 0), Size = new Vector2I(500, 500) }},
            PrimaryOrientation =  Orientation.Vertical,
            ElementsPerPrimarayDimension = 4,
            SpacingX = 115,
            SpacingY = 115
        };
        rootCanvas.AddChild(gridPanel);
        
        for (var i = 0; i < 11; i++) {
            var stackPanelChild = new UiPanel() {
                Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
                Transform = new Transform{ Size = new Vector2I(100, 100) }}, 
                BackgroundColor = Color.Orange
            };
            gridPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddSlider(UiElement rootCanvas) {
        var topRight = new UiPanel() {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopRight,
            Transform = new Transform{ Offset = new Vector2I(-50, 50), Size = new Vector2I(750, 260) }},
            BackgroundColor = Color.Gray,
        };
        rootCanvas.AddChild(topRight);

        var slider = new UiSlider() {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 25), Size = new Vector2I(500, 50) }},
            Renderer = new RectRenderer{ Color = Color.Orange}
        };
        topRight.AddChild(slider);
        
        var slider2 = new UiSlider() {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 100), Size = new Vector2I(500, 50), Scale = new Vector2(1.1f) }},
            Renderer = new RectRenderer{ Color = Color.Orange}
        };
        topRight.AddChild(slider2);
        
        var slider3 = new UiSlider() {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 175), Size = new Vector2I(500, 50), Scale = new Vector2(1.2f) }},
            Renderer = new RectRenderer{ Color = Color.Orange},
        };
        topRight.AddChild(slider3);
    }

}

