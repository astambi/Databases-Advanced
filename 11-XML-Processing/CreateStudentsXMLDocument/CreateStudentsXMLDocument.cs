namespace CreateStudentsXMLDocument
{
    using System;
    using System.Xml.Linq;

    class CreateStudentsXMLDocument
    {
        static void Main(string[] args)
        {
            XDocument xmlDoc = CreateXmlDocument();
            PrintAndExportToFile(xmlDoc);
        }

        private static void PrintAndExportToFile(XDocument xmlDoc)
        {
            Console.WriteLine(xmlDoc);

            xmlDoc.Save("../../ExportsXml/students.xml");
            xmlDoc.Save("../../ExportsXml/studentsNoFormatting.xml", SaveOptions.DisableFormatting);
            Console.WriteLine("\nStudents exported as: \nExportsXml/students.xml & ExportsXml/studentsNoFormatting.xml");
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
