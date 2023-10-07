using System.Collections.Generic;

namespace Sample.Services.Models
{
    /// <summary>
    /// 树形结构
    /// </summary>
    public class TreeClass : TreeNode<TreeClass>
    {

    }

    public class TreeNode<T>
    {
        public string Key { get; set; }

        public string Parent { get; set; }

        public List<T> Children { get; set; }
    }

    public class TreeNode<T, T2>
    {
        public string Key { get; set; }

        public string Parent { get; set; }

        public List<T> Children { get; set; }
    }

    public class TreeNode<T, T2, T3>
    {
        public string Key { get; set; }

        public string Parent { get; set; }

        public List<T> Children { get; set; }
    }
}
