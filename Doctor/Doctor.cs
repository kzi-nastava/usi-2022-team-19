using Newtonsoft.Json;

public enum Field{
    Surgeon,
    Kardiolog,
    Psychiatrists,
    Radiologists,
    Ophthalmology,
    Paediatricians,
} 

public class Doctor
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public Field field{ get; set; }
    public List<int> doctorAppointments { get; set; }

    public Doctor(int id, string name, string email, string password, Field field, List<int> doctorAppointments){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.field = field;
        this.doctorAppointments = doctorAppointments;
    }

    public override string ToString()
    {
        string appointments = "";
        foreach (var appointment in doctorAppointments){
            appointments += appointment.ToString() + " ";
        }
        return "Doctor [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + ", field: " + field + ", doctor appointment: [" + appointments + "]]";
    }
    
    public static void doctorMenu(Doctor doctor){
        Console.WriteLine("Welcome, "+doctor.name+ "!");
        
        Console.WriteLine("\n");

        int option=0;
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");

        while (option!=3){
            Console.WriteLine("DOCTOR MENU");
            Console.WriteLine("1. CRUD for Doctor Appointments");
            Console.WriteLine("2. Physical examination");
            Console.WriteLine("3. Log out");

            Console.WriteLine("Enter an option: ");
            option=Convert.ToInt32(Console.ReadLine());
            if (option==1){
                while (option!=5){
                    Console.WriteLine("CRUD for Doctor Appointments");
                    Console.WriteLine("1. Create new appointment");
                    Console.WriteLine("2. Timetable review");
                    Console.WriteLine("3. Update an appointment");
                    Console.WriteLine("4. Delete an appointment");
                    Console.WriteLine("5. Back");

                    Console.WriteLine("Enter an option: ");
                    option=Convert.ToInt32(Console.ReadLine());
                    if (option==1){
                        DoctorAppointment.CreateAppointment(doctor);
                        
                    }
                    if (option==2){
                        doctor.ReviewTimetable();
                        Console.WriteLine("Do you want to see someone's medical record? Enter id.");
                        var id=Console.ReadLine();
                        PatientsFactory patients = new PatientsFactory("Data/patients.json");
                        foreach(Patient patient in patients.allPatients){
                            if (patient.id==Convert.ToInt32(id)){
                                Console.WriteLine(patient);
                            }
                        }
                        
                    }
                    if (option==3){
                        doctor.UpdateAppointment(doctor);
                    }
                    if (option==4){
                        
                        DoctorsFactory doctors = new DoctorsFactory("Data/doctors.json");
                        doctors.allDoctors.Remove(doctor);
                        
                        doctor.DeleteAppointment();

                        doctors.allDoctors.Add(doctor);
                        DoctorsFactory.UpdateDoctors(doctors.allDoctors);
                    }
                        
                    }
            }
            if (option==2){
                Console.WriteLine("Physical examination");
                doctor.ReviewTimetable();
                Console.WriteLine("Enter id of the appointment you want to do.");
                int id=Convert.ToInt32(Console.ReadLine());

        
                DoctorAppointment appointmentToDo=null;
                foreach(DoctorAppointment appointment in appointments.allDoctorAppointments){
                    if (appointment.id==id){
                        appointmentToDo=appointment;
                        
                    }
                if(appointmentToDo!=null){
                    Console.WriteLine(appointmentToDo.patient);
                    List<DoctorAppointment> permanentAppointments=appointments.allDoctorAppointments;
                    int number=0;
                    while (number!=5){
                        Console.WriteLine("What do you want to update? Enter number.");
                        Console.WriteLine("1.height\n2.weight\n3.previous illnesses\n4.allergens\n5.done");
                        number=Convert.ToInt32(Console.ReadLine());
                        if(number==1){
                            Console.WriteLine("Enter new patient's height: ");

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            int newHeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.height=newHeight;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);


                        }
                        else if(number==2){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                             Console.WriteLine("Enter new patient's weight: ");
                            int newWeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.weight=newWeight;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);

                        }
                        else if(number==3){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter patient's previous illnesses: ");
                            string newIllnesses=Console.ReadLine();
                            List<string>illnesses=new List<string>();
                            illnesses.Add(newIllnesses);

                            appointmentToDo.patient.medicalRecord.previousIllnesses=illnesses;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);
                        }
                        else if(number==4){
                                                       
                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter new patient's allergens: ");
                            string newAlergens=Console.ReadLine();
                            List<string>alergens=new List<string>();
                            alergens.Add(newAlergens);
                            appointmentToDo.patient.medicalRecord.allergens=alergens;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);
                        }

                    }
                }
            }
        }        
    }}
}