using System;

namespace Less5_DZ_Viktor_Vill
{
    public class Employee
    {
        private int _id;
        private string _name;
        private string _firstname;
        private string _departament;
        private string _position;
        private DateTime _birthDay;

        public string Name { get => _name; set => _name = value; }
        public string FirstName { get => _firstname; set => _firstname = value; }
        public string Departament { get => _departament; set => _departament = value; }
        public string Position { get => _position; set => _position = value; }
        public DateTime BirthDay { get => _birthDay; set => _birthDay = value; }
        public int ID { get => _id; set => _id = value; }

        public Employee()
        { }

        public Employee(int id, string name, string firstname, string departament, string position, DateTime birthDay)
        {
            _id = id;
            _name = name;
            _firstname = firstname;
            _departament = departament;
            _position = position;
            _birthDay = birthDay;
        }
    }
}
