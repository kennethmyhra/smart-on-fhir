using System.Linq;
using System.Windows.Forms;
using Hl7.Fhir.Model;

namespace EHRApp
{
    public partial class PatientForm : Form
    {
        public PatientForm()
        {
            InitializeComponent();
        }

        internal void SetPatient(Patient patient)
        {
            Patient = patient;
            PopulateForm(Patient);
        }

        internal Patient Patient { get; private set; }

        private void PopulateForm(Patient patient)
        {
            HumanName name = patient.Name.FirstOrDefault();
            if(name != null)
                nameTextBox.Text = $"{name.Given.First()} {name.Family}";
            genderTextBox.Text = patient.Gender.ToString();
            birthDateTextBox.Text = patient.BirthDate;
            Address address = patient.Address.FirstOrDefault();
            if (address != null)
            {
                streetAddressTextBox.Text = address.Line.FirstOrDefault();
                zipCodeTextBox.Text = address.PostalCode;
                postalPlaceTextBox.Text = address.District;
            }
        }
    }
}
