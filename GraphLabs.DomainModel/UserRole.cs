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
    using GraphLabs.DomainModel.Infrastructure;
    
    [Flags]
    public enum UserRole : int
    {
        Student = 1,
        Teacher = 2,
        Administrator = 4,
        None = 0
    }
}
