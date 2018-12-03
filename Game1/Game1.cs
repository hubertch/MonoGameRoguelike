using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueSharp;
using RogueSharp.MapCreation;
using System;
using Game1.Interfaces;
using Game1.Models;
using Game1.Core;
using System.Collections.Generic;
using Game1.Screens;
using Game1.Core.Delegates;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousState;
        IScreen activeScreen = null;

        private string messageToDraw = string.Empty;
        public SpriteBatch SpriteBatch { get => spriteBatch; }
        public ILevel Level { get; private set; }
        public IActor Player { get; private set; }
        public Random Random { get; private set; }

        private static bool _renderRequired = true;
        public CommandItemDrop CommandItemDrop { get; private set; }
        public TypeView TypeView = TypeView.Map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Random = new Random();

            Level = new Level(new RandomRoomsMapCreationStrategy<Map>(20, 40, 100, 10, 3));
            (Level as Level).SendMessage += DrawMessage;

            Player = new Player(Level.GetRandomWalkableCell(Random));

            MapScreen.Instance.Inicialize(this);

            activeScreen = MapScreen.Instance;

            previousState = Keyboard.GetState();
            Level.FirstView(Player);

            base.Initialize();
        }

        private void DrawMessage(object sender, string message)
        {
            messageToDraw = message;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            DrawView.Instance.LoadContent(Content, spriteBatch, Player, Level);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                var arraykeys = state.GetPressedKeys();

                if (state.IsKeyDown(arraykeys[0]) & !previousState.IsKeyDown(arraykeys[0]))
                {

                    var result = activeScreen.RespondToUserInput(arraykeys);

                    activeScreen = result.Screen;
                    if (result != null && result.IsSuccessAction)
                    {
                        if (result.IsNpcTurn) Level.NPCTurn(Player);

                        Level.ComputeFov(Player.X, Player.Y, Player.RangeFov);
                        Level.UpdateMapToDraw(Player);
                        _renderRequired = true;
                    }
                }
            }

            previousState = state;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (_renderRequired)
            {
                GraphicsDevice.Clear(Color.Black);

                activeScreen.Draw(DrawView.Instance);

                DrawView.Instance.DrawMessage(messageToDraw);

                _renderRequired = false;
                messageToDraw = string.Empty;
            }

            base.Draw(gameTime);
        }
    }
}
