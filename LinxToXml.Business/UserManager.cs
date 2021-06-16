using LinxToXml.Interfaces;
using LinxToXml.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using System.Data;

namespace LinxToXml.Business
{
    public class UserManager : IUser
    {
        string FileName = "";

        /// <summary>
        /// Find path of the XML Document on user profile
        /// </summary>
        private void PathFinder()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
                FileName = path + @"\AppData\Local\Test\User.xml";
            }
        }

        /// <summary>
        /// Check if necessary folder for XML Document exists on user profile
        /// </summary>
        private void CheckFolder()
        {
            String FolderName = "";
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
                FolderName = path + @"\AppData\Local\Test";
            }

            if (!Directory.Exists(FolderName))
            {
                DirectoryInfo di = Directory.CreateDirectory(FolderName);
            }

        }

        /// <summary>
        /// Add User data to XML Document
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {

            PathFinder();
            if (File.Exists(FileName))
            {
                var xmlDocument = XDocument.Load(FileName);

                xmlDocument.Element("Users").Add(new XElement("User",
                      new XAttribute("ID", user.ID),
                      new XElement("FirstName", user.FirstName),
                      new XElement("LastName", user.LastName),
                    new XElement("StreetName", user.StreetName),
                    new XElement("HouseNumber", user.HouseNumber),
                    new XElement("ApartmentNumber", user.ApartmentNumber),
                    new XElement("PostalCode", user.PostalCode),
                    new XElement("Town", user.Town),
                    new XElement("PhoneNumber", user.PhoneNumber),
                    new XElement("DateofBirth", user.DateofBirth)
                      ));

                xmlDocument.Save(FileName);
            }
            else
            {
                CheckFolder();
                XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Creating an XML Tree using LINQ to XML"),
                new XElement("Users",
                  new XElement("User",
                  new XAttribute("ID", user.ID),
                    new XElement("FirstName", user.FirstName),
                    new XElement("LastName", user.LastName),
                    new XElement("StreetName", user.StreetName),
                    new XElement("HouseNumber", user.HouseNumber),
                    new XElement("ApartmentNumber", user.ApartmentNumber),
                    new XElement("PostalCode", user.PostalCode),
                    new XElement("Town", user.Town),
                    new XElement("PhoneNumber", user.PhoneNumber),
                    new XElement("DateofBirth", user.DateofBirth)
                )));
                xmlDocument.Save(FileName);
            }
        }

        /// <summary>
        /// Delete matching User data from XML Document
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            PathFinder();
            if (File.Exists(FileName))
            {
                var xmlDocument = XDocument.Load(FileName);
                var existingElement = xmlDocument.Element("Users").Elements("User")
                    .Where(element => element.Attribute("ID").Value == ID.ToString())
                    .FirstOrDefault();

                if (existingElement != null)
                {
                    existingElement.Remove();
                    xmlDocument.Save(FileName);
                }
            }
        }

        /// <summary>
        /// Get all nodes from XML Document
        /// </summary>
        /// <returns></returns>
        public DataSet GetAll()
        {
            PathFinder();
            if (File.Exists(FileName))
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(FileName);
                return dataSet;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Update/Edit matching user data on XML Document
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            PathFinder();
            if (File.Exists(FileName))
            {
                var xmlDocument = XDocument.Load(FileName);
                var existingElement = xmlDocument.Element("Users").Elements("User")
                    .Where(element => element.Attribute("ID").Value == user.ID.ToString())
                    .FirstOrDefault();
                existingElement.SetElementValue("FirstName", user.FirstName);
                existingElement.SetElementValue("LastName", user.LastName);
                existingElement.SetElementValue("StreetName", user.StreetName);
                existingElement.SetElementValue("HouseNumber", user.HouseNumber);
                existingElement.SetElementValue("ApartmentNumber", user.ApartmentNumber);
                existingElement.SetElementValue("PostalCode", user.PostalCode);
                existingElement.SetElementValue("Town", user.Town);
                existingElement.SetElementValue("PhoneNumber", user.PhoneNumber);
                existingElement.SetElementValue("DateofBirth", user.DateofBirth);

                xmlDocument.Save(FileName);
            }

        }
    }
}
