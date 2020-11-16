﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maths_For_Games_Assessment
{
    class Game
    {
        private static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        public static int CurrentSceneIndex
        { get { return _currentSceneIndex; } }

        public static bool SetGameOver
        { set { _gameOver = value; } }

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
        } //Start

        public void Update()
        {

        } //Update

        public void Draw()
        {

        } //Draw

        public void End()
        {

        } //End

        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
                Draw();
            }

            End();
        } //Run
    } //Game
} //Maths For Games Assessment
