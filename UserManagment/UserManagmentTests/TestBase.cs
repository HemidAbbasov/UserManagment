using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using LinqToDB.Data;

namespace UserManagmentTests
{
    public class TestBase
    {
        static TestBase()
        {
            DataConnection.TurnTraceSwitchOn();
            DataConnection.WriteTraceLine = (s, s1) =>
            {
                Console.WriteLine("{0}: {1}", s1, s);
                Debug.WriteLine(s, s1);
            };

            var assemblyPath = typeof(TestBase).Assembly.CodeBase;
            assemblyPath = Path.GetDirectoryName(assemblyPath.Substring("file:///".Length));
            if ( assemblyPath != null )
            {
                Environment.CurrentDirectory = assemblyPath;
            }
        }

        protected static int GetResult<T>(IEnumerable<T> queryable) where T : class
        {
            int result = queryable.ToList().Count;
            Console.WriteLine("\n \n");
            Console.WriteLine("================================");
            Console.WriteLine("Records Count: {0}", result);
            Console.WriteLine("Record Type: {0}", typeof(T));
            Console.WriteLine("================================");
            Console.WriteLine("\n \n");
            return result;
        } 
    }
}