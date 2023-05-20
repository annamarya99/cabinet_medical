using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Medical
{
    internal class Doctor:ICloneable, IComparable<Doctor>
    {
        private string name;
        private int phone;
        private string role;
        private string gender;
      

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Phone
        {
            get { return phone; }
            set { phone = value; }

        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        public Doctor() { }
        public Doctor(string name, int phone, string role, string gender)
        {
            this.name = name;
            this.phone = phone;
            this.role = role;
            this.gender = gender;
        }

        public object Clone()
        {
            Doctor clone = new Doctor();
            clone.name = this.name;
            clone.phone = this.phone;
            clone.gender = this.gender;
            clone.role = this.role;
            return clone;
        }

        public int CompareTo(Doctor other)
        {
            int res = this.name.CompareTo(other.name);
            if (res == 0)
            {
                res = this.phone.CompareTo(other.phone);
            }
            if (res == 0)
            {
                res = this.gender.CompareTo(other.gender);
            }
            if (res == 0)
            {
                res = this.role.CompareTo(other.role);
            }
            return res;
        }
    }
}


