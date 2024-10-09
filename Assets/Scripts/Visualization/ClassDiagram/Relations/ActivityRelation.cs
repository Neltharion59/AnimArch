using System;
using UMSAGL.Scripts;
using UnityEngine;
using Visualization.ClassDiagram.ComponentsInDiagram;

namespace Visualization.ClassDiagram.Relations
{
    public class ActivityRelation
    {
        private readonly Graph _graph;        
        private readonly String _type;
        private readonly ActivityInDiagram _start;
        private readonly ActivityInDiagram _end;
        public GameObject GameObject;
        public ActivityRelation(Graph graph, ActivityInDiagram start, ActivityInDiagram end)
        {
            _graph = graph;
            _type = "ASSOCIATION";
            _start = start;
            _end = end;
        }

        public void Generate()
        {
            Debug.Log("[Karin] ActivityRelation::Generate()");
            GameObject = InitEdge();
            var uEdge = GameObject.GetComponent<UEdge>();
            uEdge.Points = new Vector2[]
            {
                _start.VisualObject.transform.position,
                _end.VisualObject.transform.position
            };
        }

        private GameObject InitEdge()
        {
            Debug.Log("[Karin] ActivityRelation::InitEdge()");
            return _graph.AddEdge(_start.VisualObject, _end.VisualObject,
                DiagramPool.Instance.associationNonePrefab);
        }

    }
}