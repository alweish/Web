//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GraphLabs.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestQuestion
    {
        public TestQuestion()
        {
            this.AnswerVariants = new HashSet<AnswerVariant>();
            this.LabVariant = new HashSet<LabVariant>();
        }
    
        public long Id { get; private set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string TypeOfQuestion { get; set; }
        public string Section { get; set; }
    
        public virtual ICollection<AnswerVariant> AnswerVariants { get; set; }
        public virtual ICollection<LabVariant> LabVariant { get; set; }
    }
}
