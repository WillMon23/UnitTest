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
        /// Called when application starts 
        /// </summary>
        private void Start()
        {
            //Creats a window  using raylibaaa
            Raylib.InitWindow(800, 450, "Math For Games");

            Raylib.SetTargetFPS(0);

            _stopwatch.Start(); 

            //Initulises the characters 
            Scene scene = new Scene();

            //Lead Protaganise 
            Player player  = new Player( 400, 100, 500, "Player", "Images/player.png");
            player.SetScale(100, 100);
            player.SetTranslation(300, 300);

            CircleCollider playerCollider = new CircleCollider(20, player);
            AABBCollider playerBoxCollider = new AABBCollider(50, 50, player);
            player.Collider = playerBoxCollider;

            //Creats thr actors starting position
            Actor actor = new Actor(200, 300, "Actor1", "Images/bullet.png");
            actor.SetScale(50, 50);
            CircleCollider actorCollider = new CircleCollider(20, actor);
            actor.Collider = actorCollider;

            //Creats thr actors starting position
            Actor actor2 = new Actor(1,1, "Actor2", "Images/bullet.png");
            actor.SetScale(1, 1);
            AABBCollider actorBoxCollider = new AABBCollider(50, 50, actor2);
            actor2.Collider = actorCollider;

            //Antaganise 
            Enemy enemy = new Enemy(300,100, 250, player,"Enemy", "Images/enemy.png");
            enemy.SetScale(50, 50);
            enemy.Forward = new Vector2(700, 900);
            CircleCollider enemyCollider = new CircleCollider(20, enemy);
            AABBCollider enemyBoxCollider2 = new AABBCollider(50, 50, enemy);
            enemy.Collider = enemyCollider;

            player.AddChild(actor2);

            scene.AddActor(player);
            scene.AddActor(actor);
            scene.AddActor(actor2);
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
            Raylib.ClearBackground(Color.BLACK);
            //Adds all actor icon to buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
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
    }

}
