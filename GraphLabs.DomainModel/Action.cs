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
    using GraphLabs.DomainModel.Infrastructure;
    
    
    #pragma warning disable 1591
    
    public partial class Action : AbstractEntity
    {
        public long Id { get; private set; }
        public string Description { get; set; }
        public int Penalty { get; set; }
        public System.DateTime Time { get; set; }
    
        public virtual Result Result { get; set; }
        public virtual Task Task { get; set; }
    }
    
    #pragma warning restore 1591
    
}
