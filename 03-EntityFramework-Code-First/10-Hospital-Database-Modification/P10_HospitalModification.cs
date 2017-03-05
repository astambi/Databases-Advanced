using _10_Hospital_Database_Modification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_Hospital_Database_Modification
{
    class P10_HospitalModification
    {
        static void Main(string[] args)
        {
            HospitalContext context = new HospitalContext();
            
            // copied from Problem 09.Hospital Database
            context.Database.Initialize(true);
            SeedPatients(context);
            SeedVisitations(context);
            SeedDiagnoses(context);
            SeedMedications(context);

            // Modification
            SeedDoctors(context);
            UpdatePastVisitations(context); // comment to not update null doctorIds in past visitations to id=1 (Unknown doctor)
            SeedNewVisitationsWithDoctorId(context);
        }

        private static void SeedNewVisitationsWithDoctorId(HospitalContext context)
        {
            context.Visitations.Add(new Visitation(1, DateTime.Now, "Annual medical check up", 2));
            context.Visitations.Add(new Visitation(2, DateTime.Now, "Follow up in 2 months", 2));
            context.Visitations.Add(new Visitation(3, DateTime.Now, "Regular monthly visits required to monitor the condition", 3));
            context.SaveChanges();
        }

        private static void UpdatePastVisitations(HospitalContext context)
        {
            // update previous visitations to an unknown doctor
            context.Visitations
                .Where(v => v.DoctorId == null).ToList()
                .ForEach(x => x.DoctorId = 1);
            context.SaveChanges();
        }

        private static void SeedDoctors(HospitalContext context)
        {
            context.Doctors.Add(new Doctor("Unknown", "Unknown")); // for past visitations
            context.Doctors.Add(new Doctor("D-r Mihailov", "Homeopathy"));
            context.Doctors.Add(new Doctor("D-r Emil Iliev", "Dermatology"));
            context.SaveChanges();
            // then update past visitations to an anonymous doctor
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
