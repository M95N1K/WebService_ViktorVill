namespace Less5_DZ_Viktor_Vill
{
    public class Departament
    {
        private string _name;

        public string DepartamentName { get => _name; set => _name = value; }

        public Departament()
        { }

        public Departament(string departamentName)
        {
            _name = departamentName;
        }

    }
}
