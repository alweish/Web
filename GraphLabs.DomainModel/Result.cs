//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GraphLabs.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    
    #pragma warning disable 1591
    
    public partial class Result
    {
        public Result()
        {
            this.Actions = new HashSet<Action>();
        }
    
        public long Id { get; private set; }
        public LabExecutionMode Mode { get; set; }
        public System.DateTime StartDateTime { get; private set; }
        public Nullable<Grade> Grade { get; set; }
    
        public virtual ICollection<Action> Actions { get; set; }
        public virtual Student Student { get; set; }
        public virtual LabVariant LabVariant { get; set; }
    }
    
    #pragma warning restore 1591
    
}
