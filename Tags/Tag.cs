using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tags
{
    public class Tag
    {
        public string Text { get; set; }

        public List<Tag> Tags { get; set; }

    }
}
