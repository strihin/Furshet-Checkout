using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Furshet.Order;

/*
Алгоритм для автотеста:
1.  Перейти  по ссылке: http://furshet.altsolution.ua/
2. Нажать кнопку "Вход" для входа в систему
3. Ввод данных для входа - телефон и пароль, успешная авторизация
4. Клик "Войти"
5. Нажать кнопку "купить" для выбранных элементов
6. Нажать кнопку "оформить заказ" => переход по ссылке: http://furshet.altsolution.ua/order/process
7. Заполнить поля для оформления заказа: http://clip2net.com/s/3DGOk6y
8. Выбрать 1 из разделов в таблице и селект ниже: http://clip2net.com/s/3DGOKw2
9. Выбрать селекты в разделах "Вид упаковки" и в разделе "Способ оплаты"
10. Ввести в 2 инпута свои личные данные в разделе "Личные данные"
11. Заполнить текст в разделе "Дополнительно"
12. Нажать кнопку "Сделать заказ"
13. Нажать кнопку "Отправить номер в виде СМС"
*/

namespace TestBySeleniumTitorialXXVI.X
{
    class Program
    {
        #region random
        /*
        public static T[] SubArrayDeepClone<T>(T[] data, int index, int length)
        {
            T[] arrCopy = new T[length];
            Array.Copy(data, index, arrCopy, 0, length);
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, arrCopy);
                ms.Position = 0;
                return (T[])bf.Deserialize(ms);
            }
        }
        public static string[] GetRandomValue()
        {
            string[] array = new[] {
            "21546",
            "22039",
            "23066",
            "23176",
            "24134",
            "24153",
            "31949",
            "31953",
            "34855",
            "37310",
            "38703",
            "23066",
            "38703",
            "21546",
            "20588"
        };
            SubArrayDeepClone(array, 1, 13);
            
            var count = array.Length;
            
            var random= new Random();
            int howMuchSelectElement= random.Next(1, count-1);
            int startPosition = random.Next(0, howMuchSelectElement - 1);
           // string[] outputArray = new List<string>(array).GetRange(startPosition, howMuchSelectElement).ToArray();
           

            return SubArrayDeepClone(array, startPosition, howMuchSelectElement); ;
        }
        */
        #endregion
        static void Main(string[] args)
        {
            #region InputingIDsOfProducts
            string[] array = new[] {
            "21546",
            "22039",
            "23066",
            "23176",
            "24134",
            "24153",
            "31949",
            "31953",
            "34855",
            "37310",
            "38703",
            "23066",
            "38703",
            "21546",
            "20588"
        };
            var arr1 = new[]
            {
                "21546",
                "22039",
                "23066",
                "23176"
            };
            var arr2 = new[]
            {
                "24153",
                "31949",
                "31953",
                "34855",
                "37310",
                "38703"
            };
            var arr3 = new[]
            {
                "34855",
                "37310",
                "38703",
                "23066",
                "38703",
                "21546",
                "20588"
            };
            var arr4 = new[]
            {
                "34855",
                "37310",
                "38703"
            };
            #endregion
            //inizitialisation
            IWebDriver driverChrome = new ChromeDriver();
            Cases cases= new Cases();

            MainUnitTest unitTest0 = new MainUnitTest("+380505577332", "1q2w3e", arr4, driverChrome);
            unitTest0.Navigate();
            CheckoutOrder order0 = new CheckoutOrder(2, "Lisichansk", "Petrovskogo", "5B", 6, 2, 2, "comment to address");
            unitTest0.CheckoutOrder(order0);
            cases.OutputFailedCases("I",unitTest0);
            
            MainUnitTest unitTest = new MainUnitTest("+380666999966", "1q2w3e", arr1 , driverChrome);
            unitTest.Navigate();
            CheckoutOrder order= new CheckoutOrder(2, "Severodonetsk", "Mayakovskogo", "58", 4, 2, 2, "comment to address");
            unitTest.CheckoutOrder(order);
            cases.OutputFailedCases("II", unitTest);

            MainUnitTest unitTest2 = new MainUnitTest("+380123456789", "1q2w3e", arr2, driverChrome);
            unitTest2.Navigate();
            CheckoutOrder order2 = new CheckoutOrder(2, "Kiev", "Lenina", "33", 18, 10, 1, "comment to address2");
            unitTest2.CheckoutOrder(order2);
            cases.OutputFailedCases("III", unitTest2);

            MainUnitTest unitTest3 = new MainUnitTest("+380101010101", "1q2w3e", arr3, driverChrome);
            unitTest3.Navigate();
            CheckoutOrder order3 = new CheckoutOrder(2, "Odessa", "Chkalova", "16", 1, 1, 1, "comment to address2");
            unitTest3.CheckoutOrder(order3);
            cases.OutputFailedCases("IV", unitTest3);

            driverChrome.Close();
            Environment.Exit(0);
        }
    }
}
