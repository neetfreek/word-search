using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WordSearch
{
    public class ManagerDisplay
    {
        public Matrix ScaleMatrix;

        private Vector2 screenMid;
        public Vector2 ScreenMid
        {
            get { return screenMid; }
            set { screenMid = value; }

        }

        private int heightTarget;
        private int widthTarget;
        //private int heightTargetScaled;
        //private int widthTargetScaled;
        private readonly int heightVirtual;
        private readonly int widthVirtual;

        private readonly float scaleHeight;
        private readonly float scaleWidth;

        public int HeightTargetScaled
        {
            get { return (int)(heightTarget * ScaleHeight); }
        }
        public int WidthTargetScaled
        {
            get { return (int)(widthTarget * ScaleWidth); }
        }

        public float ScaleHeight
        {
            get { return scaleHeight; }
        }
        public float ScaleWidth
        {
            get { return scaleWidth; }
        }

        public ManagerDisplay(GraphicsDeviceManager gdManager)
        {
            // Set display to full screen
            gdManager.IsFullScreen = true;
            gdManager.ApplyChanges();
                        
            // Set heights, widths
            heightVirtual = 1080;
            widthVirtual = 1920;
            // Set scales
            heightTarget = gdManager.GraphicsDevice.DisplayMode.Height; // previously heightTarget = gdManager.GraphicsDevice.Viewport.Height;
            widthTarget = gdManager.GraphicsDevice.DisplayMode.Width; // previously widthTarget = gdManager.GraphicsDevice.Viewport.Width;
            scaleHeight = (float)heightTarget / heightVirtual;
            scaleWidth = (float)widthTarget / widthVirtual;

            // Set output display height, width
            gdManager.PreferredBackBufferHeight = HeightTargetScaled;
            gdManager.PreferredBackBufferWidth = WidthTargetScaled;
            System.Console.WriteLine($"gdManager.PreferredBackBufferHeight: {gdManager.PreferredBackBufferHeight}, gdManager.PreferredBackBufferWidth: {gdManager.PreferredBackBufferWidth}");
            gdManager.ApplyChanges();

            // Used in Sprite.cs to draw sprites in scale
            ScaleMatrix = Matrix.CreateScale(scaleHeight, scaleHeight, 1f);
        }

        public void FullScreen(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = (int)WidthTargetScaled;
            graphics.PreferredBackBufferWidth = (int)HeightTargetScaled;
            graphics.IsFullScreen = true;

            graphics.ApplyChanges();
        }
    }
}