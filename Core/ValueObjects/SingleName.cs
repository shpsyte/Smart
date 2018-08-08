using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ValueObjects
{
    public class SingleName
    {
        public SingleName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }
}
