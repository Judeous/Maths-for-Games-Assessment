# MathsForGamesAssessment
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
        Sets index to _scenes.Length, then sets tempArray[index] to the passed in scene
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
        Returns Raylib.IsKeyDown((KeyboardKey)key)

    static bool GetKeyPressed: pass in an int (key)
        Returns Raylib.IsKeyPressed(KeyboardKey)key)

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
        Returns zombieNumber

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
        If the passed in child is null, returns
        Creates a temporary array tempArray of Actors that's the length of _children + 1, and enters a for loop for every position of _children
            This loop sets tempArray[i] to _children[i]
        Sets tempArray[_children.Length] to the passed in child, sets _children to tempArray, then sets child._parent to this

    bool RemoveChild: pass in an Actor (child)
        If the passed in child is null, returns false
        Creates a temporary array tempArray of Actors that's the length of _children, creates bool childRemoved, (initialized false) then enters a for loop for every position of _children
          If the passed in child isn't _children[i], then sets newArray[j] to _children[i] and increments j
          If it is, then sets childRemoved to true
        Sets _children to tempArray, sets child._parent to null, then returns childRemoved

    void AddSprite: pass in a Sprite (sprite)
        Creates a new array of Sprites called appendedArray, setting it to _sprites.Length + 1, then enters a for loop for every position in _sprites
          sets appendedArray[i] to _sprites[i]
        Sets the last position in appendedArray to the passed in Sprite, then sets _sprites to appendedArray

    bool RemoveSprite: pass in an int (index)
        If the passed in index is less than 0, or index is greater than or equal to _sprites.Length, returns false
        Sets new bool spriteRemoved to false
        Creates new array tempArray, which is _sprites.Length - 1
        Creates a new int j, then enters a for loop for every position of _sprites
          If i isn't the passed in index, then set tempArray[j] to _sprites[i] and increment j
          Otherwise, set spriteRemoved to true
        If actorRemoved is true, then set _actors to tempArray, then return tempArray

    
    bool RemoveSprite: pass in a Sprite (sprite)
        If the passed in Sprite is null, returns false
        Sets new bool spriteRemoved to false
        Creates new array newArray, which is _sprites.Length - 1
        Creates a new int j, then enters a for loop for every position of _sprites
          If newArray[i] isn't the passed in sprite, then set newArray[j] to _sprites[i] and increment j
          Otherwise, set spriteRemoved to true
        If spriteRemoved is true, then set _sprites to newArray, then return spriteRemoved

    void Start:
        Sets Started to true

    void Update: pass in a float (deltaTime)
        If _health is equal to or less than 0, get a reference to the current Scene to call RemoveActor, then call End and return
        Otherwise, call UpdateFacing, UpdateLocalTransform, then UpdateGlobalTransform
        If Velocity.Magnitude is not 0, then divide Velocity.X and Velocity.Y by 5
        Add Acceleration to Velocity, then if Velocity.Magnitude is greater than MaxSpeed, set Velocity to it's Magnitude multiplied by MaxSpeed
        LocalPosition has Velocity multiplied by deltaTime added to it

    void Draw:
        If _currentSprite isn't null, then call it's Draw function, passing in GlobalTransform

    void End:
        Set Started to false

    bool CheckCollision: pass in an Actor (actor)
        If the sum of both this Actor as well as the passed in Actor's _collRadius is greater than the sum of the distance between the two Actors, then calls _OnCollision for both Actors, then returns true
        Otherwise, returns false

    void OnCollision: pass in an Actor (actor)
        If actor isn't a Tile nor a Projectile, then creates a Vector2 (direction) that is a line from actor to this, then calls actor.SetTranslate, passing in actor.LocalPosition + direction.Normalized

    void SetTranslate: pass in a Vector2 (position)
        Sets _translation to a call for Matrix3.CreateTranslation, passing in the passed in position

    void SetRotation: pass in a float (radians)
        Multiplies _rotation to itself multiplied by a call for Matrix3.CreateRotation, passing in the passed in radians

    void SetScale: pass in two floats (x and y)
        Sets _scale to a call for Matrix3.CreateScale, passing in the passed in x and y

    void UpdateLocalTransform:
        sets _localTransform to _translation multiplied by _rotation multiplied by _scale

    void UpdateGlobalTransform
        If _parent isn't null, sets _globalTransform to _parent._globalTransform multiplied by _localTransform
        Otherwise, sets _globalTransform to Game.GetCurrentScene.World multiplied by _localTransform

    void UpdateFacing
        if Velocity.Magnitude isn't 0, then sets Forward to Velocity

    void LookAt: pass in a Vector2 (position)
        Create a Vector2 (direction) set to position subtracted from GlobalPosition, with the result .Normalized
        Create a float (angle) set to a call for Vector2.FindAngle, passing in Forward and direction, then call Rotate, passing in negative angle

    void DealDamage: pass in an Actor (actor)
        Calls actor.TakeDamage

    void TakeDamage: pass in a float (damage)
        Subtracts damage from _health

    void CreateProjectile: pass in a Projectile (projectile)
        Using a Game.GetScenes call, passing in Gane.CurrentSceneIndex, calls scene.AddActor, passing in the passed in projectile

### Tile:

    override void OnCollision: pass in an Actor
        Empty. This prevents the original code of OnCollision from running

### Player.cs:

    override void Start:
        A barrage of calls for AddSprite, using the Sprite Constructor, passing in the various Sprites that the character used for the Player has

    override void Update: pass in a float (deltaTime)
        Creates two ints (xDirection and yDirection) and sets them to negative calls for Convert.ToInt32, passing in calls for Game.GetKeyDown, passing in KeyBoardKeys (The keys used as input to make the Player move across the Scene)cast as ints, and adding this to the same thing, but positive and using the keys used to move in the opposite direction
        Acceleration is set to a new Vector2, calling the constructor (passing in xDirection and yDirection)
        Adds deltaTime to _timeSinceFire
        If _timeSinceFire is greater than _fireCoolDown, then sets _inCoolDown to false, and sets _currentSprite to _sprites[5]
        Otherwise, sets _inCooldown to true
        If a call for Game.GetKeyDown (passing in KeyboardKey.KEY_SPACE cast as an int) returns true (meaning that the spacebar is down), and _inCoolDown is false, then calls CreateProjectile, passing in 'f' (for fireball)
        If a call for Game.GetKeyDown (passing in KeyboardKey.KEY_ONE cast as an int) returns true (meaning that the one key is down), and _inCoolDown is false, then calls CreateProjectile, passing in 'b' (for bush) (Yes, the Player can create a bush projectile)
        Calls base.Update

    override void UpdateFacing
        Enters a switch block for _facingType
            Case 'c' (for cursor) calls LookAt, passing in a new Vector2, passing in Raylib.GetMousePosition.X divided by 32, and Raylib.GetMousePosition.Y divided by 32
            Case 'a' (for arrows) checks to see if the Left or Right arrow keys are down, using calls for Game.GetKeyDown, passing in KeyboardKey.KEY_LEFT and KeyboardKey.KEY_RIGHT. Left calls SetRotation, passing in _rotate with .2 added, Right does the same with -.2

    void DrawWinText
        Calls Raylib.DrawText, passing in "You Win!", 150, 150, 50, and Color.GRAY
        This is currently unused, but is kept as an example of how to draw text on the Raylib window

    void CreateProjectile: pass in a char (projectileType)
        Creates a Projectile (projectile) sets _currentSprite to _sprites[1], then enters a switch block for projectileType
          case 'f' (for fireball) sends projectile through the constructor, passing inGlobalPosition + Forward, Forward, _damage, and _fireBallSprite
          case 'b' (for bush) sends projectile through the constructor, passing inGlobalPosition + Forward, Forward, _damage, and _bushSprite, then calls AddChild, passing in projectile
          default sends projectile through the constructor, passing inGlobalPosition + Forward, Forward, _damage, and _fireBallSprite
        CreateProjectile is called, passing in projectile, and _timeSinceFire is set to 0

### Zombie:

    void Start:
        GameManager.enemyCount is incremented, and base.Start is called

    void Update: pass in float (deltaTime)
        _timeSinceRotation has deltaTime added to it
        If a call for CheckTargetInSight (passing in hardcoded 0.5f and 10) returns true, then Acceleration is set to itself plus Forward, the result being Normalized, then _currentSprite is set to _sprites[1]
        Otherwise, GenerateMovement is called, and _currentSprite is set to _sprites[0]
        base.Update is called

    void End:
        GameManager.enemyCount is decremented, and base.End is called

    override void OnCollision: pass in an Actor (actor)
        If actor is a Player, then actor.TakeDamage is called, passing in _damage
        base.OnCollision is called either way

    bool CheckTargetInSight: pass in two floats (maxAngle and maxRange)
        if Target is null, return false
        Creates a Vector2 (direction) which is set to GlobalPosition subtracted from Target.GlobalPosition
        Creates a float (distance) which is set to direction.Magnitude
        Creates another float which is set to a call for Math.Acos (Cast as a float) passing in a call for Vector2.DotProduct, passing in Forward and direction.Normalized
        If angle is less than or equal to maxAngle, and distance is less than or equal to maxRange, then return true
        Otherwise, return false

    void GenerateMovement
        Creates two ints (randomX and randomY) and sets both to 0
        Creates a new int (forwardsOrNot) and passes it through a Random.Next call, the minimum being 1, the max being 6, then enters a switch for forwardsOrNot
          Case 1 sets Velocity to Forward
          Cases 2 and 3 send randomX and randomY through Random.Next calls, with minimums of 1 and maximums of 5
          Default case checks to see if _timeSinceRotation is greater than 10, and if it is, then GenerateRotation is called
        Convert is called, passing in randomX and randomY

    void Convert: pass in two ints (randomX and randomY)
        Enters a switch block for randomX
          Case 1 sets Acceleration.X to -1
          Cases 2 and 3 set Acceleration.X to 0
          Cases 4 and 5 set Acceleration.X to 1
        Enters a switch block for randomY
          Case 1 sets Acceleration.Y to -1
          Cases 2 and 3 set Acceleration.Y to 0
          Cases 4 and 5 set Acceleration.Y to 1

    void GenerateRotation
        Creates int (direction) and sets it to a Random.Next call, 1 being the minimum and 3 being the max, then enters a switch block for direction
          Case 1 calls SetRotation, passing in _rotate with .02 added to it
          Case 2 calls SetRotation, passing in _rotate with .02 subtracted from it
        Sets _timeSinceRotation to 0

### Projectile:

    override void Update: pass in float (deltaTime)
        Adds deltaTime to _timeSinceCreation, then checks to see if _timeSinceCreation is greater than or equal to _duration
        If so, then use a reference to the current Scene to call RemoveActor on itself, then returns
        If not, then Sets Velocity to Forward multiplied by _speed, then calls base.Update, passing in deltaTime

    override void OnCollision: pass in an Actor (actor)
        if actor isn't a Tile, then call actor.TakeDamage, passing in _damage, then call actor.SetTranslate, passing in actor.LocalPosition added to actor.GlobalPosition subtracted to GlobalPosition

# MathLibrary
### Vector2:
    Vector2 Normalize: pass in a Vector2 (vector)
        if vector.Magnitude is 0, then returns a new Vector2
        Otherwise, returns vector divided by it's own Magnitude

    float DotProduct: pass in two Vectors2 (lhs and rhs)
        Returns the sum of lhs.X multiplied by rhs.X added to lhs.Y multiplied by rhs.Y

    float FindAngle: pass in two Vector2s (lhs and rhs)
        Normalizes lhs and rhs, then creates float dorProd, which is set to a DotProduct call (with lhs and rhs passed in)
        If the Absoulte Value of dotProd is greater than 1, (somehow) then returns 0
        Creates a new float (angle) which is set to the cosine of dotProduct
        Creates a new Vector2 (perp) which is passed into the constructor (passing in rhs.Y and negative rhs.X)
        Creates a new float (perpDot) which is set to the DotProduct of perp and lhs
        If perpDot isn't 0, then angle is multiplied by perpDot divided by the absolute value of perpDot
        Returns angle

    operator overload + for two Vector2s (lhs and rhs)
        Returns a new Vector2, which is lhs.X + rhs.X and lhs.Y + rhs.Y

    operator overload - for two Vector2s (lhs and rhs)
        Returns a new Vector2, which is lhs.X - rhs.X and lhs.Y - rhs.Y

    operator overload * for a Vector2 and a float (lhs and scalar)
        Returns a new Vector2, which is lhs.X * scalar and lhs.Y * scalar

    operator overload / for a Vector2 and a float (lhs and scalar)
        Returns a new Vector2, which is lhs.X / scalar and lhs.Y / scalar

### Vector3:

    Vector3 Normalize: pass in a Vector3 (vector)
        if vector.Magnitude is 0, then returns a new Vector2
        Otherwise, returns vector divided by it's own Magnitude

    float DotProduct: pass in two Vectors3 (lhs and rhs)
        Returns the sum of lhs.X multiplied by rhs.X added to lhs.Y multiplied by rhs.Y added to lhs.Z multiplied by rhs.Z

    Vector3 CrossProduct: pass in two Vector3s (lhs and rhs)
        reutrns a new Vector3, which is lhs.Y multiplied by rhs.Z subtracted from lhs.Z multiplied by rhs.Y,
                                        lhs.Z multiplied by rhs.X subtracted from lhs.X multiplied by rhs.Z,
                                        lhs.X multiplied by rhs.Y subtracted from lhs.Y multiplied by rhs.X

    operator overload * for a Matrix3 (lhs) and a Vector3 (rhs)
        Returns a new Vector3, which is rhx.X multiplied by lhs.m11 added to rhs.Y multiplied by lhs.m12 added to rhs.Z multiplied by lhs.m13,
                                        rhx.X multiplied by lhs.m21 added to rhs.Y multiplied by lhs.m22 added to rhs.Z multiplied by lhs.m23,
                                        rhx.X multiplied by lhs.m31 added to rhs.Y multiplied by lhs.m32 added to rhs.Z multiplied by lhs.m33,

    operator overload + for two Vector3s (lhs and rhs)
        Returns a new Vector3, which is lhs.X + rhs.X and lhs.Y + rhs.Y and lhs.Z + rhs.Z

    operator overload - for two Vector3s (lhs and rhs)
        Returns a new Vector3, which is lhs.X - rhs.X and lhs.Y - rhs.Y and lhs.Z - rhs.Z

    operator overload * for a Vector3 and a float (lhs and scalar)
        Returns a new Vector3 which is lhs.X * scalar and lhs.Y * scalar and lhs.Z * scalar
        This has a copy for occasions when the parameters have swapped sides

    operator overload / for a Vector3 and a float (lhs and scalar)
        Returns a new Vector3, which is lhs.X / scalar and lhs.Y / scalar and lhs.Z / scalar

### Vector4:

    Vector4 Normalize: pass in a Vector4 (vector)
        if vector.Magnitude is 0, then returns a new Vector2
        Otherwise, returns vector divided by it's own Magnitude

    float DotProduct: pass in two Vectors4 (lhs and rhs)
        Returns the sum of lhs.X multiplied by rhs.X added to lhs.Y multiplied by rhs.Y added to lhs.Z multiplied by rhs.Z added to lhs.W multiplied by rhs.W

    Vector4 CrossProduct: pass in two Vector4s (lhs and rhs)
        Returns a new Vector4, which is lhs.Y multiplied by rhs.Z subtracted from lhs.Z multiplied by rhs.Y,
                                        lhs.Z multiplied by rhs.X subtracted from lhs.X multiplied by rhs.Z,
                                        lhs.X multiplied by rhs.Y subtracted from lhs.Y multiplied by rhs.X,
                                        and 0

    operator overload * for a Matrix4 (lhs) and a Vector4 (rhs)
        Returns a new Vector4, which is rhx.X multiplied by lhs.m11 added to rhs.Y multiplied by lhs.m12 added to rhs.Z multiplied by lhs.m13, added to rhs.W multiplied by lhs.m14
                                        rhx.X multiplied by lhs.m21 added to rhs.Y multiplied by lhs.m22 added to rhs.Z multiplied by lhs.m23, added to rhs.W multiplied by lhs.m24
                                        rhx.X multiplied by lhs.m31 added to rhs.Y multiplied by lhs.m32 added to rhs.Z multiplied by lhs.m33, added to rhs.W multiplied by lhs.m34
                                        rhx.X multiplied by lhs.m41 added to rhs.Y multiplied by lhs.m42 added to rhs.Z multiplied by lhs.m43, added to rhs.W multiplied by lhs.m44

    operator overload + for two Vector3s (lhs and rhs)
        Returns a new Vector3, which is lhs.X + rhs.X and lhs.Y + rhs.Y and lhs.Z + rhs.Z and lhs.W + rhs.W

    operator overload - for two Vector3s (lhs and rhs)
        Returns a new Vector3, which is lhs.X - rhs.X and lhs.Y - rhs.Y and lhs.Z - rhs.Z and lhs.W - rhs.W

    operator overload * for a Vector3 and a float (lhs and scalar)
        Returns a new Vector3 which is lhs.X * scalar and lhs.Y * scalar and lhs.Z * scalar and lhs.W * scalar
        This has a copy for occasions when the parameters have swapped sides

    operator overload / for a Vector3 and a float (lhs and scalar)
        Returns a new Vector3, which is lhs.X / scalar and lhs.Y / scalar and lhs.Z / scalar and lhs.W / scalar

### Matrix3:

    Matrix3 CreateRotation: pass in a float (radians)
        Returns a new Matrix3 which has the XX position set to the cosine of radians (Cast as a float), the XY position set to the inverse of sine (Cast as a float), the YX position set to the sine of radians (Cast as a float), and the YY position set to the cosine of radians (Cast as a float)

    Matrix3 CreateTranslation: pass in a Vector2 (position)
        Returns a new Matrix3 that has the WX position set to position.X, and the WY position set to position.Y

    Matrix3 CreateScale: pass in two floats (xScale and yScale)
        Returns a new Matrix3 which has the XX position set to xScale and the YY position set to yScale

    operator overload + for two Matrix3s (lhs and rhs)
        Returns a new Matrix3 which has all the positions of lhs added to the respective positions of rhs

    operator overload - for two Matrix3s (lhs and rhs)
        Returns a new Matrix3 which has all the positions of lhs subtracted from the respective positions of rhs

    operator overload * for two Matrix3s (lhs and rhs)
        Returns a new Matrix3 which has each row of lhs multiplied by each column of rhs

### Matrix4

    Matrix4 CreateRotationX: pass in a float (radians)
        Returns a new Matrix4 which has the XY position set to the cosine of radians (Cast as a float), the XZ position set to the inverse of the sine of radians (Cast as a float), the YY position set to the sine of radians (Cast as a float), and the YZ position set to the cosine of radians (Cast as a float)

    Matrix4 CreateRotationY: pass in a float (radians)
        Returns a new Matrix4 which has the XX position set to the cosine of radians (Cast as a float), the XZ position set to the sine of radians (Cast as a float), the YX position set to the inverse sine of radians (Cast as a float), and the YZ position set to the cosine of radians (Cast as a float)

    Matrix4 CreateRotationZ: pass in a float (radians)
        Returns a new Matrix4 which has the XX position set to the cosine of radians (Cast as a float), the XY position set to the inverse sine of radians (Cast as a float), the YX position set to the sine of radians (Cast as a float), and the YY position set to the cosine of radians (Cast as a float)

    Matrix4 CreateTranslation: pass in a Vector2 (position)
        Returns a new Matrix4 that has the WX position set to position.X, the WY position set to position.Y, and the WZ position set to position.Z

    Matrix4 CreateScale: pass in three floats (xScale, yScale, and zScale)
        Returns a new Matrix4 which has the XX position set to xScale, the YY position set to yScale, and the ZZ position set to zScale

    operator overload + for two Matrix4s (lhs and rhs)
        Returns a new Matrix4 which has all the positions of lhs added to the respective positions of rhs

    operator overload - for two Matrix4s (lhs and rhs)
        Returns a new Matrix4 which has all the positions of lhs subtracted from the respective positions of rhs

    operator overload * for two Matrix4s (lhs and rhs)
        Returns a new Matrix4 which has each row of lhs multiplied by each column of rhs