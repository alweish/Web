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
    
    public partial class Student : User
    {
        public Student()
        {
            this.Results = new HashSet<Result>();
        }
    
        public bool IsVerified { get; set; }
        public bool IsDismissed { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
    
    #pragma warning restore 1591
    
}
