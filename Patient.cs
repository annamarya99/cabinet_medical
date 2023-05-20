using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Medical
{
    internal class Patient: ICloneable, IComparable<Patient>
    {
        private string name;
        private int phone;
        private string adress;
        private string gender;
        private string dateBirth;

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

        public string Adress
        {
            get { return adress; }
            set { adress= value; }
        }

        public string DateBirth
        {
            get { return dateBirth; }
            set { dateBirth = value;}
        }
        public string Gender
        {
            get { return gender;}
            set{ gender = value; }
        }
  

        public Patient() { }
        public Patient(string name, int phone, string adress, string gender, string dateBirth)
        {
            this.name = name;
            this.phone = phone;
            this.adress = adress;
            this.gender = gender;
            this.dateBirth = dateBirth;
        }

        public object Clone()
        {
            Patient clone = new Patient();
            clone.name = this.name;
            clone.phone = this.phone;
            clone.adress = this.adress;
            clone.gender = this.gender;
            clone.dateBirth = this.dateBirth;
            return clone;
        }

        public int CompareTo(Patient other)
        {
            int res = this.name.CompareTo(other.name);
            if (res == 0)
            {
                res=this.phone.CompareTo(other.phone);
            }
            if(res == 0)
            {
                res= this.adress.CompareTo(other.adress);
            }
            if(res == 0)
            {
                res=this.gender.CompareTo(other.gender);
            }
            if(res == 0)
            {
                res=this.dateBirth.CompareTo(other.dateBirth);
            }
            return res;
        }

        public static bool operator==(Patient a, Patient b)
        {
            bool status = false;
            if (a.gender == b.gender)
            {
                status = true;
            }
            return status;
        }
        public static bool operator !=(Patient a, Patient b)
        {
            bool status = false;
            if(a.gender!=b.gender)
            {
                status=true;
            }
            return status;
        }
    }
}
