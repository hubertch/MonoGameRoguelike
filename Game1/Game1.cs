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
        ScreenManager screens = null;

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
            Player = new Player(Level.GetRandomWalkableCell(Random));

            screens = new ScreenManager(MapScreen.Instance,
                InventoryScreen.Instance);

            activeScreen = screens.MapScreen;

            CommandItemDrop = new CommandItemDrop(this);

            previousState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CommandSystem.Instance.LoadContent(this, screens);
            DrawView.Instance.LoadContent(Content, spriteBatch, Player, Level);
            Level.FirstView(Player);
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

                if (CheckKey(state, arraykeys[0]) && CommandSystem.Instance.Commands.ContainsKey(arraykeys[0]))
                {

                    var result = activeScreen.RespondToUserInput(arraykeys, screens);

                    activeScreen = result.Screen;
                    if (result != null && result.IsSuccessAction) RefreshMapView(result.IsNpcTurn);
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

                _renderRequired = false;
            }

            base.Draw(gameTime);
        }

        private bool CheckKey(KeyboardState state, Keys key)
        {
            return state.IsKeyDown(key) & !previousState.IsKeyDown(key);
        }

        private void RefreshMapView(bool isNpcTurn)
        {
            if (isNpcTurn) Level.NPCTurn(Player);

            Level.ComputeFov(Player.X, Player.Y, Player.RangeFov);
            Level.UpdateMapToDraw(Player);
            _renderRequired = true;
        }
    }
}
