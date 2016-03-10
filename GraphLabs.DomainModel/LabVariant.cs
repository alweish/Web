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
    
    public partial class LabVariant : AbstractEntity
    {
        protected LabVariant()
        {
            this.IntroducingVariant = false;
            this.TestQuestions = new HashSet<TestQuestion>();
            this.TaskVariants = new HashSet<TaskVariant>();
            this.Results = new HashSet<Result>();
        }
    
        public long Id { get; private set; }
        public string Number { get; set; }
        public long Version { get; set; }
        public bool IntroducingVariant { get; set; }
    
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<TaskVariant> TaskVariants { get; set; }
        public virtual LabWork LabWork { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
    
    #pragma warning restore 1591
    
}
