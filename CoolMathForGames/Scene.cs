using System;
using System.Collections.Generic;
using System.Text;

namespace CoolMathForGames
{
    class Scene
    {
        /// <summary>
        /// Array thst contsind all the scenes 
        /// </summary>
        private Actor[] _actors;
        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls start for all the actors in the actors array
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                    _actors[i].Start();    
        }

        /// <summary>
        /// Calls all the actors in the scene 
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();
                    
                _actors[i].Update();

                //Checks for collision
                for( int j = 0; j < _actors.Length; j++)
                {
                    if (_actors[i].Posistion == _actors[j].Posistion && j != i)
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }

        /// <summary>
        /// Draws the 
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        /// <summary>
        /// Once update ends the 
        /// all the actors in the scene
        /// will end as well
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
        }

        /// <summary>
        /// Adds an actor to the scene list of actors 
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public virtual void AddActor(Actor actor)
        {
            // Creats a temp array larger than the originsl
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all the values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
                tempArray[i] = _actors[i];
            //Add the new actor to the end of the new array 
            tempArray[_actors.Length] = actor;
            //Set the old array to the new array 
            _actors = tempArray;
        } 

        /// <summary>
        /// Removes ana actor from the arary of actors 
        /// </summary>
        /// <param name="actor">The actor being removed</param>
        /// <returns>If actor was removed or not</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal of the actor happened 
            bool actorRemoved = false;
            //Creat a temp array smaller then the original
            Actor[] tempArray = new Actor[_actors.Length - 1];
            //Index of the new array 
            int j = 0;

            //Copy's all the actors from the old array to the new array that we don't want to remove 
            for(int i = 0; i < tempArray.Length; i++)
            {
                // If the actor does not equal to the actor we want 
                if (_actors[i] != actor)
                {
                    tempArray[j] = _actors[i];
                    j++;
                }
                //Other wise return true
                else
                    actorRemoved = true;
            }
            //If the actor was removed 
            if (actorRemoved)
                //Sets the actors to 
                _actors = tempArray;

            return actorRemoved;
        }
    }
}
