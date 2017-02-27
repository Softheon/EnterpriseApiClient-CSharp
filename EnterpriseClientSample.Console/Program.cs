using Newtonsoft.Json;
using System;
using System.Net;

namespace EnterpriseClientSample.Console
{
    /// <summary>
    /// Sample of executing the Enterprise Rest API
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The OAuth client identifier
        /// </summary>
        private static string ClientID = "hack001";

        /// <summary>
        /// The OAuth client secret
        /// </summary>
        private static string ClientSecret = "hack001";

        /// <summary>
        /// The OAuth scope
        /// </summary>
        private static string Scope = "enterpriseapi";

        /// <summary>
        /// The identity server token endpoint URI
        /// </summary>
        private static string TokenEndpointUri = "https://hack.softheon.io/api/identity/core/connect/token";

        /// <summary>
        /// The enterprise REST API endpoint URI
        /// </summary>
        private static string EnterpriseEndpointUri = "https://hack.softheon.io/api/enterprise";

        /// <summary>
        /// The template type
        /// </summary>
        private static int TemplateType = 999;

        /// <summary>
        /// The drawer identifier
        /// </summary>
        private static int DrawerID = 1;

        /// <summary>
        /// Whether or not the applicaiton cleans up after it self
        /// </summary>
        private static bool ShouldCleanup = true;

        /// <summary>
        /// The entrpy point of the application
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // Get Token
            var tokenResult = OAuthToken.Get(TokenEndpointUri, ClientID, ClientSecret, Scope);
            dynamic jsonToken = JsonConvert.DeserializeObject(tokenResult);
            var token = jsonToken.access_token.Value as string;

            // Demonstrate inserting and fetching templates
            InsertTemplate(token, TemplateType);
            GetTemplate(token, TemplateType);

            // Demonstrate inserting, updating and fetching entitys
            var entityID = InsertEntity(token, DrawerID, TemplateType);
            GetEntity(token, DrawerID, entityID);
            UpdateEntity(token, DrawerID, entityID);
            GetEntity(token, DrawerID, entityID);

            // Demonstrate deleting entitys and templates
            if (ShouldCleanup)
            {
                DeleteEntity(token, DrawerID, entityID);
                DeleteTemplate(token, TemplateType);
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }

        #region Templates

        /// <summary>
        /// Inserts the template.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="type">The template type.</param>
        private static void InsertTemplate(string token, int type)
        {
            System.Console.WriteLine("=== Insert Template ===");
            var templateClient = new EntityTemplateClient(EnterpriseEndpointUri);

            // Create request object
            var template = new
            {
                Category = "Common",
                Type = type,
                Name = "Hello World",
                Profiles = new object[] {
                    new
                    {
                        Type = 1,
                        Name = "Content",
                        Fields = new object[] {
                        new
                        {
                            Name = "Phrase 1",
                            Type = "String",
                            Index = 0,
                            Position = 0,
                            Default = "Hello",
                            Length = 10
                        },
                        new
                        {
                            Name = "Phrase 2",
                            Type = "ComboBox",
                            Index = 0,
                            Position = 1,
                            Items = new object [] {
                              new
                              {
                                  Name = "World",
                                  Value = 0,
                                  Type = "ComboBoxItem"
                              },
                              new
                              {
                                  Name = "CEWIT",
                                  Value = 1,
                                  Type = "ComboBoxItem"
                              },
                              new
                              {
                                  Name = "Hackers",
                                  Value = 2,
                                  Type = "ComboBoxItem"
                              }
                          }
                        }
                        }
                    },
                    new
                    {
                        Type = 2,
                        Name = "Demo",
                        Fields = new object[] {
                        new
                        {
                            Name = "Basic String",
                            Type = "String",
                            Index = 0,
                            Position = 0
                        },
                        new
                        {
                            Name = "Multiline String",
                            Type = "String",
                            Index = 1,
                            Position = 1,
                            Length = 500,
                            IsMultiline = true
                        },
                        new
                        {
                            Name = "Basic Integer",
                            Type = "Integer",
                            Index = 0,
                            Position = 2
                        },
                        new
                        {
                            Name = "Basic Double",
                            Type = "Double",
                            Index = 0,
                            Position = 3
                        },
                        new
                        {
                            Name = "Basic Date",
                            Type = "DateTime",
                            Index = 0,
                            Position = 4
                        },
                        new
                        {
                            Name = "Check Box",
                            Type = "CheckBox",
                            Index = 1,
                            Position = 5
                        },
                        new
                        {
                            Name = "Radio Button",
                            Type = "RadioButton",
                            Index = 2,
                            Position = 6,
                            Items = new object [] {
                              new
                              {
                                  Name = "Radio 1",
                                  Value = 0,
                                  Type = "RadioButton"
                              },
                              new
                              {
                                  Name = "Radio 2",
                                  Value = 1,
                                  Type = "RadioButton"
                              },
                              new
                              {
                                  Name = "Radio 3",
                                  Value = 2,
                                  Type = "RadioButton"
                              }
                            }
                        },
                        new
                        {
                            Name = "Combo Box",
                            Type = "ComboBox",
                            Index = 3,
                            Position = 7,
                            Items = new object [] {
                              new
                              {
                                  Name = "Combo 1",
                                  Value = 0,
                                  Type = "ComboBoxItem"
                              },
                              new
                              {
                                  Name = "Combo 2",
                                  Value = 1,
                                  Type = "ComboBoxItem"
                              },
                              new
                              {
                                  Name = "Combo 3",
                                  Value = 2,
                                  Type = "ComboBoxItem"
                              }
                            }
                        },
                        new
                        {
                            Name = "List Box",
                            Type = "ListBox",
                            Index = 4,
                            Position = 8,
                            Items = new object [] {
                              new
                              {
                                  Name = "List 1",
                                  Value = 1,
                                  Type = "ListBoxItem"
                              },
                              new
                              {
                                  Name = "List 2",
                                  Value = 2,
                                  Type = "ListBoxItem"
                              },
                              new
                              {
                                  Name = "List 3",
                                  Value = 4,
                                  Type = "ListBoxItem"
                              }
                            }
                        }
                        }
                    }
                  },
                Drawers = new [] { 1 }
            };

            // Execute REST call
            var response = templateClient.Insert(token, JsonConvert.SerializeObject(template));            
            System.Console.WriteLine("Result: {0}", response.StatusCode);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                System.Console.WriteLine();
                return;                
            }

            // Display Results
            dynamic templateResult = JsonConvert.DeserializeObject(response.Content);
            System.Console.WriteLine("ID: {0}", templateResult.ID.Value);
            System.Console.WriteLine();
        }

        /// <summary>
        /// Gets the template and displays the results.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="type">The template type.</param>
        private static void GetTemplate(dynamic token, int type)
        {
            System.Console.WriteLine("=== Get Template ===");

            // Execute REST call
            var templateClient = new EntityTemplateClient(EnterpriseEndpointUri);
            var response = templateClient.Get(token, TemplateType);
            System.Console.WriteLine("Result: {0}", response.StatusCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Console.WriteLine();
                return;
            }

            // Display Results
            dynamic templateResult = JsonConvert.DeserializeObject(response.Content);
            System.Console.WriteLine("Template: {0} [{1}]", templateResult.Name.Value, templateResult.Type.Value);

            foreach (var profile in templateResult.Profiles)
            {
                System.Console.WriteLine("{2}Profile: {0} [{1}]", profile.Name.Value, profile.Type.Value, string.Empty.PadLeft(4));
                foreach (var field in profile.Fields)
                {
                    System.Console.WriteLine("{2}Field: {0} [{1}]", field.Name.Value, field.Type.Value, string.Empty.PadLeft(8));
                    foreach (var item in field.Items)
                    {
                        System.Console.WriteLine("{2}Item: {0} [{1}]", item.Name.Value, item.Value.Value, string.Empty.PadLeft(12));
                    }
                }
            }

            foreach (var drawer in templateResult.Drawers)
            {
                System.Console.WriteLine("{1}Drawer: {0}", drawer.Value, string.Empty.PadLeft(4));
            }

            System.Console.WriteLine();
        }

        /// <summary>
        /// Deletes the template.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="type">The template type.</param>
        private static void DeleteTemplate(string token, int type)
        {
            System.Console.WriteLine("=== Delete Template ===");
            var templateClient = new EntityTemplateClient(EnterpriseEndpointUri);
            var response = templateClient.Delete(token, type);

            System.Console.WriteLine("Result: {0}", response.StatusCode);
            System.Console.WriteLine();
        }

        #endregion Templates

        #region Entitys

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawer">The drawer identifier.</param>
        /// <param name="type">The template type.</param>
        private static int InsertEntity(string token, int drawer, int type)
        {
            System.Console.WriteLine("=== Insert Entity ===");
            var entityClient = new EntityClient(EnterpriseEndpointUri);

            // Create request object
            var entity = new
            {
                Acl = -1,
                Type = type,
                Name = "EnterpriseClientSample - CSharp",
                Profiles = new object[]
                {
                    new {
                        Type = 1,
                        Strings = new string[]
                        {
                            "Hello"
                        },
                        Integers = new int[]
                        {
                            2
                        }
                    },
                    new
                    {
                        Type = 2,
                        Strings = new string[]
                        {
                            "Small String",
                            "Big\r\nMultiline\r\nString."
                        },
                        Integers = new int[]
                        {
                            2017,
                            1,
                            2,
                            1,
                            4
                        },
                        Doubles = new double[]
                        {
                            2.17
                        },
                        Dates = new DateTime[]
                        {
                            new DateTime(2017, 2, 17)
                        }
                    }
                }
            };

            // Execute REST call
            var response = entityClient.Insert(token, drawer, JsonConvert.SerializeObject(entity));
            System.Console.WriteLine("Result: {0}", response.StatusCode);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                System.Console.WriteLine();
                return -1;
            }

            // Display results
            dynamic entityResult = JsonConvert.DeserializeObject(response.Content);
            System.Console.WriteLine("ID: {0}", entityResult.ID.Value);
            System.Console.WriteLine();
            return (int)entityResult.ID.Value;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        private static void GetEntity(string token, int drawerID, int entityID)
        {
            System.Console.WriteLine("=== Get Entity ===");

            // Execute REST call
            var entityClient = new EntityClient(EnterpriseEndpointUri);
            var response = entityClient.Get(token, drawerID, entityID);

            System.Console.WriteLine("Result: {0}", response.StatusCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Console.WriteLine();
                return;
            }

            // Display Results
            dynamic entityResult = JsonConvert.DeserializeObject(response.Content);
            System.Console.WriteLine("Entity: {0} [{1}]", entityResult.Name.Value, entityResult.ID.Value);
            
            foreach (var profile in entityResult.Profiles)
            {
                System.Console.WriteLine("{2}Profile: {0} [{1}]", profile.ID.Value, profile.Type.Value, string.Empty.PadLeft(4));
                for (int i = 0; i < profile.Strings.Count; i++) 
                {
                    if (profile.Strings[i] != null & profile.Strings[i].Value != null)
                    {
                        System.Console.WriteLine("{2}String {0}: {1}", i, profile.Strings[i].Value, string.Empty.PadLeft(8));
                    }
                }
                for (int i = 0; i < profile.Integers.Count; i++)
                {
                    if (profile.Integers[i] != null & profile.Integers[i].Value != null)
                    {
                        System.Console.WriteLine("{2}Integer {0}: {1}", i, profile.Integers[i].Value, string.Empty.PadLeft(8));
                    }
                }
                for (int i = 0; i < profile.Doubles.Count; i++)
                {
                    if (profile.Doubles[i] != null & profile.Doubles[i].Value != null)
                    {
                        System.Console.WriteLine("{2}Double {0}: {1}", i, profile.Doubles[i].Value, string.Empty.PadLeft(8));
                    }
                }
                for (int i = 0; i < profile.Dates.Count; i++)
                {
                    if (profile.Dates[i] != null & profile.Dates[i].Value != null)
                    {
                        System.Console.WriteLine("{2}Date {0}: {1}", i, profile.Dates[i].Value, string.Empty.PadLeft(8));
                    }
                }
            }

            System.Console.WriteLine();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        private static void UpdateEntity(string token, int drawerID, int entityID)
        {
            System.Console.WriteLine("=== Update Entity ===");

            // Execute REST call (GET entity first)
            var entityClient = new EntityClient(EnterpriseEndpointUri);
            var response = entityClient.Get(token, drawerID, entityID);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Console.WriteLine();
                return;
            }

            dynamic entityResult = JsonConvert.DeserializeObject(response.Content);

            // Set first field to "Goodbye"
            entityResult.Profiles[0].Strings[0].Value = "Goodbye";

            // Execute REST call (UPDATE)
            response = entityClient.Update(token, drawerID, entityID, JsonConvert.SerializeObject(entityResult));

            // Display Result
            System.Console.WriteLine("Result: {0}", response.StatusCode);

            System.Console.WriteLine();            
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        private static void DeleteEntity(string token, int drawerID, int entityID)
        {
            System.Console.WriteLine("=== Delete Entity ===");
            var entityClient = new EntityClient(EnterpriseEndpointUri);
            var response = entityClient.Delete(token, drawerID, entityID);

            System.Console.WriteLine("Result: {0}", response.StatusCode);
            System.Console.WriteLine();
        }


        #endregion Entitys
    }
}
