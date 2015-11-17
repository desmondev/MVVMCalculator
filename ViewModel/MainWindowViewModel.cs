using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Calc.Model;

namespace Calc.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly Calculator _calculator;
        private string _display;
        private string _lastOperation;
        private bool _newDisplayRequired = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindowViewModel()
        {

            log.Debug("Creating view model");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            _calculator = new Calculator();
            Display = "0";
            CreateButtonCommand();
            CreateDotCommand();
            CreateClearCommand();
            CreatePlusMinusCommand();
            CreateOperationCommand();
            CreateSingularOperationCommand();
            CreatePercentageOperationCommand();
        }

        private string FirstArgument
        {
            get { return _calculator.FirstArgument; }
            set { _calculator.FirstArgument = value; }
        }

        private string SecondArgument
        {
            get { return _calculator.SecondArgument; }
            set { _calculator.SecondArgument = value; }
        }

        private string Operation
        {
            get { return _calculator.Operation; }
            set { _calculator.Operation = value; }
        }

        private string LastOperation
        {
            get { return _lastOperation; }
            set { _lastOperation = value; }
        }

        private string Result
        {
            get { return _calculator.Result; }
        }

        public ICommand ButtonCommand { get; set; }

        public ICommand OperationCommand { get; set; }

        public ICommand SingularOperationCommand { get; set; }

        public ICommand DotCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public ICommand PlusMinusCommand { get; set; }

        public ICommand PercentageOperationCommand { get; set; }

        public string Display
        {
            get { return _display; }
            set
            {
                _display = value;
                OnPropertyChanged("Display");
            }
        }

        public string FullExpression
        {
            get { return _calculator.FullExpression; }
            set
            {
                _calculator.FullExpression = value;
                OnPropertyChanged("FullExpression");
            }
        }

        private void CreateButtonCommand()
        {
            ButtonCommand = new RelayCommand(ButtonCommandFunc, o => !"Err".Equals(_display));
        }


        private void CreateDotCommand()
        {
            DotCommand = new RelayCommand(DotCommandFunc, o => !"Err".Equals(_display));
        }

        private void CreateSingularOperationCommand()
        {
            SingularOperationCommand = new RelayCommand(o => SingularOperationCommandFunc(o as string),
                o => !"Err".Equals(_display));
        }

        private void CreateClearCommand()
        {
            ClearCommand = new RelayCommand(o => ClearCommandFunction(), o => true);
        }

        private void CreateOperationCommand()
        {
            OperationCommand = new RelayCommand(o => OperationCommandFunc(o as string), o => !"Err".Equals(_display));
        }

        private void CreatePlusMinusCommand()
        {
            PlusMinusCommand = new RelayCommand(o =>
            {
                PlusMinusCommandFunc();
            }, o => !"Err".Equals(_display));
        }

        private void CreatePercentageOperationCommand()
        {
            PercentageOperationCommand = new RelayCommand(o => PercentageOperationCommandFunc(o), o => !"Err".Equals(_display));
        }

        private void PlusMinusCommandFunc()
        {
            if (_display.Contains("-"))
            {
                Display = _display.Remove(_display.IndexOf("-", StringComparison.Ordinal), 1);
            }
            else Display = "-" + _display;
            _newDisplayRequired = false;
        }

        public void PercentageOperationCommandFunc(object o)
        {
            try
            {
                if (FirstArgument != string.Empty && LastOperation != null && LastOperation != "=")
                {
                    SecondArgument = _display;
                    string previousOperation = _lastOperation;
                    Operation = o as string;
                    _calculator.CalculateResult();
                    LastOperation = previousOperation;
                    Display = Result;
                }
                _newDisplayRequired = true;
            }
            catch (Exception ex)
            {
                Display = Result == string.Empty ? "Err" : Result;
            }
        }
        public void DotCommandFunc(object obj)
        {
            if (_newDisplayRequired)
            {
                Display = "0.";
            }
            else
            {
                if (!_display.Contains("."))
                {
                    Display = _display + ".";
                }
            }
            _newDisplayRequired = false;
        }

        public void ButtonCommandFunc(object button)
        {
            if (_display == "0" || _newDisplayRequired)
                Display = (string)button;
            else
                Display = _display + button;
            _newDisplayRequired = false;
        }

        public void OperationCommandFunc(string operation)
        {
            try
            {
                if (FirstArgument == string.Empty || LastOperation == "=")
                {
                    FirstArgument = _display;
                    LastOperation = operation;
                }
                else
                {
                    SecondArgument = _display;
                    Operation = _lastOperation;
                    log.Debug("Current operation : " + FirstArgument + " " + Operation + " " + SecondArgument);
                    _calculator.CalculateResult();

                    FullExpression = ParseFullExpression();

                    LastOperation = operation;
                    Display = Result;
                    FirstArgument = _display;
                }
                _newDisplayRequired = true;
            }
            catch (Exception ex)
            {
                Display = Result == string.Empty ? "Err" : Result;
            }
        }

        private string ParseFullExpression()
        {
            return Math.Round(Convert.ToDouble(FirstArgument), 10) + " " + Operation + " "
                   + Math.Round(Convert.ToDouble(SecondArgument), 10) + " = "
                   + Math.Round(Convert.ToDouble(Result), 10);
        }

        public void SingularOperationCommandFunc(string operation)
        {
            try
            {
                FirstArgument = Display;
                Operation = operation;
                log.Debug("Current operation : " + FirstArgument + " " + Operation);

                _calculator.CalculateResult();

                FullExpression = Operation + "(" + Math.Round(Convert.ToDouble(FirstArgument), 10) + ") = "
                    + Math.Round(Convert.ToDouble(Result), 10);

                LastOperation = "=";
                Display = Result;
                FirstArgument = _display;
                _newDisplayRequired = true;
            }
            catch (Exception ex)
            {
                Display = Result == string.Empty ? "Err" : Result;
            }
        }

        public void ClearCommandFunction()
        {
            Display = "0";
            FirstArgument = string.Empty;
            SecondArgument = string.Empty;
            Operation = string.Empty;
            LastOperation = string.Empty;
            FullExpression = string.Empty;
        }
    }
}
