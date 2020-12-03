## Classes
### Game.cs:

    static Scene GetScene: pass in an int (index)
        Gets the Scene at the passed in index if there is a Scene at that index in the _scenes array
        If there is no Scene there, returns a new Scene

    static Scene GetCurrentScene:
        Returns the scene at the passed in index

    static Scene GetScenes: pass in an int (index)
        Gets and returns the Scene at the passed in index

    static int AddScene: pass in a Scene (scene)
        If the passed in Scene is null, returns
        Creates a temporary array tempArray of Scenes that's the length of _scenes + 1, and enters a for loop for every position of _scenes
            This loop sets tempArray[i] to _scenes[i]
        Sets index to _scenes.Length, then sets the passed in scene to tempArray[index]
        Sets _scenes to tempArray, then returns _scenes

    static bool RemoveScene: pass in a Scene (scene)
        If the passed in Scene is null, returns false
        Sets new bool sceneRemoved to false
        Creates new array tempArray, which is _scenes.Length - 1
        Creates a new int j, then enters a for loop for every position of _scenes
          If tempArray[i] isn't the passed in scene, then set tempArray[j] to _scenes[i] and increment j
          Otherwise, set sceneRemoved to true
        If sceneRemoved is true, then set _scenes to tempArray, then return tempArray

     static void SetCurrentScene: pass in an int (index)
        If passed in index is less than zero, or is greater than or equal to _scenes.Length, then returns
        If the Scene at _currentSceneIndex has Started, then calls End for that Scene
        Sets _currentSceneIndex to passed in index

    static bool GetKeyDown: pass in an int (key)
        returns Raylib.IsKeyDown((KeyboardKey)key)

    static bool GetKeyPressed: pass in an int (key)
        returns Raylib.IsKeyPressed(KeyboardKey)key)

    void Start
        Console.Title is set to "Maths For Games Assessment"

        A new facing char is set to a GetPlayerMovementType call, the Console is cleared, then DisplayControls is called, passing in facing
        a new zombieNumber int is initialized then set to a GetZombieNumber call, passing in the zombieNumber

        A Raylib Window is created

        Scenes scene1, scene2, and scene3 are created and initialized
        Player player is created and initialized, passing in coordniates 1, 1, and the facing variable
        2DArray Tile tiles is created and initialized, then enters a for loop for every position before _worldWidth
          Enters a second for loop for every position before _worldHeight
            tiles[i, j] is sent back through the constructor, and given proper locations and Sprites, and AddActor is called
        A new int for startingSceneIndex is set to a call for AddScene, passing in scene1, and two more calls for AddScene are made for scene2 and scene3
        A new Zombie array zombies is put through a constructor, passing in zombieNumber, then a for loop is entered
          A random int r is created, and zombies[i] is passed through a constructor, passing in two random calls for 0 and the ends of the screen, and player, for a target
          An AddActor call is made for zombies[i]
        A call for SetCurrentScene is made, passing in startingSceneIndex
    
    void Update: pass in deltaTime
        If Started property is false then call Start for _scenes[_currentSceneIndex]
        Calls GameManager.CheckWin, then calls Update for _scenes[_currentSceneIndex]

    void Draw:
        Calls Raylib's BeginDrawing, Raylib's ClearBackground, _scenes[_currentSceneIndex].Draw, then Raylib's EndDrawing

    void End:
        Empty, I would like to fill this out sometime in the future, but that time is not now

    void Run:
        Calls Start, then enters the main Game loop (while GameManager.GameOver is false, and while Raylib.WindowShouldClose is false)
          Float deltaTime is set, then Update is called, passing deltaTime in, then calls Draw
          When Console.KeyAvailable is true, calls Console.ReadKey to take any additional key presses
        Calls End

    Sprite RandomStoneSprite:
        Sprite sprite is created, as well as int randInt, which is set to a new Random between 7 and 10
          A switch statement is entered for randInt, which passes sprite through different constructors that assign a different Sprite each
        Returns sprite

    Char GetPlayerMovementType:
        Asks the user what controls they'd like to use to turn the Players orientation using a do while loop for invalid input
        Enters a switch based off of the input, which sets the value of facing to something readable to the function, then clears the console and returns facing

    DisplayControls: pass in a char (facingType)
        Prints out the controls used to make the Player maneuver the game, and if the Player turns when the arrow keys are pressed, then shows that arrows are used to turn the Player
        Uses a Console.ReadKey to show the previous text, then calls Console.Clear

    GetSpawnLocation: pass in a Vector2 (positionVar
        Sets the X and Y of positionVar to a number between 0 and Raylib.GetScreenWidth/Height/32, then returns positionVar

    GetZombieNumber: pass in an int (zombieNumber)
        Creates string zombieNumberCandidate and char selection
        Asks the user whether they'd like a specific number of zombies or if they'd like the game to choose a number between one and ten using a do while input is invalid, then calls Console.Clear
          If the user wants a specific number, asks what the number is (And shows a suggested amount) using a do while for invalid input
          If the user wants a random number, sets zombieNumber to a random number between 1 and 10
        returns zombieNumber

### Scene:

    void AddActor: pass in an Actor (actor)
        If the passed in Actor is null, returns
        Creates a temporary array tempArray of Actors that's the length of _actors + 1, and enters a for loop for every position of _actors
            This loop sets tempArray[i] to _actors[i]
        Sets index to _actors.Length, then sets the passed in actor to tempArray[index]
        Sets _actors to tempArray, then returns _actors

    bool RemoveActor: pass in an int (index)
        If the passed in index is less than 0, or index is greater than or equal to _actors.Length, or _actors.Length is 0, returns false
        Sets new bool actorRemoved to false
        Creates new array tempArray, which is _actors.Length - 1
        Creates a new int j, then enters a for loop for every position of _actors
          If i isn't the passed in index, then set tempArray[j] to _actors[i] and increment j
          Otherwise, set actorRemoved to true and if _actors[i].Started is true, then call _actors[i].End
        If actorRemoved is true, then set _actors to tempArray, then return tempArray

    bool RemoveActor: pass in an Actor (actor)
        If the passed in Actor is null, returns false
        Sets new bool actorRemoved to false
        Creates new array tempArray, which is _actors.Length - 1
        Creates a new int j, then enters a for loop for every position of _actors
          If tempArray[i] isn't the passed in actor, then set tempArray[j] to _actors[i] and increment j
          Otherwise, set actorRemoved to true and if _actors[i].Started is true, then call _actors[i].End
        If actorRemoved is true, then set _actors to tempArray, then return tempArray

    void Start
        Sets Started to true

    void Update: pass in float (deltaTime)
        enters a loop for every Actor
          if _actor[i].Started is false, calls Start for that Actor
          Calls _actor[i].Update
        Calls CheckCollisions

    void Draw
        Enters a for loop for every Actor
          if _actor[i] is a Tile, then calls that Actor's Draw
        Enters a for loop for every Actor
          if _actor[i] is not a Tile, then calls that Actor's Draw

    void End
        Enters a for loop for every Actor
          if _actor[i].Started is true, calls End for that Actor

    void CheckCollisions
        Enters a for loop for every Actor
          Enters a for loop for every Actor
            Calls _actors[i].CheckCollision(_actors[j])

### Actor:

    void Addchild: pass in an Actor (child)
        