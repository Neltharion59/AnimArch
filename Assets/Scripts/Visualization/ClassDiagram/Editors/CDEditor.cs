using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OALProgramControl;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.ClassDiagram.Relations;
using Attribute = Visualization.ClassDiagram.ClassComponents.Attribute;

namespace Visualization.ClassDiagram.Editors
{
    public static class CDEditor
    {
        public static CDClass CreateNode(Class newClass)
        {
            CDClass cdClass = null;
            var i = 0;
            var baseName = newClass.Name;
            while (cdClass == null)
            {
                newClass.Name = baseName + (i == 0 ? "" : i.ToString());
                cdClass = Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.SpawnClass(newClass.Name);
                i++;
                if (i > 1000) break;
            }

            return cdClass;
        }

        private static CDAttribute CreateCdAttributeFromAttribute(Attribute attribute)
        {
            return new CDAttribute(attribute.Name, EXETypes.ConvertEATypeName(attribute.Type));
        }

        public static void AddAttribute(ClassInDiagram classInDiagram, Attribute attribute)
        {
            var cdAttribute = CreateCdAttributeFromAttribute(attribute);
            classInDiagram.ClassInfo.AddAttribute(cdAttribute);
        }

        public static void UpdateAttribute(ClassInDiagram classInDiagram, string oldAttribute, Attribute newAttribute)
        {
            var cdAttribute = classInDiagram.ClassInfo.GetAttributeByName(oldAttribute);
            cdAttribute.Reset();
            cdAttribute.UpdateAttribute(newAttribute);
        }

        private static CDMethod CreateCdMethodFromMethod(CDClass cdClass, Method method)
        {
            return new CDMethod(cdClass, method.Name, EXETypes.ConvertEATypeName(method.ReturnValue));
        }

        public static void AddMethod(ClassInDiagram classInDiagram, Method method)
        {
            var cdMethod = CreateCdMethodFromMethod(classInDiagram.ClassInfo, method);

            if (method.arguments != null)
                AddParameters(method, cdMethod);

            classInDiagram.ClassInfo.AddMethod(cdMethod);
        }


        private static void AddParameters(Method method, CDMethod cdMethod)
        {
            foreach (var argument in method.arguments)
            {
                var tokens = argument.Split(' ');
                var type = tokens[0];
                var name = tokens[1];
                if (cdMethod.Parameters.Where(_parameter => _parameter.Name.Equals(name)).Any()) {
                    UnityEngine.Debug.LogError("tried to add parameter with existing name!");
                    continue;
                }
                cdMethod.Parameters.Add(new CDParameter { Name = name, Type = EXETypes.ConvertEATypeName(type) });
            }
        }

        public static void UpdateMethod(ClassInDiagram classInDiagram, string oldMethod, Method newMethod)
        {
            var cdMethod = classInDiagram.ClassInfo.GetMethodByName(oldMethod);
            cdMethod.Reset();
            cdMethod.UpdateMethod(classInDiagram.ClassInfo, newMethod);
            if (newMethod.arguments != null)
            {
                AddParameters(newMethod, cdMethod);
            }
        }

        public static CDRelationship CreateRelation(Relation relation)
        {
            var cdRelationship =
                Animation.Animation.Instance.CurrentProgramInstance.RelationshipSpace.SpawnRelationship(relation.FromClass, relation.ToClass)
                ?? throw new ArgumentNullException(nameof(relation));
            relation.OALName = cdRelationship.RelationshipName;

            if (!"Generalization".Equals(relation.PropertiesEaType) && !"Realisation".Equals(relation.PropertiesEaType))
                return cdRelationship;

            var fromClass = Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.getClassByName(relation.FromClass);
            var toClass = Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.getClassByName(relation.ToClass);

            if (fromClass != null && toClass != null) fromClass.SuperClass = toClass;

            return cdRelationship;
        }

        public static void DeleteAttribute(ClassInDiagram classInDiagram, string attribute)
        {
            classInDiagram.ClassInfo.DeleteAttributesByName(attribute);
        }

        public static void DeleteMethod(ClassInDiagram classInDiagram, string method)
        {
            classInDiagram.ClassInfo.DeleteMethodsByName(method);
        }

        public static void DeleteNode(ClassInDiagram classInDiagram)
        {
            Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.Classes.Remove(classInDiagram.ClassInfo);
        }

        public static void DeleteRelation(RelationInDiagram relationInDiagram)
        {
            Animation.Animation.Instance.CurrentProgramInstance.RelationshipSpace.RemoveRelationship(relationInDiagram.RelationInfo);

            if (!"Generalization".Equals(relationInDiagram.ParsedRelation.PropertiesEaType) &&
                !"Realisation".Equals(relationInDiagram.ParsedRelation.PropertiesEaType))
                return;

            var fromClass = Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.getClassByName(relationInDiagram.RelationInfo.FromClass);
            Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.getClassByName(relationInDiagram.RelationInfo.FromClass)
                .SuperClass = null;

            Animation.Animation.Instance.CurrentProgramInstance.ExecutionSpace.getClassByName(relationInDiagram.RelationInfo.ToClass).SubClasses
                .Remove(fromClass);
        }
    }
}
