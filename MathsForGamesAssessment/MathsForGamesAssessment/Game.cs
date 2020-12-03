using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Game
    {
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        private readonly int _worldHeight = 24;
        private readonly int _worldWidth = 33;

        public static int CurrentSceneIndex
        { get { return _currentSceneIndex; } }

        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        } //Get Scene function

        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        } //Get Current Scene function

        public static Scene GetScenes(int index)
        { return _scenes[index]; }

        /// <summary>
        /// Adds a Scene to the _scenes array
        /// </summary>
        /// <param name="scene">The Scene being added to _scenes</param>
        /// <returns></returns>
        public static int AddScene(Scene scene)
        {
            if (scene == null)
                return -1;

            Scene[] tempArray = new Scene[_scenes.Length + 1];

            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            int index = _scenes.Length;

            tempArray[index] = scene;

            _scenes = tempArray;

            return index;
        } //Add Scene function

        /// <summary>
        /// Removes a Scene from the _scenes array by passing in the Scene that is to be removed
        /// </summary>
        /// <param name="scene">The Scene being removed</param>
        /// <returns></returns>
        public static bool RemoveScene(Scene scene)
        {
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            Scene[] tempArray = new Scene[_scenes.Length - 1];

            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        } //Remove Scene by scene function

        /// <summary>
        /// Sets the current scene in the game to be the scene at the given index
        /// </summary>
        /// <param name="index">The index of the scene to switch to</param>
        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        } //Set Current Scene

        /// <summary>
        /// Returns true while the KeyboardKey with the passed in ASCII value is being pressed
        /// </summary>
        /// <param name="key">The ASCII value of the key to check</param>
        /// <returns></returns>
        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        } //Get Key Down function

        /// <summary>
        /// Returns true while if key was pressed once
        /// </summary>
        /// <param name="key">The ASCII value of the key to check</param>
        /// <returns></returns>
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        } //Get Key Pressed function

        public Game()
        {
            _scenes = new Scene[0];
        } //Game Constructor

        public void Start()
        {
            Console.Title = "Maths For Games Assessment";

            char facing = GetPlayerTurningMethod();
            Console.Clear();
            DisplayControls(facing);

            int zombieNumber = 0;
            zombieNumber = GetZombieNumber(zombieNumber);

            Raylib.InitWindow(1024, 760, "Maths For Games Assessment");
            Raylib.SetTargetFPS(30);

            Console.CursorVisible = false;

            Scene scene1 = new Scene();
            Scene scene2 = new Scene();
            Scene scene3 = new Scene();

            Player player = new Player(1, 1, facing);

            Tile[,] tiles = new Tile[_worldWidth, _worldHeight];

            for (int i = 0; i < _worldWidth; i++)
                for (int j = 0; j < _worldHeight; j++)
                {
                    tiles[i, j] = new Tile(i + .05f, j + 0.5f, RandomStoneSprite());
                    scene1.AddActor(tiles[i, j]);
                }

            int startingSceneIndex = AddScene(scene1);
            AddScene(scene2);
            AddScene(scene3);

            scene1.AddActor(player);

            Zombie[] zombies = new Zombie[zombieNumber];

            for (int i = 0; i < zombieNumber; i++)
            {
                Random r = new Random();
                zombies[i] = new Zombie(r.Next(0, Raylib.GetScreenWidth() / 32), r.Next(0, Raylib.GetScreenHeight() / 32), player);

                scene1.AddActor(zombies[i]);
            } //For every Zombie you'd like to create

            SetCurrentScene(startingSceneIndex);
        } //Start

        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            GameManager.CheckWin();

            _scenes[_currentSceneIndex].Update(deltaTime);
        } //Update

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);

            _scenes[CurrentSceneIndex].Draw();

            Raylib.EndDrawing();
        } //Draw

        public void End()
        {

        } //End

        public void Run()
        {
            Start();

            while (!GameManager.GameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();

                Update(deltaTime);
                Draw();

                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            } //While game isn't over and Raylib window shouldn't close

            End();
        } //Run

        public Sprite RandomStoneSprite()
        {
            Sprite sprite;

            int randInt = new Random().Next(7, 10);

            switch (randInt)
            {
                case 7:
                    sprite = new Sprite("Images/PNG/Tiles/tile_07.png");
                    break;

                case 8:
                    sprite = new Sprite("Images/PNG/Tiles/tile_08.png");
                    break;

                case 9:
                    sprite = new Sprite("Images/PNG/Tiles/tile_09.png");
                    break;

                case 10:
                    sprite = new Sprite("Images/PNG/Tiles/tile_10.png");
                    break;

                default:
                    sprite = new Sprite("Images/PNG/Tiles/tile_10.png");
                    break;
            } //randInt switch

            return sprite;
        } //Random Stone Sprite function

        public char GetPlayerTurningMethod()
        {
            char facing;
            do
            {
                Console.WriteLine("How would you like to change the direction of your Player?");
                Console.WriteLine("1: Look where the cursor is (Recommended)\n2: Turn left using Left Arrow and turn right using Right Arrow \n3: Look where the Player is moving");
                facing = Console.ReadKey().KeyChar;
                Console.Clear();
            }
            while (facing != '1' && facing != '2' && facing != '3');

            switch (facing)
            {
                case '1': //Case cursor
                    facing = 'c';
                    break;

                case '2': //Case arrows
                    facing = 'a';
                    break;

                case '3': //Case movement
                    facing = 'm';
                    break;
            } //facing switch
            Console.Clear();
            return facing;
        } //Get Player Movement Type function

        public void DisplayControls(char facingType)
        {
            Console.WriteLine("Controls:");
            Console.WriteLine("Move Up:    W");
            Console.WriteLine("Move Down:  S");
            Console.WriteLine("Move Left:  A");
            Console.WriteLine("Move Right: D");
            Console.WriteLine("");
            Console.WriteLine("Shoot Fireball: Space");
            Console.WriteLine("Shoot Bush:     1");
            if (facingType == 'a')
            {
                Console.WriteLine("");
                Console.WriteLine("Turn Left:  Left Arrow");
                Console.WriteLine("Turn Right: Right Arrow");
            }
            Console.ReadKey();
            Console.Clear();
        } //Display Controls function

        public int GetZombieNumber(int zombieNumber)
        {
            string zombieNumberCandidate;
            char selection;

            do
            {
                Console.WriteLine("Would you like a specific number of Zombies, or would you like a random number from 1 to 10?");
                Console.WriteLine("1: Specific number\n2: Random");
                selection = Console.ReadKey().KeyChar;
                Console.Clear();
            }
            while (selection != '1' && selection != '2');

            if(selection == '1')
            { //If specific number
                do
                {
                    Console.WriteLine("How many Zombies do you want?");
                    Console.WriteLine("[0 to 10 recommended]");
                    Console.WriteLine("");
                    Console.Write("> ");
                    zombieNumberCandidate = Console.ReadLine();
                    Int32.TryParse(zombieNumberCandidate, out zombieNumber);
                    Console.Clear();
                }
                while (!Int32.TryParse(zombieNumberCandidate, out zombieNumber));
            } //If specific number
            else
                zombieNumber = new Random().Next(1, 10);

            return zombieNumber;
        } //Zombie Number
    } //Game
} //Maths For Games Assessment