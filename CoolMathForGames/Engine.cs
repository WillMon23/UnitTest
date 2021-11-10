using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;
using System.Diagnostics;


namespace CoolMathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];

        Stopwatch _stopwatch = new Stopwatch();

        Player _player;

        private Camera3D _camera = new Camera3D()
;
        /// <summary>
        /// Called to begin the application 
        /// </summary>
        public void Run()
        {
            // Call start for the entire application 
            Start();
            float currentTme = 0;
            float lastTime = 0;
            float deltTime = 0;
            // Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started 
                currentTme = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the diffrence in time from the last time recorded to the current time
                deltTime = currentTme - lastTime;

                //Update Application
                Update(deltTime);
                //Draw The Update
                Draw();

                //Set the last recorded to be the current time
                lastTime = currentTme;

            }
            // Called end for the entire application
            End();
        }

        /// <summary>
        /// Defult setting for the camera 
        /// </summary>
        private void InitlializeCamera()
        {
            _camera.position = new System.Numerics.Vector3(0, 10, 10); // Camera position
            _camera.target = new System.Numerics.Vector3(0, 0, 0); // Point the camera is focused on
            _camera.up = new System.Numerics.Vector3(0, 1, 0); // Camera up vector (rotation towards target)
            _camera.fovy = 100; // Camera field of Y
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; // Camera Mode Type 

        }

        /// <summary>
        /// Called when application starts 
        /// </summary>
        private void Start()
        {
            //Creats a window  using raylibaaa
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(0);

            InitlializeCamera();

            _stopwatch.Start();

            //Initulises the characters 
            Scene scene = new Scene();

            //Lead Protaganise 
            _player = new Player(0, 0, 50, "Player", Shape.SPHERE);
            _player.SetScale(1, 1, 1);
            _player.SetColor(new Vector4(100, 100, 10, 225));

            CircleCollider playerCollider = new CircleCollider(1, _player);
            AABBCollider playerBoxCollider = new AABBCollider(1, 1, _player);
            _player.Collider = playerCollider;

            ////Creats thr actors starting position
            //Actor actor = new Actor(200, 300, "Actor1", "Images/bullet.png");
            //actor.SetScale(50, 50);
            //CircleCollider actorCollider = new CircleCollider(20, actor);
            //actor.Collider = actorCollider;

            ////Creats thr actors starting position
            //Actor actor2 = new Actor(1,1, "Actor2", "Images/bullet.png");
            //actor.SetScale(1, 1);
            //AABBCollider actorBoxCollider = new AABBCollider(50, 50, actor2);
            //actor2.Collider = actorCollider;

            //Antaganise 
            Enemy enemy = new Enemy(20, 20, 5, _player, "Enemy", Shape.CUBE);
            enemy.SetScale(1, 1, 1);

            //CircleCollider enemyCollider = new CircleCollider(20, enemy);
            //AABBCollider enemyBoxCollider2 = new AABBCollider(50, 50, enemy);
            //enemy.Collider = enemyCollider;

            //player.AddChild(actor2);

            scene.AddActor(_player);
            //scene.AddActor(actor);
            //scene.AddActor(actor2);
            scene.AddActor(enemy);



            _currentSceneIndex = AddScene(scene);
        }

        /// <summary>
        /// Called to draw to the scene 
        /// </summary>
        private void Draw()
        {
            Console.CursorVisible = false;

            // Resets the cursor position to the top
            Console.SetCursorPosition(0, 0);
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawGrid(100, 1);

            //CameraControls();

            //Adds all actor icon to buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
            Raylib.EndMode3D();
        }

        /// <summary>
        /// Updates the application and notifies the console of any changes 
        /// </summary>
        private void Update(float deltTime)
        {
            _scenes[_currentSceneIndex].Update(deltTime);

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called once the game has been set to game over 
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Created to append new scnene to the current listing of scene 
        /// </summary>
        /// <param name="scene">Scene being added to the current list of scens</param>
        /// <returns>returns the new ammount of scenes</returns>
        public int AddScene(Scene scene)
        {
            // Creats a Temporary array 
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copys all the values from old array info to the temp array
            for (int i = 0; i < _scenes.Length; i++)
                tempArray[i] = _scenes[i];

            //Sets adds the new scene to the new size
            tempArray[_scenes.Length] = scene;

            // Set the old array to the new array
            _scenes = tempArray;

            // returns the new allocated size
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Get the nexy key in the input stream
        /// </summary>
        /// <returns>The key thst waspressed </returns>
        public static ConsoleKey GetNextKey()
        {
            //if there is no key being pressed. . . 
            if (!Console.KeyAvailable)

                //. . . return
                return 0;

            //Return the current key being pressed 
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// when called will end the game
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }

        /// <summary>
        /// Sets 
        /// </summary>
        private void CameraControls()
        {
            _camera.target.X = _player.GlobalTransform.M03;

            _camera.target.Y = _player.GlobalTransform.M13;

            _camera.target.Z = _player.GlobalTransform.M23;

            _camera.up.X = _player.GlobalTransform.M03;

            _camera.up.Y = _player.GlobalTransform.M13;

            _camera.up.Z = _player.GlobalTransform.M23 - 10f;

            _camera.position.X = _player.GlobalTransform.M03;

            _camera.position.Y = _player.GlobalTransform.M13 - 20f;

            _camera.position.Z = _player.GlobalTransform.M23;
        }
    }
}

