﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CopaDoMundo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CopaDoMundoEntities : DbContext
    {
        public CopaDoMundoEntities()
            : base("name=CopaDoMundoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Chaves> Chaves { get; set; }
        public virtual DbSet<Jogos> Jogos { get; set; }
        public virtual DbSet<TimesDaCopa> TimesDaCopa { get; set; }
    }
}
