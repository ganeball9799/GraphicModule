using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicModuleUI.ViewModels
{
    public class TreeNode<T>
    {
        public string Name { get; set; }

        public List<T> Lines { get; set; }
    }
}
