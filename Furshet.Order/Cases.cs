using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Furshet.Order
{
    class Cases
    {
        public List<MainUnitTest> maintests { get; set; }
        
        public List<MainUnitTest> passedCases { get; set; }
        public List<MainUnitTest> failedCases { get; set; }

        public Cases()
        {
            this.maintests = new List<MainUnitTest>();
            this.passedCases = new List<MainUnitTest>();
            this.failedCases = new List<MainUnitTest>();
        }

        public void SummaryOfModule(MainUnitTest testCase)
        {
            string[] moduleDescription = new string[3];
           
            moduleDescription[0] = ".Status of Authorization Module: \t\t";
            moduleDescription[1] = ".Status of Adding products to busket Module: \t";
            moduleDescription[2] = ".Status of Successfull Checkout Module: \t";

            for (int i=0;i<testCase.valueOfModule.Length;i++)
            {
               Console.Write((i+1)+moduleDescription[i]);
                if (testCase.valueOfModule[i])
                    Console.Write(" PASSED.\n");
                else
                    Console.Write(" FAILED.\n");
            }
        }
        public void AddFailedCases(MainUnitTest testCase)
        {
            failedCases.Add(testCase);
        }
        public void AddPassedCases(MainUnitTest testCase)
        {
            passedCases.Add(testCase);
        }

        public void OutputPassedCases()
        {
            Console.WriteLine("Passed cases:");
            foreach (var passedCase in passedCases)
            {
                foreach (var ID in passedCase.dataID)
                {
                    Console.WriteLine(ID);
                }
            }
        }
        public void OutputFailedCases(string numberCase, MainUnitTest testCase)
        {
            string content = "Case with products: ";
            Console.Write('\n'+numberCase+' '+content);
                foreach (var ID in testCase.productsList)
                {
                    Console.Write(ID+"; ");
                }
                Console.WriteLine("");
            SummaryOfModule(testCase);
        }
    }
}
