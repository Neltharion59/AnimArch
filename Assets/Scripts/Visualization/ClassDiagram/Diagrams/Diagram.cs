using UnityEngine;

namespace Visualization.ClassDiagram.Diagrams
{
    public class Diagram : MonoBehaviour
    {
        public float offsetZ = 800;
        public GameObject CreateInterGraphLine(GameObject start, GameObject end)
        {
            GameObject Line = Instantiate(DiagramPool.Instance.interGraphLinePrefab);

            Line.GetComponent<LineRenderer>().SetPositions
            (
                new []
                {
                    start.GetComponent<RectTransform>().position,
                    end.GetComponent<RectTransform>().position
                }
            );

            Line.GetComponent<LineRenderer>().widthMultiplier = 4f;
            //Line.transform.SetParent(graph.units);

            return Line;
        }
    }
}
