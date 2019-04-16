using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WordSearch
{
    public class ManagerDisplay
    {
        public static Matrix ScaleMatrix;

        private int widthTarget;
        private int heightTarget;
        private readonly int widthVirtual;
        private readonly int heightVirtual;

        private readonly float scaleWidthScreen;
        private readonly float scaleHeightScreen;

        public int WidthTargetScaled
        {
            get { return (int)(widthTarget * ScaleWidth); }
        }
        public int HeightTargetScaled
        {
            get { return (int)(heightTarget * ScaleHeight); }
        }

        public float ScaleWidth
        {
            get { return scaleWidthScreen; }
        }
        public float ScaleHeight
        {
            get { return scaleHeightScreen; }
        }

        public ManagerDisplay(GraphicsDeviceManager gdManager)
        {
            // Setup preferred BackBuffer dimensions
            gdManager.PreferredBackBufferWidth = 1920;
            gdManager.PreferredBackBufferHeight = 1080;

            // Setup viewport
            gdManager.GraphicsDevice.PresentationParameters.BackBufferWidth = gdManager.PreferredBackBufferWidth;
            gdManager.GraphicsDevice.PresentationParameters.BackBufferHeight = gdManager.PreferredBackBufferHeight;
            gdManager.GraphicsDevice.Viewport = new Viewport(0, 0, gdManager.PreferredBackBufferWidth, gdManager.PreferredBackBufferHeight);

            // Set heights, widths
            widthVirtual = 1920;
            heightVirtual = 1080;
            widthTarget = gdManager.GraphicsDevice.DisplayMode.Width;
            heightTarget = gdManager.GraphicsDevice.DisplayMode.Height;

            // Set scales
            scaleWidthScreen = ((float)widthVirtual / (float)widthTarget);
            scaleHeightScreen = ((float)heightVirtual / (float)heightTarget);
            float scaleWidthSprite = ((float)widthTarget / (float)widthVirtual);
            float scaleHeightSprite = ((float)heightTarget / (float)heightVirtual);

            // Reset, back buffer dimensions, viewport with new BackBuffer dimensions
            gdManager.PreferredBackBufferHeight = HeightTargetScaled;
            gdManager.PreferredBackBufferWidth = WidthTargetScaled;
            gdManager.GraphicsDevice.Viewport = new Viewport(0, 0, gdManager.PreferredBackBufferWidth, gdManager.PreferredBackBufferHeight);         
            gdManager.ApplyChanges();

            // Set display to full screen
            gdManager.IsFullScreen = true;
            gdManager.ApplyChanges();

            // Used in Tile.cs to draw sprites in scale
            ScaleMatrix = Matrix.CreateScale(scaleWidthSprite, scaleHeightSprite, 1f);
        }
    }
}