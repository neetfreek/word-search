using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WordSearch
{
    public class WordSearch : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch sb;

        private Sprite spriteAlphabet;
        private Sprite spriteLines;

        public WordSearch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D textureAlphabet = Content.Load<Texture2D>("alphabet");
            spriteAlphabet = new Sprite(textureAlphabet, 2, 13);
            Texture2D textureLines = Content.Load<Texture2D>("lines");
            spriteLines = new Sprite(textureLines, 1, 4);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Test draw all sprites in both sprite atlasses
            spriteAlphabet.Draw(sb, 'a', new Vector2(0f * spriteAlphabet.HeightSprite, 0f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'b', new Vector2(0f * spriteAlphabet.HeightSprite, 1f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'c', new Vector2(0f * spriteAlphabet.HeightSprite, 2f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'd', new Vector2(0f * spriteAlphabet.HeightSprite, 3f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'e', new Vector2(0f * spriteAlphabet.HeightSprite, 4f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'f', new Vector2(0f * spriteAlphabet.HeightSprite, 5f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'g', new Vector2(0f * spriteAlphabet.HeightSprite, 6f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'h', new Vector2(0f * spriteAlphabet.HeightSprite, 7f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'i', new Vector2(0f * spriteAlphabet.HeightSprite, 8f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'j', new Vector2(0f * spriteAlphabet.HeightSprite, 9f * spriteAlphabet.WidthSprite), 0.5f);

            spriteAlphabet.Draw(sb, 'k', new Vector2(1f * spriteAlphabet.HeightSprite, 0f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'l', new Vector2(1f * spriteAlphabet.HeightSprite, 1f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'm', new Vector2(1f * spriteAlphabet.HeightSprite, 2f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'n', new Vector2(1f * spriteAlphabet.HeightSprite, 3f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'o', new Vector2(1f * spriteAlphabet.HeightSprite, 4f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'p', new Vector2(1f * spriteAlphabet.HeightSprite, 5f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'q', new Vector2(1f * spriteAlphabet.HeightSprite, 6f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'r', new Vector2(1f * spriteAlphabet.HeightSprite, 7f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 's', new Vector2(1f * spriteAlphabet.HeightSprite, 8f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 't', new Vector2(1f * spriteAlphabet.HeightSprite, 9f * spriteAlphabet.WidthSprite), 0.5f);

            spriteAlphabet.Draw(sb, 'u', new Vector2(2f * spriteAlphabet.HeightSprite, 0f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'v', new Vector2(2f * spriteAlphabet.HeightSprite, 1f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'w', new Vector2(2f * spriteAlphabet.HeightSprite, 2f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'x', new Vector2(2f * spriteAlphabet.HeightSprite, 3f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'y', new Vector2(2f * spriteAlphabet.HeightSprite, 4f * spriteAlphabet.WidthSprite), 0.5f);
            spriteAlphabet.Draw(sb, 'z', new Vector2(2f * spriteAlphabet.HeightSprite, 5f * spriteAlphabet.WidthSprite), 0.5f);

            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 0f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 1f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 2f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 3f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 4f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 5f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 6f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 7f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 8f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '-', new Vector2(1f * spriteAlphabet.HeightSprite, 9f * spriteAlphabet.WidthSprite), 0.5f);

            spriteLines.Draw(sb, '-', new Vector2(3f * spriteAlphabet.HeightSprite, 0f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '|', new Vector2(3f * spriteAlphabet.HeightSprite, 1f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '\\', new Vector2(3f * spriteAlphabet.HeightSprite, 2f * spriteAlphabet.WidthSprite), 0.5f);
            spriteLines.Draw(sb, '/', new Vector2(3f * spriteAlphabet.HeightSprite, 3f * spriteAlphabet.WidthSprite), 0.5f);

            base.Draw(gameTime);
        }
    }
}