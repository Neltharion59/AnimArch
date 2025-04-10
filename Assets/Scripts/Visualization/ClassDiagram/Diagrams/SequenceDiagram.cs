using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OALProgramControl;
using TMPro;
using UMSAGL.Scripts;
using UnityEngine;
using Visualization;
using Visualization.Animation;
using Visualization.ClassDiagram;
using Visualization.ClassDiagram.Diagrams;
using OALProgramControl;
using Assets.Scripts.AnimationControl;
using AnimArch.Encryption;


namespace AnimArch.Visualization.Diagrams
{
    public class SequenceDiagram : Diagram
    {
        private VisitorCommandToPlantUML visitor;
        public string FileNameOfPlantUMLText;

        private void Awake()
        {
            DiagramPool.Instance.SequenceDiagram = this;
            ResetDiagram();
            visitor = new VisitorCommandToPlantUML();

        }

        public void ResetDiagram()
        {
            if (graph != null)
            {
                Destroy(graph.gameObject);
                graph = null;
            }
        }

        public void LoadDiagram()
        {
            CreateGraph();
            Generate();
            ManualLayout();
        }

        private Graph CreateGraph()
        {
            var go = Instantiate(DiagramPool.Instance.graphPrefab);
            graph = go.GetComponent<Graph>();
            graph.nodePrefab = DiagramPool.Instance.sequenceEntityPrefab;
            return graph;
        }

        public void StartPlantUMLCreation(string initialClassName)
        {
            visitor.classNames.Push(initialClassName);    
            visitor.AppendToCommandString("@startuml"); 
        }

        public void ToPlantUMLCommand(EXECommand CurrentCommand)
        {
            CurrentCommand.Accept(visitor);
        }

        public void CreatePlantUMLFile()
        {
            visitor.AppendToCommandString("@enduml");

            string path = Application.dataPath + "/PlantUMLs/" + FileNameOfPlantUMLText + ".txt";
            string content = visitor.GetCommandString().ToString();

            File.WriteAllText(path, content);
        }

        public void CreateHash(string name)
        {
            FileNameOfPlantUMLText = HashService.GenerateSHA256(name);
        }

        public void Generate()
        {
            //  graph.nodePrefab = messageInDiagram.Arrow;
            // node = graph.AddNode();
            // messageInDiagram.Arrow = node;
            // var messageText = node.transform.Find("Message");
            // messageText.GetComponent<TextMeshProUGUI>().text = messageInDiagram.MessageText;
        }


        public void ManualLayout()
        {
        }
    }
}