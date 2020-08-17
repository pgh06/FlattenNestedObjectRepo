using System;
using System.Collections;

using System.Collections.Generic;

using System.Reflection;



namespace Test
{
    public class Test
    {
        public static bool NameFound { get; set; }

        public static string SearchQuery
        {
            get
            {
                return "FindMe";
            }

            set
            {

            }
        }



        public static void PrintProperties(object obj, int indent)
        {
            if (!NameFound)
            {
                if (obj == null) return;
                string indentString = new string(' ', indent);
                Type objType = obj.GetType();
                PropertyInfo[] properties = objType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object propValue = property.GetValue(obj, null);
                    var elems = propValue as IList;


                    if (elems != null)
                        foreach (var item in elems)
                            PrintProperties(item, indent + 3);
                    else
                    {
                        Console.WriteLine("{0}{1}: {2}", indentString, property.Name, propValue);
                        if (ReferenceEquals(propValue, SearchQuery))
                            NameFound = ReferenceEquals(propValue, SearchQuery);

                    }
                }
            }
        }

        class Node
        {
            public Node()
            {
                Children = new List<Node>();
            }
            public string Name { get; set; }
            public List<Node> Children { get; set; }
        }

        static Node SearchMe = new Node()

        {
            Name = "Start",
            Children = new List<Node>()
            {
                new Node(){
                    Name = "A1",
                    Children = new List<Node>(){
                        new Node(){
                            Name = "B1",
                            Children = new List<Node>(){
                                new Node(){
                                    Name = "C1"
                                }}

                        },
                        new Node(){
                            Name = "B2",
                            Children = new List<Node>(){
                                new Node(){
                                    Name = "FindMe",
                                    Children = new List<Node>(){
                                        new Node(){
                                            Name = "TooFar"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Node(){
                    Name = "A2",
                    Children = new List<Node>(){
                        new Node(){
                            Name = "D1",
                            Children = new List<Node>(){
                                new Node(){
                                    Name = "E1"
                                }
                            }
                        }
                    }
                }
            }
        };



        static void Main()
        {
            var obj = Test.SearchMe;
            PrintProperties(obj, 0);
            Console.ReadLine();
        }
    }
}