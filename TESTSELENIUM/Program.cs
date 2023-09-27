using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace TESTSELENIUM
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ingresar com una cuenta de administrador al la pagina rabrimos una ventana de chrome
            IWebDriver driverEntrar = new ChromeDriver();
            //ingresamos la direccion a ala que queremos ir
            driverEntrar.Navigate().GoToUrl("http://localhost:4200/ParkingLot");
            Thread.Sleep(2000);

            //selection de num
            var inputNum = driverEntrar.FindElement(By.Id("mat-select-value-5"));
            inputNum.Click();


            //filtro de pais
            var inputFilter = driverEntrar.FindElement(By.Id("mat-select-value-1"));
            inputFilter.Click();
            var inputPais = driverEntrar.FindElement(By.Id("mat-option-7"));
            inputPais.Click();
            Thread.Sleep(2000);

            //direccion
            var inputDirec = driverEntrar.FindElement(By.Id("mat-input-0"));
            inputDirec.SendKeys("calle 43 carrera 5");
            Thread.Sleep(2000);

            //Seleccionar Discap o No 
            var inputDiscap = driverEntrar.FindElement(By.Id("mat-select-value-3"));
            inputDiscap.Click();
            var inputYN = driverEntrar.FindElement(By.Id("mat-option-2"));
            inputYN.Click();
            Thread.Sleep(2000);

            //Mostrar Info
            var inputButton = driverEntrar.FindElement(By.ClassName("mat-mdc-button-touch-target"));
            inputButton.Click();
        }

    }
}