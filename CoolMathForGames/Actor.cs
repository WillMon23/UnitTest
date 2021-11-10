using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {
        private string _name;
        private bool _started;
        private Vector3 _froward = new Vector3(0,0, 1);
        private Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent = null;

        private Shape _shape;
        private Color _color = Color.WHITE;

        public Color ShapeColor
        {
            get { return _color; }
        }

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started { get { return _started; } }

        public string Name { get { return _name; } }

        public Vector3 Forward { 
            get 
            {
                return new Vector3(_rotation.M00, _rotation.M10, _rotation.M20);
            } 
                                 
            set {
                                        
                Vector3 point = value.Normalized + LocalPosition;
                                       
                LookAt(point);
                                     
            } 
        }

        public Vector3 LocalPosition { get { return new Vector3(_localTransform.M03, _localTransform.M13, _localTransform.M23); } 
                                       set { SetTranslation(value.X, value.Y, value.Z); } }
        public Vector3 WorldPosition

        {
            get
            {
                return new Vector3(_globalTransform.M03, _globalTransform.M12,_globalTransform.M23);
            }

            set
            {
                if (Parent != null)
                {
                    float xOffset = (value.X - Parent.WorldPosition.X) / new Vector3(_globalTransform.M00, _globalTransform.M10, _globalTransform.M30).Magnitude;
                    float yOffset = (value.Y - Parent.WorldPosition.Y) / new Vector3(_globalTransform.M01, _globalTransform.M11, _globalTransform.M21).Magnitude;
                    float zOffset = (value.Z - Parent.WorldPosition.Z) / new Vector3(_globalTransform.M02, _globalTransform.M12, _globalTransform.M22).Magnitude;
                    SetTranslation(xOffset, yOffset, zOffset);
                }

                else
                    LocalPosition = value;
            }
        }

        public Matrix4 GlobalTransform { get { return _globalTransform; } private set { _globalTransform = value; } }

        public Matrix4 LocalTransform { get { return _localTransform; } private set { _localTransform = value; } }

        public Actor Parent { get { return _parent; } set { _parent = value; } }

        public Actor[] Children { get { return _children; } set { _children = value; } }

        public Vector3 Size { 
            get 
            {
                float xScale = new Vector3(_scale.M00, _scale.M10,_scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11,_scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;
                return new Vector3(xScale, yScale, zScale); 
            } 
            set 
            { 
                SetScale(value.X, value.Y, value.Z); 
            } 
        }

        public Collider Collider { get { return _collider; } set { _collider = value; } }

        public Actor(Vector3 position, string name = "Actor", Shape shape = Shape.CUBE)
        {
            _name = name;
            LocalPosition = position;
            _shape = shape;
 

        }


        public Actor(float x, float y, string name = "Actor", Shape shape = Shape.CUBE) :
            this(new Vector3 { X = x, Y = y }, name, shape)
        { }

        /// <summary>
        /// Updates Childs Transform In conjuction To the Parents 
        /// Orgin
        /// </summary>
        public void UpdateTransform()
        {
            // Updates this actors local transform 
            LocalTransform = _translation * _rotation * _scale;

            if (Parent != null)
                _globalTransform = Parent.GlobalTransform * LocalTransform;
            else
                GlobalTransform = LocalTransform;
        }

        /// <summary>
        /// Adds Children to our Private Actor Array 
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Actor child)
        {
            Actor[] temp = new Actor[Children.Length + 1];

            for (int i = 0; i < Children.Length; i++)
                temp[i] = Children[i];
            temp[Children.Length] = child;

            Children = temp;

            // Sets the childs parents to be this actor 
            child.Parent = this;

        }

        /// <summary>
        /// Removes Character the Arrays 
        /// </summary>
        /// <param name="child"></param>
        public bool RemoveChild(Actor child)
        {
            bool removed = false;
            Actor[] temp = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] != child)
                {
                    temp[j] = _children[i];
                    j++;
                }
                else
                    removed = true;
            }
            // If child removelw as succesfull. . .
            if (removed)
            {
                // Child arrray equals to the temporray array
                _children = temp;
                // Child parent is also removed
                child.Parent = null;
            }

            return removed;
            
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            
            Console.WriteLine(_name + " Position: X = " + GlobalTransform.M02 + " Y = " + GlobalTransform.M12);
        }

        /// <summary>
        /// Draws Out To The Consoole
        /// </summary>
        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);

            System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosition.X + Forward.X * 50, WorldPosition.X + Forward.Y * 50, WorldPosition.X + Forward.Z * 50);

            switch (_shape)
            {
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.X, Size.Y, Size.Z, ShapeColor);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.X, ShapeColor);
                    break;


            }
            Raylib.DrawLine3D(position, endPos, Color.RED);
            
        }

        public virtual void End()
        {

        }

        /// <summary>
        /// Checks if this actor collides with another 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool CheckForCollision(Actor other)
        {
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="transkationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void SetTranslation(float transkationX, float translationY, float translationZ)
        {
            _translation = Matrix4.CreateTranslation(transkationX,translationY, translationZ);
        }

        public void Translate(float translationX, float translationY, float translationZ)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        public void SetRoation(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotatuinZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation = rotationX * rotationY * rotatuinZ;
        }
        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotatuinZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation = rotationX * rotationY * rotatuinZ;
        } 

        public void SetColor(Color color)
        {
            _color = color;
        }

        public void SetColor( Vector4 colorValue)
        {
            _color = new Color((int)colorValue.X, (int)colorValue.Z, (int)colorValue.Z, (int)colorValue.W);
        }

        public virtual void OnCollision( Actor actor)
        {
            Engine.CloseApplication();
        }

        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(new Vector3(x, y, z));

        }

        /// <summary>
        /// Scales the actor by the 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Scale(float x,float y, float z)
        {
            _scale *= Matrix4.CreateScale(new Vector3(x, y, z));
        }
        
        /// <summary>
        /// Rotates the actor at any given postion
        /// </summary>
        /// <param name="position"></param>
        public void LookAt(Vector3 position)
        {
            //The direction to look at
            Vector3 direction = (position - WorldPosition).Normalized;

            // If the direction has an length of Xero. . .
            if (direction.Magnitude == 0)
                //. . . set it to be the defult forward
                direction = new Vector3(0, 0, 1);
            //Creats a vector the points directly uppwards
            Vector3 alignAxis = new Vector3(0, 1, 0);

            //Creats two new vector thst will be the new x and y axis
            Vector3 newYAxis = new Vector3(0, 1, 0);
            Vector3 newXAxis = new Vector3(1, 0, 0);

            //if the direction vector is parellel to the aligned vector . . .
            if (Math.Abs(direction.Y) > 0 && direction.X == 0 && direction.Z == 0)
            {
                //. . . Set the alingnAxis vector to rhe point to the right
                alignAxis = new Vector3(1, 0, 0);

                ///get the cross product if the direction and the right to find the Y axis
                newYAxis = Vector3.CrossProduct(direction, alignAxis);
                //Normalizethe new Y axis to provent the matrix from being scaled 
                newYAxis.Normalize();

                //gettje cross product of the new Y axis and the direction to find the new X axis
                newXAxis = Vector3.CrossProduct(newYAxis, direction);
                //Normalize the X asix to prenent the matrix from  being scaled
                newXAxis.Normalize();
            }
            else
            {
                //Get the cross product of the alignedAxis and the direction vector
                newXAxis = Vector3.CrossProduct(alignAxis, direction);
                //Normalize the newAxis to prevent our matrix form being scaled 
                newXAxis.Normalize();

                //Get the cross product of the new X axis and the diretion 
                newYAxis = Vector3.CrossProduct(direction, newXAxis);
                //Normalize the newYAxis to prevent the matrix from being scaled 
                newYAxis.Normalize();

            }

            // Sets the rotation based of the codotion set
            _rotation = new Matrix4(newXAxis.X, newYAxis.X, direction.X, 0,
                                    newXAxis.Y, newYAxis.Y, direction.Y, 0,
                                    newXAxis.Z, newYAxis.Z, direction.Z, 0,
                                    0, 0, 0, 1);
            
        }

    }
}
