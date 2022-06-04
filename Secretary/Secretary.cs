public class Secretary{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public Secretary(int id, string name, string email, string password){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
    }

    public override string ToString()
    {
        return "Secretary [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + "]";
    }
    
    public static Secretary FindById(int id, List<Secretary> allSecretaries){
        foreach (var secretary in allSecretaries){
            if (secretary.id == id){
                return secretary;
            }
        }
        return null;
    }
    public static void Menu(Secretary secretary){
        PatientsFactory patientsFactory = new PatientsFactory("Data/patients.json");
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        DeletionRequestsFactory deletionRequestsFactory = new DeletionRequestsFactory("Data/deletionRequests.json");
        ModificationRequestsFactory modificationRequestsFactory = new ModificationRequestsFactory("Data/modificationRequests.json");
        DoctorsFactory doctorsFactory = new DoctorsFactory("Data/doctors.json");
        ReferralsFactory referralsFectory = new ReferralsFactory("Data/referrals.json");
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory("Data/dynamicEquipment.json");
        RoomFactory roomFactory = new RoomFactory("Data/rooms.json");
        Console.WriteLine("Welcome, " + secretary.name + "!");
        Console.WriteLine("MENU FOR SECRETARY");
        while (true){
            Console.WriteLine("1. Patient accounts management\n2. Requests for modification and deletion of doctor appointments management\n3. Create a doctors appointment for patient via their referral\n4. Create urgent appointment\n5. Create request for missing equipment\n6. Rearrange dynamic equipment\n7. Log out");
            Console.WriteLine("Enter the option: ");
            var optionForMainMenu = Console.ReadLine();
            if (optionForMainMenu == "1"){
                while (true){
                    Console.WriteLine("1. Create new patient account\n2. Modify patient account\n3. Delete patient account\n4. View all patients\n5. Block patient account\n6. Unblock patient account\n7. View all blocked patients\n8. Back");
                    Console.WriteLine("Enter the option: ");
                    var optionForPatientAccountsMenu = Console.ReadLine();
                    if (optionForPatientAccountsMenu == "1"){
                        Patient.CreateNewAccount(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "2"){
                        Patient.ModifyAccount(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "3"){
                        Patient.DeleteAccount(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "4"){
                        Patient.ViewAllPatients(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "5"){
                        Patient.BlockAccount(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "6"){
                        Patient.UnblockAccount(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "7"){
                        Patient.ViewAllBlockedPatients(patientsFactory.allPatients);
                    } else if (optionForPatientAccountsMenu == "8"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "2"){
                while (true) {
                    Console.WriteLine("1. View modification requests\n2. View deletion requests\n3. Back");
                    Console.WriteLine("Enter the option:");
                    var optionForRequestsMenu = Console.ReadLine();
                    if (optionForRequestsMenu == "1"){
                        ModificationRequest.ManageRequest(modificationRequestsFactory.allModificationRequests);
                    } else if (optionForRequestsMenu == "2"){
                        DeletionRequest.ManageDeletionRequests(deletionRequestsFactory.allDeletionRequests);
                    } else if (optionForRequestsMenu == "3"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "3"){
                while (true) {
                    Console.WriteLine("1. View all patients with referrals\n2. Create appointment for patient\n3. Back");
                    Console.WriteLine("Enter the option:");
                    var optionForReferralsMenu = Console.ReadLine();
                    if (optionForReferralsMenu == "1"){
                        Patient.ViewAllPatientsWithReferral(patientsFactory.allPatients);
                    } else if (optionForReferralsMenu == "2"){
                        Referral referral = Referral.FindReferralByPatient(patientsFactory.allPatients);
                        DoctorAppointment.CreateViaReferral(referral.referralsId, referralsFectory.allRefferals, referral.patientsId, referral.doctorsId, patientsFactory.allPatients, doctorsFactory.allDoctors, doctorAppointmentsFactory.allDoctorAppointments);
                    } else if (optionForReferralsMenu == "3"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "4"){
                Console.WriteLine("Enter patient's id: ");
                var patientsId = Console.ReadLine();
                int field = Doctor.FindField();
                DoctorAppointment.CreateUrgentAppointment(Convert.ToInt32(patientsId), (Field)field, patientsFactory.allPatients, doctorsFactory.allDoctors, doctorAppointmentsFactory.allDoctorAppointments);
            } else if (optionForMainMenu == "5"){
                DynamicEquipment.OrderMissingEquipment(dynamicEquipmentFactory.allEquipment, roomFactory.allRooms);
            } else if (optionForMainMenu == "6"){
                DynamicEquipment.RearrangeTheEquipment(roomFactory.allRooms, dynamicEquipmentFactory.allEquipment);
            } else if (optionForMainMenu == "7"){
                break;
            } else {
                Console.WriteLine("You entered wrong option! Please try again.");
            }
        }
    }

}
