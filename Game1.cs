using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static sprites.Entrenador;
using static sprites.Pokemon;

namespace sprites
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D textura;
        Sprite ala;
        Sprite chari;
        Sprite dig;
        Sprite gen;
        Sprite mew;
        Sprite psy;
        Sprite Ash;
        Entrenador ash;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textura = Content.Load<Texture2D>("alakazam");
            ala = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("charizard");
            chari = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("diglett");
            dig = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("gengar");
            gen = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("mewtwo");
            mew = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("psyduck");
            psy = new Sprite(textura, Vector2.Zero);

            textura = Content.Load<Texture2D>("ash");
            Ash = new Sprite(textura, Vector2.Zero);

            Pokemon[] equipo = new Pokemon[]
            {
                new Pokemon("Alakazam", 50, 150, 150, 90, 55, 110, 50, 90, "Paralizado", ala),
                new Pokemon("Charizard", 55, 180,180, 84, 78, 109, 85, 100, "Normal", chari),
                new Pokemon("Digglet", 81, 175,175, 83, 100, 85, 105, 78, "Normal", dig),
                new Pokemon("Gengar", 51, 170,170, 82, 83, 100, 100, 80, "Normal", gen),
                new Pokemon("Mewtwo", 48, 160,160, 110, 65, 55, 55, 45, "Normal", mew),
                new Pokemon("Psyduck", 50, 130,130, 65, 60, 130, 110, 110, "Normal", psy)
            };

            string[] medallas = new string[5]
            {
                "Medalla Roca",
                "Medalla Cascada",
                "Medalla Trueno",
                "Medalla Arcoíris",
                "Medalla Alma",
            };

            ash = new Entrenador("Ash", 500, medallas, equipo, Ash);
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
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Rectangle MostrarPokemon;

            for (int x = 0; x <= 5; x++)
            {
                MostrarPokemon = new Rectangle(50 + (x * 80), 150, 100, 100);
                _spriteBatch.Draw(ash.equipo[x].frente.textura, MostrarPokemon, Color.White);
            }

            Rectangle MostrarEntrenador;

            MostrarEntrenador = new Rectangle(250, 50, 100, 100);
            _spriteBatch.Draw(Ash.textura, MostrarEntrenador, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
