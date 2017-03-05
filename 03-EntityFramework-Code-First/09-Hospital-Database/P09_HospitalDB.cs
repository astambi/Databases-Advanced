using _09_Hospital_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Hospital_Database
{
    class P09_HospitalDB
    {
        static void Main(string[] args)
        {
            HospitalContext context = new HospitalContext();
            context.Database.Initialize(true);
            SeedPatients(context);
            SeedVisitations(context);
            SeedDiagnoses(context);
            SeedMedications(context);
        }

        private static void SeedMedications(HospitalContext context)
        {
            context.Medications.Add(new Medication(1, "Less stress at SoftUni"));
            context.Medications.Add(new Medication(2, "Yoga, walking, relaxing music, eating health"));
            context.Medications.Add(new Medication(3, "No angagements for at least a month"));
            context.SaveChanges();
        }
        private static void SeedDiagnoses(HospitalContext context)
        {
            context.Diagnoses.Add(new Diagnose(1, "Insomnia", "Severe"));
            context.Diagnoses.Add(new Diagnose(2, "Anxiety", "Recovering"));
            context.Diagnoses.Add(new Diagnose(3, "Coughing", "Improving"));
            context.SaveChanges();
        }

        private static void SeedVisitations(HospitalContext context)
        {
            context.Visitations.Add(new Visitation(1, new DateTime(2017, 1, 7, 14, 0, 0), "Annual medical check up"));
            context.Visitations.Add(new Visitation(2, new DateTime(2017, 2, 15, 18, 30, 0), "Follow up in 3 months"));
            context.Visitations.Add(new Visitation(3, DateTime.Now, "Regular monthly visits required to monitor the condition"));
            context.SaveChanges();
        }

        private static void SeedPatients(HospitalContext context)
        {
            context.Patients.Add(new Patient(
                "Svetlin", "Nakov", "Sofia, 15-17 Tintiava", "nakov@softuni.bg", new DateTime(1980, 10, 10), true));
            context.Patients.Add(new Patient(
                "Angela", "Merkel", "Berlin", "Angela.Merkel@BundesrepublikDeutschland.de", new DateTime(1954, 7, 17), true));
            context.Patients.Add(new Patient(
                "Veselina", "Katsarova", "Lucern", "veselina-katsarova@musicferein.at", new DateTime(1960, 3, 27), true));
            context.SaveChanges();
        }
    }
}