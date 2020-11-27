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

            Raylib.InitWindow(1024, 760, "Maths For Games Assessment");
            Raylib.SetTargetFPS(30);

            Console.CursorVisible = false;
            Console.Title = "Maths For Games Assessment";

            Scene scene1 = new Scene();
            Scene scene2 = new Scene();
            Scene scene3 = new Scene();

            Player player = new Player(1, 1);

            Tile tile01 = new Tile(1, 1, new Sprite("Images/PNG/Tiles/tile_07.png"));
            Tile tile02 = new Tile(2, 1, new Sprite("Images/PNG/Tiles/tile_09.png"));
            Tile tile03 = new Tile(3, 1, new Sprite("Images/PNG/Tiles/tile_09.png"));


            int startingSceneIndex = AddScene(scene1);
            AddScene(scene2);
            AddScene(scene3);

            scene1.AddActor(player);

            scene1.AddActor(tile01);
            scene1.AddActor(tile02);
            scene1.AddActor(tile03);

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
    } //Game
} //Maths For Games Assessment