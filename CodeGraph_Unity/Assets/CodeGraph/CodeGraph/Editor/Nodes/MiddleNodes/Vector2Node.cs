using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Math", "Vectors", "Vector2")]
    public class Vector2Node : AbstractMiddleNode {
        public Vector2Node() {
            Initialize("Vector2", DefaultNodePosition);
            var xInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            xInputPort.portName = "x";
            var yInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            yInputPort.portName = "y";
            AddInputPort(xInputPort, () => {
                var connections = xInputPort.connections.ToList();
                if (connections.Count == 0) return $"0.0f /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"0.0f /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            AddInputPort(yInputPort, () => {
                var connections = yInputPort.connections.ToList();
                if (connections.Count == 0) return $"0.0f /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"0.0f /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var vector3OutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            vector3OutputPort.portName = "(2)";
            AddOutputPort(vector3OutputPort, () => $"new Vector2({InputPorts[0].RequestCode()},{InputPorts[1].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var floatNode1Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode1Position.center += new Vector2(-DefaultNodeSize.x/1.5f, -25f);
            var floatNode2Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode2Position.center += new Vector2(-DefaultNodeSize.x/1.5f, 50f);
            CreateAndConnectNode<FloatNode>(floatNode1Position, 0, 0, this);
            CreateAndConnectNode<FloatNode>(floatNode2Position, 0, 1, this);
        }
    }
}