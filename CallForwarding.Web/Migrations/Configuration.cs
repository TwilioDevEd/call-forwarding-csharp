namespace CallForwarding.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.VisualBasic.FileIO;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using System.IO;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CallForwardingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }        

        protected override void Seed(CallForwardingContext context)
        {
            ParseAndSaveZipcodes(context);
            ParseAndSaveStatesAndSenators(context);
        }

        private void ParseAndSaveStatesAndSenators(CallForwardingContext context)
        {
            string resourceName = MapPath("senators.json");
            var senatorsFileContent = File.ReadAllText(resourceName);

            var json = JObject.Parse(senatorsFileContent);

            var stateList = json["states"].Children().ToList();
            foreach (JToken stateJson in stateList)
            {

                var stateString = stateJson.ToString();
                var stateObject = new State() { name = stateString };
                context.States.AddOrUpdate(stateObject);
                if (json[stateString] == null)
                    continue;

                var senators = json[stateString].ToArray();
                foreach (JToken senatorJson in senators)
                {
                    context.Senators.AddOrUpdate(new Senator()
                    {
                        Name = senatorJson["name"].ToString(),
                        Phone = senatorJson["phone"].ToString(),
                        State = stateObject
                    });

                }
                context.SaveChanges();
            }
        }

        private void ParseAndSaveZipcodes(CallForwardingContext context)
        {
            string resourceName = MapPath("free-zipcode-database.csv");
            using (var parser = new TextFieldParser(resourceName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                var zipcodes = new List<Zipcode>();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    int zipcodeNumber;
                    bool isNumeric = int.TryParse(fields[0], out zipcodeNumber);
                    if (isNumeric)
                    {
                        zipcodes.Add(new Zipcode()
                        {
                            State = fields[3],
                            ZipcodeNumber = zipcodeNumber
                        });

                        if (zipcodes.Count > 1000)
                        {

                            SaveRange(context, zipcodes);
                            zipcodes = new List<Zipcode>();
                        }
                    }

                }
                SaveRange(context, zipcodes);
            }
        }

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var directoryName = Path.GetDirectoryName(absolutePath);

            var path = Path.Combine(directoryName, "..\\Resources\\" + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }

        private void SaveRange(CallForwardingContext context, List<Zipcode> zipcodes)
        {
            context.Zipcodes.AddRange(zipcodes);
            context.SaveChanges();
        }
    }
}
