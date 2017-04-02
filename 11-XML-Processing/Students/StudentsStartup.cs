namespace CreateStudentsXMLDocument
{
    using System;
    using System.Xml.Linq;

    class StudentsStartup
    {
        static void Main(string[] args)
        {
            // Creating XML Document in one go
            XDocument xmlDoc = CreateXmlDocument();
            ExportAndPrint(xmlDoc, "students1");

            // Creating XML Document from compenent Elements
            XDocument studentsXmlDoc = CreateXmlDocumentFromElements(); 
            ExportAndPrint(studentsXmlDoc, "students2");
        }

        private static void ExportAndPrint(XDocument xmlDoc, string fileName) // filename without extentsion
        {
            Console.WriteLine(xmlDoc);
            xmlDoc.Save($"../../Export/{fileName}.xml");
            xmlDoc.Save($"../../Export/{fileName}NoFormatting.xml", SaveOptions.DisableFormatting);
            Console.WriteLine($"\nXML Document exported as: \nExport/{fileName}.xml & Export/{fileName}NoFormatting.xml\n");
        }

        private static XDocument CreateXmlDocumentFromElements()
        {
            // Creating XML Document students, holding info on 2 students & their exams

            // Exams
            XElement examsStudent1 = CreateCollection("exams");
            examsStudent1.Add(
                CreateExam("Databases Basics", "2017/02/19", "6.00"),
                CreateExam("JS Applications", "2016/12/11", "6.00"),
                CreateExam("JS Advanced", "2016/11/13", "6.00"),
                CreateExam("JS Fundamentals", "2016/10/16", "6.00")
                );
            XElement examsStudent2 = CreateCollection("exams");
            examsStudent2.Add(
                CreateExam("Databases Basics", "2017/02/19", "6.00"),
                CreateExam("JS Applications", "2016/12/11", "6.00")
                );
            // Students
            XElement students = CreateCollection("students");
            students.Add(
                CreateStudent("Petar Petrov", "Male", "1989/12/23", "+359 87654321", "Petar.Petrov@softuni.bg", "Software University", "C# Web Developer", "111111111", examsStudent1),
                CreateStudent("Ivan Ivanov", "Male", "1999/12/23", "+359 87654321", "Ivan.Ivanov@softuni.bg", "Software University", "C# Web Developer", "222222222", examsStudent2)
                );

            // XmlDocument holding students
            XDocument studentsXmlDoc = new XDocument();
            studentsXmlDoc.Add(students);

            return studentsXmlDoc;
        }

        private static XElement CreateStudent(string name, string gender, string birthDate, string phoneNumber, string email, string university, string specialty, string facultyNumber, XElement exams)
        {
            return new XElement("student",
                        new XElement("name", name),
                        new XElement("gender", gender),
                        new XElement("birthDate", birthDate),
                        new XElement("phoneNumber", phoneNumber),
                        new XElement("email", email),
                        new XElement("univesity", university),
                        new XElement("specialty", specialty),
                        new XElement("facultyNumber", facultyNumber),
                        exams);
        }

        private static XElement CreateExam(string name, string dateTaken, string grade)
        {
            return new XElement($"exam",
                        new XElement("name", name),
                        new XElement("dateTaken", dateTaken),
                        new XElement("grade", grade));
        }

        private static XElement CreateCollection(string collection)
        {
            return new XElement($"{collection}");
        }

        private static XDocument CreateXmlDocument()
        {
            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(
                new XElement("students",
                    new XElement("student",
                        new XElement("name", "Petar Petrov"),
                        new XElement("gender", "Male"),
                        new XElement("birthDate", "1989/12/23"),
                        new XElement("phoneNumber", "+359 87654321"),
                        new XElement("email", "Petar.Petrov@softuni.bg"),
                        new XElement("univesity", "Software University"),
                        new XElement("specialty", "C# Web Developer"),
                        new XElement("facultyNumber", "111111111"),
                        new XElement("exams",
                            new XElement("exam",
                                new XElement("name", "Databases Basics"),
                                new XElement("dateTaken", "2017/02/19"),
                                new XElement("grade", "6.00")),
                            new XElement("exam",
                                new XElement("name", "JS Applications"),
                                new XElement("dateTaken", "2016/12/11"),
                                new XElement("grade", "6.00")),
                            new XElement("exam",
                                new XElement("name", "JS Advanced"),
                                new XElement("dateTaken", "2016/11/13"),
                                new XElement("grade", "6.00")),
                            new XElement("exam",
                                new XElement("name", "JS Fundamentals"),
                                new XElement("dateTaken", "2016/10/16"),
                                new XElement("grade", "6.00"))
                        )),
                    new XElement("student",
                        new XElement("name", "Ivan Ivanov"),
                        new XElement("gender", "Male"),
                        new XElement("birthDate", "1999/12/23"),
                        new XElement("phoneNumber", "+359 87654321"),
                        new XElement("email", "Ivan.Ivanov@softuni.bg"),
                        new XElement("univesity", "Software University"),
                        new XElement("specialty", "C# Web Developer"),
                        new XElement("facultyNumber", "222222222"),
                        new XElement("exams",
                            new XElement("exam",
                                new XElement("name", "Databases Basics"),
                                new XElement("dateTaken", "2017/02/19"),
                                new XElement("grade", "6.00")),
                            new XElement("exam",
                                new XElement("name", "JS Applications"),
                                new XElement("dateTaken", "2016/12/11"),
                                new XElement("grade", "6.00"))
                        ))
            ));
            return xmlDoc;
        }
    }
}
