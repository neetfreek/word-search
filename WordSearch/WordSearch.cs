using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WordSearch
{
    public class WordSearch : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            spriteAlphabet.Draw(spriteBatch, 'a', new Vector2(0f * 84f, 0 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'b', new Vector2(0f * 84f, 1 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'c', new Vector2(0f * 84f, 2 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'd', new Vector2(0f * 84f, 3 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'e', new Vector2(0f * 84f, 4 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'f', new Vector2(0f * 84f, 5 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'g', new Vector2(0f * 84f, 6 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'h', new Vector2(0f * 84f, 7 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'i', new Vector2(0f * 84f, 8 * 84f));

            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 0 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 1 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 2 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 3 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 4 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 5 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 6 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 7 * 84f));
            spriteLines.Draw(spriteBatch, '-', new Vector2(0f * 84f, 8 * 84f));

            spriteAlphabet.Draw(spriteBatch, 'j', new Vector2(1f * 84f, 0 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'k', new Vector2(1f * 84f, 1 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'l', new Vector2(1f * 84f, 2 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'm', new Vector2(1f * 84f, 3 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'n', new Vector2(1f * 84f, 4 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'o', new Vector2(1f * 84f, 5 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'p', new Vector2(1f * 84f, 6 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'q', new Vector2(1f * 84f, 7 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'r', new Vector2(1f * 84f, 8 * 84f));

            spriteAlphabet.Draw(spriteBatch, 's', new Vector2(2f * 84f, 0 * 84f));
            spriteAlphabet.Draw(spriteBatch, 't', new Vector2(2f * 84f, 1 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'u', new Vector2(2f * 84f, 2 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'v', new Vector2(2f * 84f, 3 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'w', new Vector2(2f * 84f, 4 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'x', new Vector2(2f * 84f, 5 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'y', new Vector2(2f * 84f, 6 * 84f));
            spriteAlphabet.Draw(spriteBatch, 'z', new Vector2(2f * 84f, 7 * 84f));

            spriteLines.Draw(spriteBatch, '-', new Vector2(3f * 84f, 0 * 84f));
            spriteLines.Draw(spriteBatch, '|', new Vector2(3f * 84f, 1 * 84f));
            spriteLines.Draw(spriteBatch, '\\', new Vector2(3f * 84f, 2 * 84f));
            spriteLines.Draw(spriteBatch, '/', new Vector2(3f * 84f, 3 * 84f));

            base.Draw(gameTime);
        }
    }
}