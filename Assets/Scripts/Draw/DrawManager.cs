using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace HomeVisit.Draw
{
    public class DrawManager : MaskableGraphic
    {
        static DrawManager self;

        DrawManager()
        {
            self = this;
        }

        static public void redraw()
        {
            print("redraw");
            self.SetAllDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = Color.red;

            // draw a diagonal red line from bottom left to top right

            // first triangle
            vertex.position = new Vector2(0, 0); // lower left position 
            vh.AddVert(vertex);
            vertex.position = new Vector2(canvas.pixelRect.width + 2, canvas.pixelRect.height);
            vh.AddVert(vertex);
            vertex.position = new Vector2(canvas.pixelRect.width - 2, canvas.pixelRect.height);
            vh.AddVert(vertex);

            // second triangle
            vertex.position = new Vector2(2, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector2(-2, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
            // position it on the upper right.
            vh.AddVert(vertex);

            // draw two elongated triangles to form a straight line
            vh.AddTriangle(0, 1, 2); // only triangles can be drawn
            vh.AddTriangle(3, 4, 5); // only triangles can be drawn
        }
    }
}


