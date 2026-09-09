using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.elements;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui;
public static class UiTempContent {

    public static void AddTopLeft(UiElement rootCanvas) {
        var topLeft = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Transform = new Transform{ Size = new Vector2I(500, 500) },
            BackgroundColor = Color.Gray,
        };

        rootCanvas.AddChild(topLeft);
        
        var testButton = new UiButton {
            AnchorType = AnchorType.BottomRight,
            Transform = new Transform{ Size = new Vector2I(200, 50) },
            BackgroundColor = Color.Gold,
            OnClick = () => Console.WriteLine("Button was clicked!"),
            Text = new UiText {
                AnchorType = AnchorType.Center,
                Text = "Click me!",
                FontSize = 24,
                TextColor = Color.White,
                Transform = new Transform{ Size = new Vector2I(200, 50) },
                AlignH = TextAlignmentHorizontal.Center,
                AlignV = TextAlignmentVertical.Center
            }
        };
        
        topLeft.AddChild(testButton);

    }
    
    public static void AddTexture(UiElement rootCanvas) {
        var testTexture = new UiImage() {
            AnchorType = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(50, 50) },
            Texture = Raylib.LoadTexture("assets/textures/grass0.png")
        };
        rootCanvas.AddChild(testTexture);

        var transparentPanel = new UiPanel() {
            AnchorType = AnchorType.Center,
            Transform = new Transform{ Offset = new Vector2I(100, 0), Size = new Vector2I(200, 200) },
            BackgroundColor = new Color(0, 0, 0, 200),
        };
        rootCanvas.AddChild(transparentPanel);
    }

    public static void AddStackPanel(UiElement rootCanvas) {
        var stackPanel = new UiStackPanel() {
            AnchorType = AnchorType.BottomCenter,
            Transform = new Transform{ Offset = new Vector2I(0, -50), Size = new Vector2I(1020, 100) },
            Spacing = 115,
            Orientation = Orientation.Horizontal,
            BackgroundColor = Color.DarkGray
        };
        rootCanvas.AddChild(stackPanel);
    
        for (var i = 0; i < 9; i++) {
            var stackPanelChild = new UiPanel() {
                AnchorType = AnchorType.Center,
                Transform = new Transform{ Size = new Vector2I(100, 100) }, 
                BackgroundColor = Color.Orange
            };
            stackPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddGridPanel(UiElement rootCanvas) {
        var gridPanel = new UiGridPanel() {
            AnchorType = AnchorType.MiddleRight,
            Transform = new Transform{ Offset = new Vector2I(-50, 0), Size = new Vector2I(500, 500) },
            PrimaryOrientation =  Orientation.Vertical,
            ElementsPerPrimarayDimension = 4,
            SpacingX = 115,
            SpacingY = 115
        };
        rootCanvas.AddChild(gridPanel);
        
        for (var i = 0; i < 11; i++) {
            var stackPanelChild = new UiPanel() {
                AnchorType = AnchorType.Center,
                Transform = new Transform{ Size = new Vector2I(100, 100) }, 
                BackgroundColor = Color.Orange
            };
            gridPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddSlider(UiElement rootCanvas) {
        var topRight = new UiPanel() {
            AnchorType = AnchorType.TopRight,
            Transform = new Transform{ Offset = new Vector2I(-50, 50), Size = new Vector2I(750, 250) },
            BackgroundColor = Color.Gray,
        };
        rootCanvas.AddChild(topRight);

        var sliderBackground = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(20, 20), Size = new Vector2I(500, 50) },
            BackgroundColor = Color.Brown,
        };
        topRight.AddChild(sliderBackground);

        var slider = new UiSlider() {
            AnchorType = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(500, 50) },
            BackgroundColor = Color.Orange,
        };
        sliderBackground.AddChild(slider);
        
        var sliderBackground2 = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(20, 95), Size = new Vector2I(500, 50), Scale = new Vector2(1.5f) },
            BackgroundColor = Color.Brown,
            
        };
        topRight.AddChild(sliderBackground2);
        
        var slider2 = new UiSlider() {
            AnchorType = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(500, 50) },
            BackgroundColor = Color.Orange,
        };
        sliderBackground2.AddChild(slider2);
    }

}

