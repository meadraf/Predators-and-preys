namespace Predators_and_preys.GameLogic;

    public class MyString : BindableObject
    {

        public MyString(string value)
        {
            ValueString = value;
        }

        private string _valueString;
        public string ValueString
        {
            get => _valueString;
            set
            {
                if (_valueString != value)
                {
                    _valueString = value;
                    OnPropertyChanged();
                }
            }
        }


    }

