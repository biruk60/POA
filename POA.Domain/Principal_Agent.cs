//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POA.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Principal_Agent
    {
        public Principal_Agent()
        {
            this.Principal_Agent_Category_SubCategory = new HashSet<Principal_Agent_Category_SubCategory>();
        }
    
        public int Principal_Agent_ID { get; set; }
        public int Principal_ID { get; set; }
        public int Agent_ID { get; set; }
        public int Template_ID { get; set; }
    
        public virtual ICollection<Principal_Agent_Category_SubCategory> Principal_Agent_Category_SubCategory { get; set; }
        public virtual Template Template { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Agent Agent1 { get; set; }
    }
}
