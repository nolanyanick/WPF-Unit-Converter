using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;
namespace WpfUnitConverter.ViewModels
{
    class UnitConverterViewModel : ObservableObject
    {
        public class NumberValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                double result = 0.0;
                bool canConvert = double.TryParse(value as string, out result);
                return new ValidationResult(canConvert, "Only numbers can be calculated, please try again");
            }
        }

        public class UpdateResult : INotifyPropertyChanged 
        {
            public event PropertyChangedEventHandler PropertyChanged;
        }

        public ICommand ButtonConvertCommand { get; set; }
        public ICommand Textbox { get; set; }

        private double _result;

        private double _operand;

        public double Operand
        {
            get { return _operand; }
            set {
                _operand = value;
                OnPropertyChanged();
            }
        }

        public string OperandUnit { get; set; }

        public string ResultUnit { get; set; }

        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public List<string> Units { get; set; }

        private List<string> BuildOutUnitComboBoxSource()
        {
            return new List<string>() { "Inches", "Feet", "Miles",
                "Millimeters", "Centimeters", "Meters", "Kilometers" };
        }

        public string placeholderText {get; set;}

        #region DICTIONARIES

        //millimeter values
        public Dictionary<string, double> mmValues = new Dictionary<string, double>()
        {
            { "Millimeters", 1},
            {"Centimeters", 0.1},
            {"Inches", 0.0393701},
            {"Feet", 0.00328084},
            {"Meters", 0.001},
            {"Kilometers", 1e-6},
            {"Miles", 6.21371e-7},
        };

        //centimeter values
        public Dictionary<string, double> cmValues = new Dictionary<string, double>()
        {
            {"Millimeters", 10},
            {"Centimeters", 1},
            {"Inches", 0.393701},
            {"Feet", 0.0328084},
            {"Meters", 0.01},
            {"Kilometers", 1e-5},
            {"Miles", 6.2137e-6},
        };

        //inch values
        public Dictionary<string, double> inValues = new Dictionary<string, double>()
        {
            {"Millimeters", 25.4},
            {"Centimeters", 2.54},
            {"Inches", 1},
            {"Feet", 0.0833333},
            {"Meters", 0.0254},
            {"Kilometers", 2.54e-5},
            {"Miles", 1.57828e-5},
        };

        //feet values
        public Dictionary<string, double> ftValues = new Dictionary<string, double>()
        {
            {"Millimeters", 304.8},
            {"Centimeters", 30.48},
            {"Inches", 12},
            {"Feet", 1},
            {"Meters", 0.3048},
            {"Kilometers", 0.0003048},
            {"Miles", 0.000189394},
        };

        //meter values
        public Dictionary<string, double> mValues = new Dictionary<string, double>()
        {
            {"Millimeters", 1000},
            {"Centimeters", 100},
            {"Inches", 39.3701},
            {"Feet", 3.28084},
            {"Meters", 1},
            {"Kilometers", 0.001},
            {"Miles", 0.000621371},
        };

        //kilometer values
        public Dictionary<string, double> kmValues = new Dictionary<string, double>()
        {
            { "Millimeters", 1000000},
            {"Centimeters", 100000},
            {"Inches", 39370.1},
            {"Feet", 3280.84},
            {"Meters", 1000},
            {"Kilometers", 1},
            {"Miles", 0.621371},
        };

        //mile values
        public Dictionary<string, double> miValues = new Dictionary<string, double>()
        {
            { "Millimeters", 1.609e+6},
            {"Centimeters", 160934},
            {"Inches", 63360},
            {"Feet", 5280},
            {"Meters", 1609.34},
            {"Kilometers", 1.60934},
            {"Miles", 1},
        };

        #endregion


        private void PerformCalculation(Object obj)
        {
            switch (OperandUnit)
            {
                case "Millimeters":
                    if (mmValues.ContainsKey(ResultUnit))
                    {
                        double number = mmValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Centimeters":
                    if (cmValues.ContainsKey(ResultUnit))
                    {
                        double number = cmValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Inches":
                    if (inValues.ContainsKey(ResultUnit))
                    {
                        double number = inValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Feet":
                    if (ftValues.ContainsKey(ResultUnit))
                    {
                        double number = ftValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Meters":
                    if (mValues.ContainsKey(ResultUnit))
                    {
                        double number = mValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Kilometers":
                    if (kmValues.ContainsKey(ResultUnit))
                    {
                        double number = kmValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;

                case "Miles":
                    if (miValues.ContainsKey(ResultUnit))
                    {
                        double number = miValues[ResultUnit];
                        Result = Operand * number;
                    }
                    break;
                default:
                    break;
            }
                        
        }

        public UnitConverterViewModel()
        {
            ResultUnit = "Meters";
            OperandUnit = "Millimeters";
            Units = BuildOutUnitComboBoxSource();
            ButtonConvertCommand = new RelayCommand(new Action<object>(PerformCalculation));
            Textbox = new RelayCommand(new Action<object>(PerformCalculation));
        }
    }
}