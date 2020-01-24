using System.Collections.Generic;

namespace Nodes {
    public class UpdateEventNode : Node { 
        public List<Node> ChildrenNodes;
        public UpdateEventNode() {
            Inputs = new List<InputNode>{new CodeBranchInputNode(this)};
            Outputs = new List<OutputNode>();
            ChildrenNodes = new List<Node>();
        }
        public override string GetCode(int callStackLevel) {
            var code =  "private void Update() {\n";
            ChildrenNodes.ForEach(node => code += node.Outputs.Count == 0 ? node.GetCode(callStackLevel+1) + "\n" : "");
            code += "}";
            return code;
        }
        
        public void AddChildNode(Node node) {
            ChildrenNodes.Add(node);
        }
        public void RemoveChildNode(Node node) {
            ChildrenNodes.Remove(node);
        }
    }
}