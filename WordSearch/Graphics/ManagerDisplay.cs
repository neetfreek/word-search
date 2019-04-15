using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WordSearch
{
    public class ManagerDisplay
    {
        public Matrix ScaleMatrix;

        private int heightTarget;
        private int widthTarget;
        private readonly int heightVirtual;
        private readonly int widthVirtual;

        private readonly float scaleHeightScreen;
        private readonly float scaleWidthScreen;

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
            get { return scaleHeightScreen; }
        }
        public float ScaleWidth
        {
            get { return scaleWidthScreen; }
        }

        public ManagerDisplay(GraphicsDeviceManager gdManager)
        {
            // Setup preferred BackBuffer dimensions
            gdManager.PreferredBackBufferHeight = 1080;
            gdManager.PreferredBackBufferWidth = 1920;

            // Setup viewport
            gdManager.GraphicsDevice.PresentationParameters.BackBufferWidth = gdManager.PreferredBackBufferWidth;
            gdManager.GraphicsDevice.PresentationParameters.BackBufferHeight = gdManager.PreferredBackBufferHeight;
            gdManager.GraphicsDevice.Viewport = new Viewport(0, 0, gdManager.PreferredBackBufferHeight, gdManager.PreferredBackBufferWidth);

            // Set heights, widths
            heightVirtual = 1080;
            widthVirtual = 1920;
            heightTarget = gdManager.GraphicsDevice.DisplayMode.Height;
            widthTarget = gdManager.GraphicsDevice.DisplayMode.Width;

            // Set scales
            scaleHeightScreen = ((float)heightVirtual / (float)heightTarget);
            scaleWidthScreen = ((float)widthVirtual / (float)widthTarget);
            float scaleHeightSprite = ((float)heightTarget / (float)heightVirtual);
            float scaleWidthSprite = ((float)widthTarget / (float)widthVirtual);

            // Reset viewport with new BackBuffer dimensions
            gdManager.PreferredBackBufferHeight = HeightTargetScaled;
            gdManager.PreferredBackBufferWidth = WidthTargetScaled;
            gdManager.GraphicsDevice.Viewport = new Viewport(0, 0, gdManager.PreferredBackBufferHeight, gdManager.PreferredBackBufferWidth);         
            gdManager.ApplyChanges();

            // Set display to full screen
            gdManager.IsFullScreen = true;
            gdManager.ApplyChanges();

            // Used in Sprite.cs to draw sprites in scale
            ScaleMatrix = Matrix.CreateScale(scaleWidthSprite, scaleHeightSprite, 1f);
        }
    }
}