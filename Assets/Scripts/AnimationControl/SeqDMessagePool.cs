using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Visualization.Diagrams;
using Assets.Scripts.AnimationControl.OAL;
using OALProgramControl;
using TMPro;
using UMSAGL.Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Visualisation.Animation;
using Visualization.ClassDiagram;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.ClassDiagram.Diagrams;
using Visualization.ClassDiagram.Relations;
using Visualization.UI;
using AnimArch.Extensions;
using UnityEngine.AI;

namespace OALProgramControl
{
    public class SeqDMessagePool
    {
        public SequenceDiagram sequenceDiagram;
        public Graph graph;
        public List<MessageInDiagram> Messages { get; private set; }
        public SeqDMessagePool() 
        {
            Messages = new List<MessageInDiagram>();
        }

        public void addMessage(EntityInDiagram EntitySource, EntityInDiagram EntityDestination, string Message)
        {
            MessageInDiagram messageInDiagram = new MessageInDiagram
            {
                MessageText = Message,
                Message = DiagramPool.Instance.sequenceMessage,
                Arrow = DiagramPool.Instance.sequenceArrowMessage,
                ActivationBlockSource = DiagramPool.Instance.sequenceActivationBlock,
                ActivationBlockDestination = DiagramPool.Instance.sequenceActivationBlock,
                SourceEntity = EntitySource,
                DestinationEntity = EntityDestination
            };
            Messages.Add(messageInDiagram);

        }

        public void GenerateMessagesWithActivationBlocksAndArrows()
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                GenerateMessage(Messages[i]);
            } 
        }

        public void GenerateMessage(MessageInDiagram messageInDiagram)
        {
            graph = sequenceDiagram.graph;

            // activationblockSource
            graph.nodePrefab = messageInDiagram.ActivationBlockSource;
            var node = graph.AddNode();
            messageInDiagram.ActivationBlockSource = node;
            node.SetActive(true);

            // activationblockDestination
            graph.nodePrefab = messageInDiagram.ActivationBlockDestination;
            node = graph.AddNode();
            messageInDiagram.ActivationBlockDestination = node;
            node.SetActive(true);

            // arrowMessage (set text)
            graph.nodePrefab = messageInDiagram.Arrow;
            node = graph.AddNode();
            messageInDiagram.Arrow = node;
            var messageText = node.transform.Find("Message");
            messageText.GetComponent<TextMeshProUGUI>().text = messageInDiagram.MessageText;

            //generateArrow(messageInDiagram);
        }

        // public void generateArrow(MessageInDiagram messageInDiagram)
        // {
        //     var edge = graph.AddEdgeSeq(messageInDiagram.ActivationBlockSource, messageInDiagram.ActivationBlockDestination,  messageInDiagram.Arrow);
            
        //     var uEdge = edge.GetComponent<UEdge>();
        //     uEdge.Points = new Vector2[]
        //     {
        //         messageInDiagram.ActivationBlockSource.transform.position,
        //         messageInDiagram.ActivationBlockDestination.transform.position
        //     };
        //     // graph.nodePrefab = messageInDiagram.Arrow;
        //     // node = graph.AddNode();
        //     // messageInDiagram.Arrow = node;
        //     // node.SetActive(true);
        // }

        public void LayoutMessagesWithActivationBlocks()
        {
            foreach (MessageInDiagram messageInDiagram in Messages) {
                Transform SEObj = messageInDiagram.SourceEntity.LifeLine.transform;
                Transform SEVOH = messageInDiagram.SourceEntity.VisualObjectHeader.transform;

                Vector3 SourcePosition = new Vector3(SEObj.position.x, SEVOH.position.y-150, 0);
                messageInDiagram.ActivationBlockSource.transform.position = SourcePosition;

                // Napr
                ScaleAndRepositionOfBlock(messageInDiagram.ActivationBlockSource.transform, new Vector3(1, 100, 1));
                ScaleAndRepositionOfBlock(messageInDiagram.ActivationBlockSource.transform, new Vector3(1, 10, 1));

                Transform DEObj = messageInDiagram.DestinationEntity.LifeLine.transform;
                Transform DEVOH = messageInDiagram.DestinationEntity.VisualObjectHeader.transform;

                Vector3 DestPosition = new Vector3(DEObj.position.x, DEVOH.position.y-150, 0);
                messageInDiagram.ActivationBlockDestination.transform.position = DestPosition;
            
                // Napr
                ScaleAndRepositionOfBlock(messageInDiagram.ActivationBlockDestination.transform, new Vector3(1, 333, 1));

                messageInDiagram.Arrow.transform.position = 
                new Vector3((SourcePosition.x + DestPosition.x)/2, DEVOH.position.y-150, 0);
            }
        }

        public void ScaleAndRepositionOfBlock(Transform obj, Vector3 newScale)
        {
            float oldHeight = obj.localScale.y;

            float offset = oldHeight / 4f;
            obj.position += new Vector3(0, offset, 0);

            obj.localScale = newScale;
            offset = newScale.y / 4f;
            obj.position -= new Vector3(0, offset, 0);
        }
    }
}
