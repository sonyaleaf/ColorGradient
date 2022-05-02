using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorGradientTestProject {
    public class ViewModel : INotifyPropertyChanged, IDataErrorInfo, INotifyDataErrorInfo {

        public ICommand MyCommand { get; set; }

        public ViewModel() {
            MyCommand = new RelayCommand(ExecuteMyMethod, CanExecuteMyMethod);
        }

        public ObservableCollection<Color> Colors { get; set; } = new ObservableCollection<Color>();

        public Brush testWindowBrush { get; set; }

        Color _color1;

        public Color SelectedColor1 {
            get { return _color1; }
            set {
                _color1 = value;
                if(_color1 == null)
                    return;
            }
        }

        Color _color2;

        public Color SelectedColor2 {
            get { return _color2; }
            set {
                _color2 = value;
                if(_color2 == null)
                    return;
            }
        }

        double? _angle;

        public double? Angle {
            get { return _angle; }
            set {
                _angle = value;
                if(_angle == null)
                    return;
            }
        }

        private bool CanExecuteMyMethod(object parameter) {
            if(HasErrors) {
                return false;
            } else {
                return true;
            }
        }

        private void ExecuteMyMethod(object parameter) {
            testWindowBrush = new LinearGradientBrush(System.Windows.Media.Color.FromRgb(_color1.R, _color1.G, _color1.B), System.Windows.Media.Color.FromRgb(_color2.R, _color2.G, _color2.B), _angle.Value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(testWindowBrush)));
        }

        public IEnumerable GetErrors(string propertyName) {
            if(errors.ContainsKey(propertyName)) {
                return errors[propertyName];
            } else {
                return null;
            }
        }

        private string myVar;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public string MyProperty {
            get { return myVar; }
            set { myVar = value; }
        }

        public string Error {
            get {
                return "";
            }
        }

        public bool HasErrors {
            get { return errors.Any(); }
        }

        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public string this[string columnName] {
            get {
                errors.Remove(columnName);
                switch(columnName) {
                    case nameof(Angle):
                        if(_angle.HasValue) {
                            if(_angle.Value >= 0 && _angle.Value < 360) {
                                break;
                            } else {
                                errors.Add(columnName, new List<string> { "Angle must be greater than or equal to 0 and less than 360." });
                                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Angle)));
                            }
                        } else {
                            errors.Add(columnName, new List<string> { "Angle must have a value." });
                            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Angle)));
                        }
                        break;
                    case nameof(SelectedColor1):
                        if(_color1 != null) {
                            break;
                        } else {
                            errors.Add(columnName, new List<string> { "First color must not be null." });
                            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(SelectedColor1)));
                        }
                        break;
                    case nameof(SelectedColor2):
                        if(_color2 != null) {
                            break;
                        } else {
                            errors.Add(columnName, new List<string> { "Second color must not be null." });
                            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(SelectedColor2)));
                        }
                        break;
                }
                return "";
            }
        }
    }

    public class Color {
        public string ColorName { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }

    public class RelayCommand : ICommand {

        Action<object> _executeMethod;
        Func<object, bool> _canExecuteMethod;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod) {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter) {
            if(_canExecuteMethod != null) {
                return _canExecuteMethod(parameter);
            } else {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) {
            _executeMethod(parameter);
        }
    }
}
