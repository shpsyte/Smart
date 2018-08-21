using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ValueObjects
{
    public class SingleName
    {
        public SingleName()
        {
            
        }

        public SingleName(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }

    }
}
