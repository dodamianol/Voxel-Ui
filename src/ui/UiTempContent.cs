using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.elements;
using VoxelUiDemo.ui.elements.basic;
using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui;

public static class UiTempContent {

    private static readonly Color RootColor = new(45, 45, 45, 255);
    private static readonly Color RootChild1Color = new(65, 65, 65, 255);
    private static readonly Color SlotColor = new(35, 35, 35, 255);
    
    public static UiElement AssembleUi(int screenWidth, int screenHeight) {
        var rootCanvas = new UiRect {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
                Transform = new Transform { Offset = new Vector2I(0, 0), Size = new Vector2I(screenWidth, screenHeight) }},
            BackgroundColor = RootColor
        };

        AddTopLeft(rootCanvas);
        AddTexture(rootCanvas);
        AddStackPanel(rootCanvas);
        AddGridPanel(rootCanvas);
        AddSlider(rootCanvas);

        return rootCanvas;
    }
    
    private static void AddTopLeft(UiElement rootCanvas) {
        var topLeft = new UiRect {
            Layout = new LayoutComponent {
                Anchor = AnchorType.TopLeft,
                Transform = new Transform { Offset = new Vector2I(50, 50), Size = new Vector2I(800, 550) }
            },
            BackgroundColor = RootChild1Color
        };
        rootCanvas.AddChild(topLeft);
        
        var buttonAlignmentParent = new UiRect {
            Layout = new LayoutComponent { Anchor = AnchorType.Center, Transform = new Transform{ Size = new Vector2I(750, 500) }
            },
            BackgroundColor = RootChild1Color
        };

        topLeft.AddChild(buttonAlignmentParent);
        
        foreach (var anchor in Enum.GetValues<AnchorType>()) {
            var button = new UiButton {
                Layout = new LayoutComponent {
                    Anchor = anchor, Transform = new Transform { Size = new Vector2I(200, 50) }
                },
                OnClick = vector2 => Console.WriteLine("Button was clicked! (At: " + vector2 + ")"),
                
                Text = new UiText {
                    Layout = new LayoutComponent {
                        Anchor = AnchorType.Center, Transform = new Transform { Size = new Vector2I(200, 50) }
                    },
                    Text = "Click me!", FontSize = 24, TextColor = Color.White
                }
            };
            buttonAlignmentParent.AddChild(button);
        }
    }
    
    private static void AddTexture(UiElement rootCanvas) {
        var middle = new UiRect {
            Layout = new LayoutComponent {
                Anchor = AnchorType.MiddleRight,
                Transform = new Transform { Offset =  new Vector2I(-600, -147), Size = new Vector2I(200, 200) }
            },
            BackgroundColor = RootChild1Color
        };
        rootCanvas.AddChild(middle);
        
        var testTexture = new UiImage {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(75, 75) }},
            Texture = Raylib.LoadTexture("assets/textures/grass0.png")
        };
        middle.AddChild(testTexture);

        var transparentPanel = new UiRect {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
            Transform = new Transform{ Offset = new Vector2I(35, 0), Size = new Vector2I(70, 125) }},
            BackgroundColor = new Color(0, 0, 0, 150)
        };
        middle.AddChild(transparentPanel);
    }

    private static void AddStackPanel(UiElement rootCanvas) {
        var bottomRect = new UiRect {
            Layout = new LayoutComponent {
                Anchor = AnchorType.BottomCenter,
                Transform = new Transform { Offset = new Vector2I(0, -50), Size = new Vector2I(1020+50, 100+50) }
            },
            BackgroundColor = RootChild1Color
        };
        rootCanvas.AddChild(bottomRect);
        
        var stackPanel = new UiStackPanel { 
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
                Transform = new Transform{ Size = new Vector2I(1020, 100) }
            },
            Spacing = 15,
            Orientation = Orientation.Horizontal
            // Renderer = new RectRenderer{ Color = Color.DarkGray }
        };
        bottomRect.AddChild(stackPanel);
    
        for (var i = 0; i < 9; i++) {
            var stackPanelChild = new UiRect {
                Layout = new LayoutComponent{ Transform = new Transform{ Size = new Vector2I(100, 100) }}, 
                BackgroundColor = SlotColor
            };
            stackPanel.AddChild(stackPanelChild);
        }
    }

    private static void AddGridPanel(UiElement rootCanvas) {
        var middleCenter = new UiRect {
            Layout = new LayoutComponent{ Anchor = AnchorType.MiddleRight,
                Transform = new Transform{ Offset = new Vector2I(-50, 0), Size = new Vector2I(445+50, 445+50) }
                
            },
            BackgroundColor = RootChild1Color
        };
        rootCanvas.AddChild(middleCenter);
        
        var gridPanel = new UiGridPanel {
            Layout = new LayoutComponent{ Anchor = AnchorType.Center,
            Transform = new Transform{ Size = new Vector2I(445, 445) }},
            PrimaryOrientation =  Orientation.Vertical,
            ElementsPerPrimaryDimension = 4,
            SpacingX = 15,
            SpacingY = 15
        };
        middleCenter.AddChild(gridPanel);
        
        for (var i = 0; i < 15; i++) {
            var stackPanelChild = new UiRect {
                Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
                Transform = new Transform{ Size = new Vector2I(100, 100) }}, 
                BackgroundColor = SlotColor
            };
            gridPanel.AddChild(stackPanelChild);
        }
    }

    private static void AddSlider(UiElement rootCanvas) {
        var topRight = new UiRect {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopRight,
            Transform = new Transform{ Offset = new Vector2I(-50, 50), Size = new Vector2I(750, 260) }},
            BackgroundColor = RootChild1Color
        };
        rootCanvas.AddChild(topRight);

        var slider1 = new UiSlider {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 25), Size = new Vector2I(500, 50) }},
            Renderer = new RectRenderer(),
            ProgressColor = new Color(34, 197, 94, 255)
        };
        topRight.AddChild(slider1);
        
        var slider2 = new UiSlider {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 100), Size = new Vector2I(500, 50), Scale = new Vector2(1.1f) }},
            Renderer = new RectRenderer(),
            ProgressColor = new Color(239, 68, 68, 255)
        };
        topRight.AddChild(slider2);
        
        var slider3 = new UiSlider {
            Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
            Transform = new Transform{ Offset = new Vector2I(25, 175), Size = new Vector2I(500, 50), Scale = new Vector2(1.2f) }},
            Renderer = new RectRenderer()
        };
        topRight.AddChild(slider3);
        
        // var slider4 = new UiSlider() {
        //     Layout = new LayoutComponent{ Anchor = AnchorType.TopLeft,
        //         Transform = new Transform{ Offset = new Vector2I(25, 175), Size = new Vector2I(500, 50), Scale = new Vector2(3f) }},
        //     Renderer = new RectRenderer{ },
        // };
        // topRight.AddChild(slider4);
    }
}