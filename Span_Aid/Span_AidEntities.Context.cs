﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Span_Aid
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Span_AidEntities : DbContext
    {
        public Span_AidEntities()
            : base("name=Span_AidEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HealingRadius_Ticket> HealingRadius_Ticket { get; set; }
        public virtual DbSet<TruckLogics_Ticket> TruckLogics_Ticket { get; set; }
    }
}
