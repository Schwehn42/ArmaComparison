using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaComparison
{
    /**
     * <summary>A ModElement contains a name a id.
     * The id refers to the steam workshop id.</summary>
     */
    class ModElement
    {
        protected string name { get; }
        /**
         * <summary>the steam workshop id</summary>
         */
        public string id { get;  }

        public ModElement(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return this.name + " (" + this.id + ")";
        }
    }
}
