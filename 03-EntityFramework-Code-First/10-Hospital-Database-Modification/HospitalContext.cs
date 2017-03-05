namespace _10_Hospital_Database_Modification
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HospitalContext : DbContext
    {
        // Your context has been configured to use a 'HospitalContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_10_Hospital_Database_Modification.HospitalContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HospitalContext' 
        // connection string in the application configuration file.
        public HospitalContext()
            : base("name=HospitalContext")
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Diagnose> Diagnoses { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Visitation> Visitations { get; set; }

        // Modification
        public virtual DbSet<Doctor> Doctors { get; set; }
    }
}