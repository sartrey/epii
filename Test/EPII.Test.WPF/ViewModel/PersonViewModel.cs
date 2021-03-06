﻿using EPII.Data;
using EPII.Test.Data;
using EPII.UI.WPF;
using System;

namespace EPII.Test.WPF
{
    public class PersonViewModel : ViewModel
    {
        //private string _Name;
        //private DateTime _Birth;

        //model is NOT always local-ready in real vm
        private Person _Person = null;

        //real vm has NOT data source member
        private FakeDataSource _Source
            = new FakeDataSource();

        private Person Person 
        {
            get { return _Person; }
            set 
            {
                new Guard<Person>(value).NotNull();
                _Person = value;
                Notice(() => Person);
                Notice(() => Name);
                Notice(() => BirthText);
            }
        }

        public string Name
        {
            get { return _Person != null ? _Person.Name : null; }
            set 
            {
                _Person.Name = value;
                Notice(() => Name);
            }
        }

        public DateTime Birth
        {
            get { return _Person != null ? _Person.Birth : DateTime.Now; }
            set 
            {
                _Person.Birth = value;
                //Notice(() => Birth);
            }
        }

        public string BirthText 
        {
            get { return Birth.ToString("yyyy年MM月dd日"); }
            set 
            {
                DateTime temp = DateTime.Now;
                if (DateTime.TryParse(value, out temp)) {
                    Birth = temp;
                    Notice(() => BirthText);
                }
            }
        }

        public PersonViewModel() 
        {
        }

        public void GetNextPerson()
        {
            var index = Person == null ? 0 :
                    _Source.Persons.FindIndex(
                        e => e.Name == Person.Name);
            if (index == _Source.Persons.Count - 1)
                index = 0;
            else
                index++;
            Person = _Source.Persons[index];
        }
    }
}
