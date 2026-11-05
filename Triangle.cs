using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Basic3DExercise;

///A class for rendering a single triangle
public class Triangle
{
    ///The vertices of the triangle
    VertexPositionColor[] vertices;

    //The effect to use rendering the triangle
    BasicEffect effect;

    //The game this triangle belongs to
    Game game;

    //Constructs a triangle instance
    //game parm: the game that is creating the triangle
    public Triangle(Game game)
    {
        this.game = game;
        InitializeVertices();
        InitializeEffect();
    }

    /// <summary>
    /// Rotates the triangle around the y-axis
    /// </summary>
    /// <param name="gameTime">The GameTime object</param>
    public void Update(GameTime gameTime)
    {
        float angle = (float)gameTime.TotalGameTime.TotalSeconds;
        effect.World = Matrix.CreateRotationY(angle);
    }

    //draws the triangle
    public void Draw()
    {
        //cache old rasterizer state
        RasterizerState oldState = game.GraphicsDevice.RasterizerState;
        
        //disable backface culling
        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.CullMode = CullMode.None;
        game.GraphicsDevice.RasterizerState = rasterizerState;
        
        //apply our effect
        effect.CurrentTechnique.Passes[0].Apply();
        //draw triangle
        game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
            PrimitiveType.TriangleList,
            vertices,   //verex data
            0,          //first vertex to use
            1           //number of triangles to draw
        );

        // restor the prior rasterizer state
        game.GraphicsDevice.RasterizerState = oldState;
    }

    ///Initializes the vertices of the triangle
    void InitializeVertices()
    {
        vertices = new VertexPositionColor[3];
        //vertex 0
        vertices[0].Position = new Vector3(0,1,0);
        vertices[0].Color = Color.Red;
        //vertex 1
        vertices[1].Position = new Vector3(1,1,0);
        vertices[1].Color = Color.Green;
        //vertex 2
        vertices[2].Position = new Vector3(1,0,0);
        vertices[2].Color = Color.Blue;
    }

    void InitializeEffect()
    {
        effect = new BasicEffect(game.GraphicsDevice);
        effect.World = Matrix.Identity;
        effect.View = Matrix.CreateLookAt(
            new Vector3(0,0,4), //The camera position
            new Vector3(0,0,0), //The camera target
            Vector3.Up          //The camera up vector
        );
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,    //The field-of-view
            game.GraphicsDevice.Viewport.AspectRatio, //the aspect ratio
            0.1f, //The near plane distance
            100.0f //the far plane distance
            );
            effect.VertexColorEnabled = true;
    }


}