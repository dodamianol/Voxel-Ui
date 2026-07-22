using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.elements;

namespace VoxelUiDemo.ui;
public static class UiTempContent {

    public static void AddTopLeft(UiElement rootCanvas) {
        var topLeft = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Size = new Vector2(500, 500),
            BackgroundColor = Color.Gray,
        };

        rootCanvas.AddChild(topLeft);
        
        var testButton = new UiButton {
            AnchorType = AnchorType.BottomRight,
            Size = new Vector2(200, 50),
            BackgroundColor = Color.Gold,
            OnClick = () => Console.WriteLine("Button was clicked!"),
            Text = new UiText {
                AnchorType = AnchorType.Center,
                Text = "Click me!",
                FontSize = 24,
                TextColor = Color.White,
                Size = new Vector2(200, 50),
                AlignH = TextAlignmentHorizontal.Center,
                AlignV = TextAlignmentVertical.Center
            }
        };
        
        topLeft.AddChild(testButton);

    }
    
    public static void AddTexture(UiElement rootCanvas) {
        var testTexture = new UiImage() {
            AnchorType = AnchorType.Center,
            Size = new Vector2(50, 50),
            Texture = Raylib.LoadTexture("assets/textures/grass0.png")
        };
        rootCanvas.AddChild(testTexture);

        var transparentPanel = new UiPanel() {
            AnchorType = AnchorType.Center,
            Offset = new Vector2(100, 0),
            Size = new Vector2(200, 200),
            BackgroundColor = new Color(0, 0, 0, 200),
        };
        rootCanvas.AddChild(transparentPanel);
    }

    public static void AddStackPanel(UiElement rootCanvas) {
        var stackPanel = new UiStackPanel() {
            AnchorType = AnchorType.BottomCenter,
            Size = new Vector2(1020, 100),
            Offset = new Vector2(0, -50),
            Spacing = 115,
            Orientation = Orientation.Horizontal,
            BackgroundColor = new Color(0, 0, 0, 0)
        };
        rootCanvas.AddChild(stackPanel);
        
        for (var i = 0; i < 9; i++) {
            var stackPanelChild = new UiPanel() {
                AnchorType = AnchorType.Center,
                Size = new Vector2(100, 100),
                BackgroundColor = Color.Orange
            };
            stackPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddGridPanel(UiElement rootCanvas) {
        var gridPanel = new UiGridPanel() {
            AnchorType = AnchorType.MiddleRight,
            Size = new Vector2(500, 500),
            Offset = new Vector2(-50, 0),
            PrimaryOrientation =  Orientation.Vertical,
            ElementsPerPrimarayDimension = 4,
            SpacingX = 115,
            SpacingY = 115
        };
        rootCanvas.AddChild(gridPanel);
        
        for (var i = 0; i < 11; i++) {
            var stackPanelChild = new UiPanel() {
                AnchorType = AnchorType.Center,
                Size = new Vector2(100, 100),
                BackgroundColor = Color.Orange
            };
            gridPanel.AddChild(stackPanelChild);
        }
    }

    public static void AddSlider(UiElement rootCanvas) {
        var topRight = new UiPanel() {
            AnchorType = AnchorType.TopRight,
            Offset = new Vector2(-50, 50),
            Size = new Vector2(750, 250),
            BackgroundColor = Color.Gray,
        };
        rootCanvas.AddChild(topRight);

        var sliderBackground = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Size = new Vector2(450, 75),
            Offset = new Vector2(20, 20),
            BackgroundColor = Color.Brown,
        };
        topRight.AddChild(sliderBackground);

        var slider = new UiSlider() {
            AnchorType = AnchorType.Center,
            Size = new Vector2(450, 75),
            BackgroundColor = Color.Orange,
        };
        sliderBackground.AddChild(slider);
    }

}

