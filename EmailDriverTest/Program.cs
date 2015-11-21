using LINQPad;
using System;


namespace EmailDriverTest
{
    class Program
    {
        static void Main(string[] args)
        {

           var compq= Util.Run(@"C:\ChristineLists.linq",QueryResultFormat.Text, args);
            compq.Wait();
            compq.Dump();
            
            
        
        }
    }
}
